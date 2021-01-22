using System;
using System.IO;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EMailEditList_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public EMailEditList_Tests()
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
        private async void EMailEditList_TestNewEMailList()
        {
            var eMailErl = await EMailEditList.NewEMailEditList();

            Assert.NotNull(eMailErl);
            Assert.IsType<EMailEditList>(eMailErl);
        }
        
        [Fact]
        private async void EMailEditList_TestGetEMailEditList()
        {
            var eMailEditList = await EMailEditList.GetEMailEditList();

            Assert.NotNull(eMailEditList);
            Assert.Equal(3, eMailEditList.Count);
        }
        
        [Fact]
        private async void EMailEditList_TestDeleteEMailsEntry()
        {
            var eMailErl = await EMailEditList.GetEMailEditList();
            var listCount = eMailErl.Count;
            var eMailToDelete = eMailErl.First(et => et.Id == 99);

            // remove is deferred delete
            var isDeleted = eMailErl.Remove(eMailToDelete); 

            var eMailListAfterDelete = await eMailErl.SaveAsync();

            Assert.NotNull(eMailListAfterDelete);
            Assert.IsType<EMailEditList>(eMailListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount,eMailListAfterDelete.Count);
        }

        [Fact]
        private async void EMailEditList_TestUpdateEMailsEntry()
        {
            const int idToUpdate = 1;
            
            var eMailEditList = await EMailEditList.GetEMailEditList();
            var countBeforeUpdate = eMailEditList.Count;
            var eMailToUpdate = eMailEditList.First(a => a.Id == idToUpdate);
            eMailToUpdate.Notes = "This was updated";

            var updatedList = await eMailEditList.SaveAsync();
            
            Assert.Equal("This was updated",updatedList.First(a => a.Id == idToUpdate).Notes);
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void EMailEditList_TestAddEMailsEntry()
        {
            var eMailEditList = await EMailEditList.GetEMailEditList();
            var countBeforeAdd = eMailEditList.Count;
            
            var eMailToAdd = eMailEditList.AddNew();
            eMailToAdd.EMailAddress = "email address to test";
            eMailToAdd.LastUpdatedBy = "edm";
            eMailToAdd.LastUpdatedDate = DateTime.Now;
            eMailToAdd.EMailType = await EMailTypeEdit.GetEMailTypeEdit(1);

            var updatedEMailsList = await eMailEditList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, updatedEMailsList.Count);
        }

       
 
    }
}