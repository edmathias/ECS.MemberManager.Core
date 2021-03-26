using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EMailTypeROC_Tests : CslaBaseTest
    {
        [Fact]
        public async void EMailTypeROC_TestGetChild()
        {
            const int ID_VALUE = 999;

            var email = BuildEMailType();
            email.Id = ID_VALUE;

            var eMailType = await EMailTypeROC.GetEMailTypeROC(email);

            Assert.NotNull(eMailType);
            Assert.IsType<EMailTypeROC>(eMailType);
            Assert.Equal(eMailType.Id, email.Id);
            Assert.Equal(eMailType.Description, email.Description);
            Assert.Equal(eMailType.Notes, email.Notes);
            Assert.Equal(eMailType.RowVersion, email.RowVersion);
        }

        private EMailType BuildEMailType()
        {
            var eMailType = new EMailType();
            eMailType.Id = 1;
            eMailType.Description = "test description";
            eMailType.Notes = "notes for doctype";

            return eMailType;
        }
    }
}