using System.IO;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class TitleRORL_Tests : CslaBaseTest
    {
        [Fact]
        private async void TitleRORL_TestGetTitleRORL()
        {
            var TitleTypeInfoList = await TitleRORL.GetTitleRORL();

            Assert.NotNull(TitleTypeInfoList);
            Assert.True(TitleTypeInfoList.IsReadOnly);
            Assert.Equal(3, TitleTypeInfoList.Count);
        }
    }
}