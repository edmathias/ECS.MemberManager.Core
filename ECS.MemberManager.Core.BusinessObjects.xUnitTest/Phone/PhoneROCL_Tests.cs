using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PhoneROCL_Tests : CslaBaseTest
    {
        [Fact]
        private async void PhoneROCL_TestGetPhoneInfoList()
        {
            var childData = MockDb.Phones;

            var phoneInfoList = await PhoneROCL.GetPhoneROCL(childData);

            Assert.NotNull(phoneInfoList);
            Assert.True(phoneInfoList.IsReadOnly);
            Assert.Equal(3, phoneInfoList.Count);
        }
    }
}