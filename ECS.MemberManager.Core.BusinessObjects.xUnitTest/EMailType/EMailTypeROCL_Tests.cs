using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EMailTypeROCL_Tests : CslaBaseTest
    {
        [Fact]
        private async void EMailTypeInfoList_TestGetEMailTypeInfoList()
        {
            var childData = MockDb.EMailTypes;
            
            var eMailTypeInfoList = await EMailTypeROCL.GetEMailTypeROCL(childData);

            Assert.NotNull(eMailTypeInfoList);
            Assert.True(eMailTypeInfoList.IsReadOnly);
            Assert.Equal(3, eMailTypeInfoList.Count);
        }
    }
}