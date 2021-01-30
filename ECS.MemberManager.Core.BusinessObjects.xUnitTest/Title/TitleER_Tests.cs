using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class TitleER_Tests 
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public TitleER_Tests()
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
        public async Task TitleER_TestGetTitle()
        {
            var title = await TitleER.GetTitleER(1);

            Assert.NotNull(title);
            Assert.IsType<TitleER>(title);
        }

        [Fact]
        public async Task TitleER_TestGetNewTitleER()
        {
            var title = await TitleER.NewTitleER();

            Assert.NotNull(title);
            Assert.False(title.IsValid);
        }

        [Fact]
        public async Task TitleER_TestUpdateExistingTitleER()
        {
            var newDescription = "This is an updated description";
            var title = await TitleER.GetTitleER(1);
            title.Description = newDescription; 
            
            var result =  await title.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal(newDescription,result.Description );
        }

        [Fact]
        public async Task TitleER_TestInsertNewTitleER()
        {
            var title = await TitleER.NewTitleER();
           
            BuildTitle(title);

            var savedTitle = await title.SaveAsync();
           
            Assert.NotNull(savedTitle);
            Assert.IsType<TitleER>(savedTitle);
            Assert.True( savedTitle.Id > 0 );
        }

        [Fact]
        public async Task TitleER_TestDeleteObjectFromDatabase()
        {
            const int ID_TO_DELETE = 99;
            
            await TitleER.DeleteTitleER(ID_TO_DELETE);

            await Assert.ThrowsAsync<DataPortalException>(() => TitleER.GetTitleER(ID_TO_DELETE));
        }
        
        // test invalid state 
        [Fact]
        public async Task TitleER_TestDescriptionRequired() 
        {
            var title = await TitleER.NewTitleER();
            title.Description = "make valid";
            var isObjectValidInit = title.IsValid;
            title.Description = string.Empty;

            Assert.NotNull(title);
            Assert.True(isObjectValidInit);
            Assert.False(title.IsValid);
        }
       
        [Fact]
        public async Task TitleER_TestDescriptionExceedsMaxLengthOf50()
        {
            var title = await TitleER.NewTitleER();
            title.Description = "valid length";
            Assert.True(title.IsValid);
            
            title.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "+
                                       "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis "+
                                       "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "+
                                       "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(title);
            Assert.False(title.IsValid);
            Assert.Equal("The field Description must be a string or array type with a maximum length of '50'.",
                title.BrokenRulesCollection[0].Description);
 
        }        
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task TitleER_TestInvalidSaveTitleER()
        {
            var title = await TitleER.NewTitleER();
            title.Description = String.Empty;
            TitleER savedTitle = null;
                
            Assert.False(title.IsValid);
            Assert.Throws<Csla.Rules.ValidationException>(() => savedTitle =  title.Save() );
        }
       
        private void BuildTitle(TitleER title)
        {
            title.Abbreviation = "abbr";
            title.Description = "test description";
            title.DisplayOrder = 1;
        }        
    }
}
