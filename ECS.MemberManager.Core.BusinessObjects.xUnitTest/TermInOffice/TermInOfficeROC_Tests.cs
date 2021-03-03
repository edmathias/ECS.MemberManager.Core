using System;
using System.IO;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class TermInOfficeROC_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public TermInOfficeROC_Tests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var testLibrary = _config.GetValue<string>("TestLibrary");

            if (testLibrary == "Mock")
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
        public async Task TestTermInOfficeROC_GetTermInOfficeROC()
        {
            var termObjToLoad = BuildTermInOffice();
            var termObj = await TermInOfficeROC.GetTermInOfficeROC(termObjToLoad);

            Assert.NotNull(termObj);
            Assert.IsType<TermInOfficeROC>(termObj);
            Assert.Equal(termObjToLoad.Id,termObj.Id);
            Assert.Equal(termObjToLoad.Office.Id, termObj.Office.Id);
            Assert.Equal(termObjToLoad.Person.Id, termObj.Person.Id);
            Assert.Equal(new SmartDate(termObjToLoad.StartDate),termObj.StartDate);
            Assert.Equal(termObjToLoad.LastUpdatedBy, termObj.LastUpdatedBy);
            Assert.Equal(new SmartDate(termObjToLoad.LastUpdatedDate), termObj.LastUpdatedDate);
            Assert.Equal(termObjToLoad.Notes, termObj.Notes);
            Assert.Equal(termObjToLoad.RowVersion, termObj.RowVersion);
        }

        private TermInOffice BuildTermInOffice()
        {
            var termObj = new TermInOffice();
            termObj.Id = 1;
            termObj.Office = new Office() {Id = 1};
            termObj.Person = new Person() {Id = 1};
            termObj.StartDate = DateTime.Now;
            termObj.LastUpdatedBy = "edm";
            termObj.LastUpdatedDate = DateTime.Now;
            termObj.Notes = "notes for doctype";

            return termObj;
        }        
    }
}