using System.IO;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PersonRORL_Tests : CslaBaseTest
    {
        [Fact]
        private async void PersonRORL_TestGetPersonRORL()
        {
            var personTypeInfoList = await PersonRORL.GetPersonRORL();

            Assert.NotNull(personTypeInfoList);
            Assert.True(personTypeInfoList.IsReadOnly);
            Assert.Equal(3, personTypeInfoList.Count);
        }
    }
}