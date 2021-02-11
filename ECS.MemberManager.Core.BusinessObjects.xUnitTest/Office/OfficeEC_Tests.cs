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
    public class OfficeEC_Tests 
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public OfficeEC_Tests()
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
        public async Task OfficeEC_TestGetOffice()
        {
            var childData = BuildOffice();
            var officeObj = await OfficeEC.GetOfficeEC(childData);

            Assert.NotNull(officeObj);
            Assert.IsType<OfficeEC>(officeObj);
            Assert.Equal(childData.Appointer,officeObj.Appointer);
            Assert.Equal(childData.Name,officeObj.Name);
            Assert.Equal(childData.Notes,officeObj.Notes);
            Assert.Equal(childData.Term,officeObj.Term);
            Assert.Equal(childData.CalendarPeriod,officeObj.CalendarPeriod);
            Assert.Equal(childData.ChosenHow,officeObj.ChosenHow);
            Assert.Equal(childData.LastUpdatedBy,officeObj.LastUpdatedBy);
            Assert.Equal(new SmartDate(childData.LastUpdatedDate),officeObj.LastUpdatedDate);
        }

        [Fact]
        public async Task OfficeEC_TestGetNewOfficeEC()
        {
            var officeObj = await OfficeEC.NewOfficeEC();

            Assert.NotNull(officeObj);
            Assert.False(officeObj.IsValid);
        }

        // test invalid state 
        [Fact]
        public async Task OfficeEC_TestDescriptionRequired()
        {
            var officeObj = await OfficeEC.NewOfficeEC();
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
        public async Task OfficeEC_TestDocumentNameExceedsMaxLengthOf50()
        {
            var officeObj = await OfficeEC.NewOfficeEC();
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
            officeObj.Notes = "notes";
            return officeObj;
        }        
    }
}
