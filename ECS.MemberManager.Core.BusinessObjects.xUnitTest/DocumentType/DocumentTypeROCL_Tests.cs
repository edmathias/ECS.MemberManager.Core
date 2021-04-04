using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class DocumentTypeROCL_Tests : CslaBaseTest
    {
        [Fact]
        private async void DocumentTypeInfoList_TestGetDocumentTypeInfoList()
        {
            var childData = MockDb.DocumentTypes;
            var eMailTypeInfoList = await DocumentTypeROCL.GetDocumentTypeROCL(childData);

            Assert.NotNull(eMailTypeInfoList);
            Assert.True(eMailTypeInfoList.IsReadOnly);
            Assert.Equal(3, eMailTypeInfoList.Count);
        }
    }
}