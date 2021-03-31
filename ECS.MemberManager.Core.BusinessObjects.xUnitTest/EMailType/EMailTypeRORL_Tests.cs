using System.IO;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EMailTypeRORL_Tests : CslaBaseTest
    {
        [Fact]
        private async void EMailTypeRORL_TestGetEMailTypeRORL()
        {
            var categoryOfPersonTypeInfoList = await EMailTypeRORL.GetEMailTypeRORL();

            Assert.NotNull(categoryOfPersonTypeInfoList);
            Assert.True(categoryOfPersonTypeInfoList.IsReadOnly);
            Assert.Equal(3, categoryOfPersonTypeInfoList.Count);
        }
    }
}