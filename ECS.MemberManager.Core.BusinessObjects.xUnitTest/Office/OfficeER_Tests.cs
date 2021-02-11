using System;
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
    public class OfficeER_Tests 
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public OfficeER_Tests()
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
        public async Task OfficeER_TestGetOffice()
        {
            var officeObj = await OfficeER.GetOfficeER(1);

            Assert.NotNull(officeObj);
            Assert.IsType<OfficeER>(officeObj);
        }

        [Fact]
        public async Task OfficeER_TestGetNewOfficeER()
        {
            var officeObj = await OfficeER.NewOfficeER();

            Assert.NotNull(officeObj);
            Assert.False(officeObj.IsValid);
        }

        [Fact]
        public async Task OfficeER_TestUpdateExistingOfficeER()
        {
            var officeObj = await OfficeER.GetOfficeER(1);
            officeObj.Notes = "These are updated Notes";
            
            var result =  await officeObj.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal("These are updated Notes",result.Notes );
        }

        [Fact]
        public async Task OfficeER_TestInsertNewOfficeER()
        {
            var officeObj = await OfficeER.NewOfficeER();
            officeObj.Term = 1;
            officeObj.CalendarPeriod = "annual";
            officeObj.Name = "office name";
            officeObj.ChosenHow = 2;
            officeObj.Appointer = "members";
            officeObj.LastUpdatedBy = "edm";
            officeObj.LastUpdatedDate = DateTime.Now;
            var savedOffice = await officeObj.SaveAsync();
           
            Assert.NotNull(savedOffice);
            Assert.IsType<OfficeER>(savedOffice);
            Assert.True( savedOffice.Id > 0 );
        }

        [Fact]
        public async Task OfficeER_TestDeleteObjectFromDatabase()
        {
            const int ID_TO_DELETE = 99;
            
            await OfficeER.DeleteOfficeER(ID_TO_DELETE);

            await Assert.ThrowsAsync<DataPortalException>(() => OfficeER.GetOfficeER(ID_TO_DELETE));
        }
        
        // test invalid state 
        [Fact]
        public async Task OfficeER_TestDescriptionRequired()
        {
            var officeObj = await OfficeER.NewOfficeER();
            officeObj.Term = 1;
            officeObj.CalendarPeriod = "annual";
            officeObj.Name = "office name";
            officeObj.ChosenHow = 2;
            officeObj.Appointer = "members";
            officeObj.LastUpdatedBy = "edm";
            officeObj.LastUpdatedDate = DateTime.Now;

            var isObjectValidInit = officeObj.IsValid;
            officeObj.Name = string.Empty;

            Assert.NotNull(officeObj);
            Assert.True(isObjectValidInit);
            Assert.False(officeObj.IsValid);
        }
       
        [Fact]
        public async Task OfficeER_TestDocumentNameExceedsMaxLengthOf50()
        {
            var officeObj = await OfficeER.NewOfficeER();
            officeObj.Term = 1;
            officeObj.CalendarPeriod = "annual";
            officeObj.Name = "office name";
            officeObj.ChosenHow = 2;
            officeObj.Appointer = "members";
            officeObj.LastUpdatedBy = "edm";
            officeObj.LastUpdatedDate = DateTime.Now;
            var isObjectValid = officeObj.IsValid;
            
            officeObj.Name = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor "+
                                     "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis "+
                                     "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "+
                                     "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(officeObj);
            Assert.True(isObjectValid);
            Assert.False(officeObj.IsValid);
            Assert.Equal("The field Name must be a string or array type with a maximum length of '50'.",
                officeObj.BrokenRulesCollection[0].Description);
        }        
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task OfficeER_TestInvalidSaveOfficeER()
        {
            var officeObj = await OfficeER.NewOfficeER();
            officeObj.Name = String.Empty;
            OfficeER savedOffice = null;
                
            Assert.False(officeObj.IsValid);
            Assert.Throws<Csla.Rules.ValidationException>(() => savedOffice =  officeObj.Save() );
        }
 
    
        [Fact]
        public async Task OfficeER_TestSaveOutOfOrder()
        {
            var office1 = await OfficeER.GetOfficeER(1);
            var office2 = await OfficeER.GetOfficeER(1);
            office1.Notes = "set up timestamp issue";  // turn on IsDirty
            office2.Notes = "set up timestamp issue";

            var office2_2 = await office2.SaveAsync();
            
            Assert.NotEqual(office2_2.RowVersion, office1.RowVersion);
            Assert.Equal("set up timestamp issue",office2_2.Notes);
            await Assert.ThrowsAsync<DataPortalException>(() => office1.SaveAsync());
        }

        [Fact]
        public async Task OfficeER_TestSubsequentSaves()
        {
            var office = await OfficeER.GetOfficeER(1);
            office.Notes = "set up timestamp issue";  // turn on IsDirty

            var office2 = await office.SaveAsync();
            var rowVersion1 = office2.RowVersion;
            office2.Notes = "another timestamp trigger";

            var office3 = await office2.SaveAsync();
            
            Assert.NotEqual(office2.RowVersion, office3.RowVersion);
        }
        
        [Fact]
        public async Task OfficeER_InvalidGet()
        {
            await Assert.ThrowsAsync<DataPortalException>(() => OfficeER.GetOfficeER(999));
        }
        
        private Office BuildOffice()
        {
            var officeObj = new Office();
            officeObj.Id = 1;
            officeObj.Term = 1;
            officeObj.CalendarPeriod = "annual";
            officeObj.Name = "office name";
            officeObj.ChosenHow = 2;
            officeObj.Appointer = "members";
            officeObj.LastUpdatedBy = "edm";
            officeObj.LastUpdatedDate = DateTime.Now;

            return officeObj;
        }        
    }
}
