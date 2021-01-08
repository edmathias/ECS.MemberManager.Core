using System;
using System.IO;
using System.Linq;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Xunit;
using DalManager = ECS.MemberManager.Core.DataAccess.ADO.DalManager;

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
            var eMailEdit = await EMailEditList.NewEMailEditList();

            Assert.NotNull(eMailEdit);
            Assert.IsType<EMailEditList>(eMailEdit);
        }
        
        [Fact]
        private async void EMailEditList_TestGetEMailEditList()
        {
            var eMailEdit = await EMailEditList.GetEMailEditList();

            Assert.NotNull(eMailEdit);
            Assert.Equal(3, eMailEdit.Count);
        }
        
        [Fact]
        private async void EMailEditList_TestDeleteEMailsEntry()
        {
            var eMailEdit = await EMailEditList.GetEMailEditList();
            var listCount = eMailEdit.Count;
            var eMailToDelete = eMailEdit.First(et => et.Id == 99);

            // remove is deferred delete
            var isDeleted = eMailEdit.Remove(eMailToDelete); 

            var eMailListAfterDelete = await eMailEdit.SaveAsync();

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
            eMailToUpdate.Notes = "updated note";

            var updatedList = await eMailEditList.SaveAsync();
            
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void EMailEditList_TestAddEMailsEntry()
        {
            var eMailEditList = await EMailEditList.GetEMailEditList();
            var countBeforeAdd = eMailEditList.Count;
            
            var eMailToAdd = eMailEditList.AddNew();
            BuildEMail(eMailToAdd);

            var updatedEMailsList = await eMailEditList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, updatedEMailsList.Count);
        }

        private void BuildEMail(EMailEdit eMailToBuild)
        {
            eMailToBuild.Notes = "member type notes";
            eMailToBuild.EMailAddress = "edm@ecs.com";
            eMailToBuild.LastUpdatedBy = "edm";
            eMailToBuild.LastUpdatedDate = DateTime.Now;
            eMailToBuild.EMailTypeId = 1;
        }
        
 
    }
}