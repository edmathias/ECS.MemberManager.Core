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
    public class EMailTypeECL_Tests 
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public EMailTypeECL_Tests()
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
        private async void EMailTypeECL_TestEMailTypeECL()
        {
            var eMailTypeEdit = await EMailTypeECL.NewEMailTypeECL();

            Assert.NotNull(eMailTypeEdit);
            Assert.IsType<EMailTypeECL>(eMailTypeEdit);
        }

        
        [Fact]
        private async void EMailTypeECL_TestGetEMailTypeECL()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailTypeDal>();
            var childData = await dal.Fetch();
            
            var listToTest = await EMailTypeECL.GetEMailTypeECL(childData);
            
            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }
        
        [Fact]
        private async void EMailTypeECL_TestDeleteEMailTypeEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailTypeDal>();
            var childData = await dal.Fetch();
            
            var eMailTypeEditList = await EMailTypeECL.GetEMailTypeECL(childData);

            var eMailType = eMailTypeEditList.First(a => a.Id == 99);

            // remove is deferred delete
            eMailTypeEditList.Remove(eMailType); 

            var eMailTypeListAfterDelete = await eMailTypeEditList.SaveAsync();
            
            Assert.NotEqual(childData.Count,eMailTypeListAfterDelete.Count);
        }

        [Fact]
        private async void EMailTypeECL_TestUpdateEMailTypeEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailTypeDal>();
            var childData = await dal.Fetch();
            
            var eMailTypeList = await EMailTypeECL.GetEMailTypeECL(childData);
            var countBeforeUpdate = eMailTypeList.Count;
            var idToUpdate = eMailTypeList.Min(a => a.Id);
            var eMailTypeToUpdate = eMailTypeList.First(a => a.Id == idToUpdate);

            eMailTypeToUpdate.Description = "This was updated";
            await eMailTypeList.SaveAsync();

            var updatedList = await dal.Fetch();
            var updatedEMailTypesList = await EMailTypeECL.GetEMailTypeECL(updatedList);
            
            Assert.Equal("This was updated",updatedEMailTypesList.First(a => a.Id == idToUpdate).Description);
            Assert.Equal(countBeforeUpdate, updatedEMailTypesList.Count);
        }

        [Fact]
        private async void EMailTypeECL_TestAddEMailTypeEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailTypeDal>();
            var childData = await dal.Fetch();

            var eMailTypeList = await EMailTypeECL.GetEMailTypeECL(childData);
            var countBeforeAdd = eMailTypeList.Count;
            
            var eMailTypeToAdd = eMailTypeList.AddNew();
            BuildEMailType(eMailTypeToAdd); 

            var eMailTypeEditList = await eMailTypeList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, eMailTypeEditList.Count);
        }

        private void BuildEMailType(EMailTypeEC eMailType)
        {
            eMailType.Description = "doc type description";
            eMailType.Notes = "document type notes";
        }
        
    }
}
