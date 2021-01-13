using System.ComponentModel;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class TitleInfo_Tests
    {
        [Fact]
        public async void TitleInfo_TestGetById()
        {
            var emailInfo = await TitleInfo.GetTitleInfo(1);
            
            Assert.NotNull(emailInfo);
            Assert.IsType<TitleInfo>(emailInfo);
            Assert.Equal(1, emailInfo.Id);
        }

        [Fact]
        public async void TitleInfo_TestGetChild()
        {
            const int ID_VALUE = 999;
            
            var emailType = new Title()
            {
                Id = ID_VALUE,
                Abbreviation = "Esq",
                DisplayOrder = 1,
                Description = "Test email type"
            };

            var emailTypeInfo = await TitleInfo.GetTitleInfo(emailType);
            
            Assert.NotNull(emailTypeInfo);
            Assert.IsType<TitleInfo>(emailTypeInfo);
            Assert.Equal(ID_VALUE, emailTypeInfo.Id);

        }
    }
}