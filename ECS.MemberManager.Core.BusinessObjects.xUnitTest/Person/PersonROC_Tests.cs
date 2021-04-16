using System;
using System.IO;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PersonROC_Tests : CslaBaseTest
    {
        [Fact]
        public async Task TestPersonROC_Get()
        {
            var person = await PersonROC.GetPersonROC(BuildPerson());

            Assert.NotNull(person);
            Assert.IsType<PersonROC>(person);
            Assert.Equal(999, person.Id);
        }

        private Person BuildPerson()
        {
            var personToBuild = new Person();
            personToBuild.Id = 999;
            personToBuild.LastName = "lastname";
            personToBuild.MiddleName = "A";
            personToBuild.FirstName = "Joe";
            personToBuild.DateOfFirstContact = DateTime.Now;
            personToBuild.BirthDate = DateTime.Now;
            personToBuild.LastUpdatedDate = DateTime.Now;
            personToBuild.LastUpdatedBy = "edm";
            personToBuild.Code = "code";
            personToBuild.Notes = "Notes";
            personToBuild.EMail = new EMail() {Id = 1};
            personToBuild.Title = new Title() {Id = 1};

            return personToBuild;
        }
    }
}