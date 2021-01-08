using System.ComponentModel;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EMailInfo_Tests
    {

        [Fact]
        public async void EMailInfo_TestGetById()
        {
            var emailInfo = await EMailInfo.GetEMailInfo(1);
            
            Assert.NotNull(emailInfo);
            Assert.IsType<EMailInfo>(emailInfo);
            Assert.Equal(1, emailInfo.Id);
        }

        [Fact]
        public async void EMailInfo_TestGetChild()
        {
            const int ID_VALUE = 999;
            
            var emailType = new EMail()
            {
                Id = ID_VALUE,
                Notes = "email type notes"
            };

            var emailTypeInfo = await EMailInfo.GetEMailInfo(emailType);
            
            Assert.NotNull(emailTypeInfo);
            Assert.IsType<EMailInfo>(emailTypeInfo);
            Assert.Equal(ID_VALUE, emailTypeInfo.Id);

        }
    }
}