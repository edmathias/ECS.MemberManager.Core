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
    public class TermInOfficeEC_Tests : CslaBaseTest
    {
        [Fact]
        public async Task TestTermInOfficeEC_NewTermInOfficeEC()
        {
            var termObj = await TermInOfficeEC.NewTermInOfficeEC();

            Assert.NotNull(termObj);
            Assert.IsType<TermInOfficeEC>(termObj);
            Assert.False(termObj.IsValid);
        }

        [Fact]
        public async Task TestTermInOfficeEC_GetTermInOfficeEC()
        {
            var termObjToLoad = BuildTermInOffice();
            var termObj = await TermInOfficeEC.GetTermInOfficeEC(termObjToLoad);

            Assert.NotNull(termObj);
            Assert.IsType<TermInOfficeEC>(termObj);
            Assert.Equal(termObjToLoad.Id, termObj.Id);
            Assert.Equal(termObjToLoad.Office.Id, termObj.Office.Id);
            Assert.Equal(termObjToLoad.Person.Id, termObj.Person.Id);
            Assert.Equal(new SmartDate(termObjToLoad.StartDate), termObj.StartDate);
            Assert.Equal(termObjToLoad.LastUpdatedBy, termObj.LastUpdatedBy);
            Assert.Equal(new SmartDate(termObjToLoad.LastUpdatedDate), termObj.LastUpdatedDate);
            Assert.Equal(termObjToLoad.Notes, termObj.Notes);
            Assert.Equal(termObjToLoad.RowVersion, termObj.RowVersion);
            Assert.True(termObj.IsValid);
        }

        [Fact]
        public async Task TestTermInOfficeEC_LastUpdatedByGreaterThan255Chars()
        {
            var termObjToTest = BuildTermInOffice();
            var termObj = await TermInOfficeEC.GetTermInOfficeEC(termObjToTest);
            var isObjectValidInit = termObj.IsValid;
            termObj.LastUpdatedBy = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                    "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                    "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                    "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.NotNull(termObj);
            Assert.True(isObjectValidInit);
            Assert.False(termObj.IsValid);
            Assert.Equal("LastUpdatedBy", termObj.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy can not exceed 255 characters", termObj.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestTermInOfficeEC_LastUpdatedByRequired()
        {
            var termObjToTest = BuildTermInOffice();
            var termObj = await TermInOfficeEC.GetTermInOfficeEC(termObjToTest);
            var isObjectValidInit = termObj.IsValid;
            termObj.LastUpdatedBy = string.Empty;

            Assert.NotNull(termObj);
            Assert.True(isObjectValidInit);
            Assert.False(termObj.IsValid);
            Assert.Equal("LastUpdatedBy", termObj.BrokenRulesCollection[0].Property);
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