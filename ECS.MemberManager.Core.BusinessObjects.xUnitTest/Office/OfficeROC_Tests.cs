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
    public class OfficeROC_Tests 
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public OfficeROC_Tests()
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
        public async Task OfficeROC_TestGetOffice()
        {
            var childData = BuildOffice();
            var officeObj = await OfficeROC.GetOfficeROC(childData);

            Assert.NotNull(officeObj);
            Assert.IsType<OfficeROC>(officeObj);
            Assert.Equal(childData.Appointer,officeObj.Appointer);
            Assert.Equal(childData.Name,officeObj.Name);
            Assert.Equal(childData.Notes,officeObj.Notes);
            Assert.Equal(childData.Term,officeObj.Term);
            Assert.Equal(childData.CalendarPeriod,officeObj.CalendarPeriod);
            Assert.Equal(childData.ChosenHow,officeObj.ChosenHow);
            Assert.Equal(childData.LastUpdatedBy,officeObj.LastUpdatedBy);
            Assert.Equal(new SmartDate(childData.LastUpdatedDate),officeObj.LastUpdatedDate);
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
