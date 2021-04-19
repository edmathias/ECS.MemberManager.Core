using System;
using System.IO;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PersonECL_Tests : CslaBaseTest
    {
        [Fact]
        private async void PersonECL_TestPersonECL()
        {
            var personEdit = await PersonECL.NewPersonECL();

            Assert.NotNull(personEdit);
            Assert.IsType<PersonECL>(personEdit);
        }


        [Fact]
        private async void PersonECL_TestGetPersonECL()
        {
            var childData = MockDb.Persons;

            var listToTest = await PersonECL.GetPersonECL(childData);

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }

        private async Task BuildPersonEC(PersonEC personToBuild)
        {
            personToBuild.LastName = "lastname";
            personToBuild.MiddleName = "A";
            personToBuild.FirstName = "Joe";
            personToBuild.DateOfFirstContact = DateTime.Now;
            personToBuild.BirthDate = DateTime.Now;
            personToBuild.LastUpdatedDate = DateTime.Now;
            personToBuild.LastUpdatedBy = "edm";
            personToBuild.Code = "code";
            personToBuild.Notes = "Notes";
            personToBuild.EMail = await EMailEC.GetEMailEC(new EMail() {Id = 1});
            personToBuild.Title = await TitleEC.GetTitleEC(new Title() {Id = 1});
        }
    }
}