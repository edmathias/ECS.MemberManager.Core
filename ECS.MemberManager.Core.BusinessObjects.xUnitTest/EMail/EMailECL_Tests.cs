using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EMailECL_Tests 
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public EMailECL_Tests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var testLibrary = _config.GetValue<string>("TestLibrary");
            
            if(testLibrary == "Mock")
                MockDb.ResetMockDb();
            else
            {
                if (!IsDatabaseBuilt)
                {
                    var adoDb = new ADODb();
                    adoDb.BuildMemberManagerADODb();
                    IsDatabaseBuilt = true;
                }
            }
        }
        
        [Fact]
        private async void EMailECL_TestEMailECL()
        {
            var eMailEdit = await EMailECL.NewEMailECL();

            Assert.NotNull(eMailEdit);
            Assert.IsType<EMailECL>(eMailEdit);
        }

        
        [Fact]
        private async void EMailECL_TestGetEMailECL()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailDal>();
            var childData = await dal.Fetch();
            
            var listToTest = await EMailECL.GetEMailECL(childData);
            
            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }
        
        [Fact]
        private async void EMailECL_TestDeleteEMailEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailDal>();
            var childData = await dal.Fetch();
            
            var eMailEditList = await EMailECL.GetEMailECL(childData);

            var eMail = eMailEditList.First(a => a.Id == 99);

            // remove is deferred delete
            eMailEditList.Remove(eMail); 

            var eMailListAfterDelete = await eMailEditList.SaveAsync();
            
            Assert.NotEqual(childData.Count,eMailListAfterDelete.Count);
        }

        [Fact]
        private async void EMailECL_TestUpdateEMailEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailDal>();
            var childData = await dal.Fetch();
            
            var eMailList = await EMailECL.GetEMailECL(childData);
            var countBeforeUpdate = eMailList.Count;
            var idToUpdate = eMailList.Min(a => a.Id);
            var eMailToUpdate = eMailList.First(a => a.Id == idToUpdate);

            eMailToUpdate.Notes = "This was updated";
            await eMailList.SaveAsync();

            var updatedList = await dal.Fetch();
            var updatedEMailsList = await EMailECL.GetEMailECL(updatedList);
            
            Assert.Equal("This was updated",updatedEMailsList.First(a => a.Id == idToUpdate).Notes);
            Assert.Equal(countBeforeUpdate, updatedEMailsList.Count);
        }

        [Fact]
        private async void EMailECL_TestAddEMailEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailDal>();
            var childData = await dal.Fetch();

            var eMailList = await EMailECL.GetEMailECL(childData);
            var countBeforeAdd = eMailList.Count;
            
            var eMailToAdd = eMailList.AddNew();
            await BuildEMail(eMailToAdd); 

            var eMailEditList = await eMailList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, eMailEditList.Count);
        }

        private async Task BuildEMail(EMailEC eMail)
        {
            eMail.EMailAddress = "email@email.com";
            eMail.EMailType = await EMailTypeER.GetEMailTypeER(1);
            eMail.Notes = "document type notes";
            eMail.LastUpdatedBy = "edm";
            eMail.LastUpdatedDate = DateTime.Now;
        }
        
    }
}
