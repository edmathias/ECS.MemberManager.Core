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
    public class EMailTypeEditList_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public EMailTypeEditList_Tests()
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
        private async void EMailTypeEditList_TestNewEMailTypeList()
        {
            var eMailTypeErl = await EMailTypeEditList.NewEMailTypeEditList();

            Assert.NotNull(eMailTypeErl);
            Assert.IsType<EMailTypeEditList>(eMailTypeErl);
        }
        
        [Fact]
        private async void EMailTypeEditList_TestGetEMailTypeEditList()
        {
            var eMailTypeErl = await EMailTypeEditList.GetEMailTypeEditList();

            Assert.NotNull(eMailTypeErl);
            Assert.Equal(3, eMailTypeErl.Count);
        }
        
        [Fact]
        private async void EMailTypeEditList_TestDeleteEMailTypesEntry()
        {
            var eMailTypeErl = await EMailTypeEditList.GetEMailTypeEditList();
            var listCount = eMailTypeErl.Count;
            var eMailTypeToDelete = eMailTypeErl.First(et => et.Id == 99);

            // remove is deferred delete
            var isDeleted = eMailTypeErl.Remove(eMailTypeToDelete); 

            var eMailTypeListAfterDelete = await eMailTypeErl.SaveAsync();

            Assert.NotNull(eMailTypeListAfterDelete);
            Assert.IsType<EMailTypeEditList>(eMailTypeListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount,eMailTypeListAfterDelete.Count);
        }

        [Fact]
        private async void EMailTypeEditList_TestUpdateEMailTypesEntry()
        {
            const int idToUpdate = 1;
            
            var eMailTypeEditList = await EMailTypeEditList.GetEMailTypeEditList();
            var countBeforeUpdate = eMailTypeEditList.Count;
            var eMailTypeToUpdate = eMailTypeEditList.First(a => a.Id == idToUpdate);
            eMailTypeToUpdate.Description = "This was updated";

            var updatedList = await eMailTypeEditList.SaveAsync();
            
            Assert.Equal("This was updated",updatedList.First(a => a.Id == idToUpdate).Description);
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void EMailTypeEditList_TestAddEMailTypesEntry()
        {
            var eMailTypeEditList = await EMailTypeEditList.GetEMailTypeEditList();
            var countBeforeAdd = eMailTypeEditList.Count;
            
            var eMailTypeToAdd = eMailTypeEditList.AddNew();
            BuildEMailType(eMailTypeToAdd);

            var updatedEMailTypesList = await eMailTypeEditList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, updatedEMailTypesList.Count);
        }

        private void BuildEMailType(EMailTypeEdit eMailTypeToBuild)
        {
            eMailTypeToBuild.Notes = "member type notes";
            eMailTypeToBuild.Description = "member type";
        }
        
 
    }
}