using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class TitleROCL_Tests : CslaBaseTest
    {
        [Fact]
        private async void TitleInfoList_TestGetTitleInfoList()
        {
            var childData = MockDb.Titles;

            var titleTypeInfoList = await TitleROCL.GetTitleROCL(childData);

            Assert.NotNull(titleTypeInfoList);
            Assert.True(titleTypeInfoList.IsReadOnly);
            Assert.Equal(3, titleTypeInfoList.Count);
        }
    }
}