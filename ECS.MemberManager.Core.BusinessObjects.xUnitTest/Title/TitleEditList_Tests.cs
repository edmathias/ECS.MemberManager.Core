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
    public class TitleEditList_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public TitleEditList_Tests()
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
        private async void TitleEditList_TestNewTitleList()
        {
            var titleEdit = await TitleEditList.NewTitleEditList();

            Assert.NotNull(titleEdit);
            Assert.IsType<TitleEditList>(titleEdit);
        }
        
        [Fact]
        private async void TitleEditList_TestGetTitleEditList()
        {
            var titleEdit = await TitleEditList.GetTitleEditList();

            Assert.NotNull(titleEdit);
            Assert.Equal(3, titleEdit.Count);
        }
        
        [Fact]
        private async void TitleEditList_TestDeleteTitlesEntry()
        {
            var titleEdit = await TitleEditList.GetTitleEditList();
            var listCount = titleEdit.Count;
            var titleToDelete = titleEdit.First(et => et.Id == 99);

            // remove is deferred delete
            var isDeleted = titleEdit.Remove(titleToDelete); 

            var titleListAfterDelete = await titleEdit.SaveAsync();

            Assert.NotNull(titleListAfterDelete);
            Assert.IsType<TitleEditList>(titleListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount,titleListAfterDelete.Count);
        }

        [Fact]
        private async void TitleEditList_TestUpdateTitlesEntry()
        {
            const int idToUpdate = 1;
            
            var titleEditList = await TitleEditList.GetTitleEditList();
            var countBeforeUpdate = titleEditList.Count;
            var titleToUpdate = titleEditList.First(a => a.Id == idToUpdate);
            titleToUpdate.Description = "This was updated";

            var updatedList = await titleEditList.SaveAsync();
            
            Assert.Equal("This was updated",updatedList.First(a => a.Id == idToUpdate).Description);
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void TitleEditList_TestAddTitlesEntry()
        {
            var titleEditList = await TitleEditList.GetTitleEditList();
            var countBeforeAdd = titleEditList.Count;
            
            var titleToAdd = titleEditList.AddNew();
            BuildTitle(titleToAdd);

            var updatedTitlesList = await titleEditList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, updatedTitlesList.Count);
        }

        private void BuildTitle(TitleEdit titleToBuild)
        {
            titleToBuild.Abbreviation = "Mr";
            titleToBuild.Description = "Mister";
            titleToBuild.DisplayOrder = 1;
        }
        
 
    }
}