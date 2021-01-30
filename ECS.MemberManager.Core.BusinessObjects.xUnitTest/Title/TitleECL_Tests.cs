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
    public class TitleECL_Tests 
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public TitleECL_Tests()
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
        private async void TitleECL_TestTitleECL()
        {
            var titleEdit = await TitleECL.NewTitleECL();

            Assert.NotNull(titleEdit);
            Assert.IsType<TitleECL>(titleEdit);
        }
        
        [Fact]
        private async void TitleECL_TestGetTitleECL()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITitleDal>();
            var childData = await dal.Fetch();
            
            var listToTest = await TitleECL.GetTitleECL(childData);
            
            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }
        
        [Fact]
        private async void TitleECL_TestDeleteTitleEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITitleDal>();
            var childData = await dal.Fetch();
            
            var titleEditList = await TitleECL.GetTitleECL(childData);

            var title = titleEditList.First(a => a.Id == 99);

            // remove is deferred delete
            titleEditList.Remove(title); 

            var titleListAfterDelete = await titleEditList.SaveAsync();
            
            Assert.NotEqual(childData.Count,titleListAfterDelete.Count);
        }

        [Fact]
        private async void TitleECL_TestUpdateTitleEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITitleDal>();
            var childData = await dal.Fetch();
            
            var titleList = await TitleECL.GetTitleECL(childData);
            var countBeforeUpdate = titleList.Count;
            var idToUpdate = titleList.Min(a => a.Id);
            var titleToUpdate = titleList.First(a => a.Id == idToUpdate);

            titleToUpdate.Description = "This was updated";
            await titleList.SaveAsync();

            var updatedList = await dal.Fetch();
            var updatedTitlesList = await TitleECL.GetTitleECL(updatedList);
            
            Assert.Equal("This was updated",updatedTitlesList.First(a => a.Id == idToUpdate).Description);
            Assert.Equal(countBeforeUpdate, updatedTitlesList.Count);
        }

        [Fact]
        private async void TitleECL_TestAddTitleEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITitleDal>();
            var childData = await dal.Fetch();

            var titleList = await TitleECL.GetTitleECL(childData);
            var countBeforeAdd = titleList.Count;
            
            var titleToAdd = titleList.AddNew();
            BuildTitle(titleToAdd); 

            var titleEditList = await titleList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, titleEditList.Count);
        }

        private void BuildTitle(TitleEC title)
        {
            title.Abbreviation = "abbr";
            title.Description = "test description";
            title.DisplayOrder = 1;
        }
        
    }
}
