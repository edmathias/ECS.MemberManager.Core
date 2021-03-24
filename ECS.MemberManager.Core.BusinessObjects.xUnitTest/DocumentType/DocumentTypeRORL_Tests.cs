using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class DocumentTypeRORL_Tests : CslaBaseTest
    {
        [Fact]
        private async void DocumentTypeRORL_TestGetDocumentTypeRORL()
        {
            var categoryOfPersonTypeInfoList = await DocumentTypeRORL.GetDocumentTypeRORL();

            Assert.NotNull(categoryOfPersonTypeInfoList);
            Assert.True(categoryOfPersonTypeInfoList.IsReadOnly);
            Assert.Equal(3, categoryOfPersonTypeInfoList.Count);
        }
    }
}