using System;
using System.ComponentModel;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class DocumentTypeROR_Tests
    {

        [Fact]
        public async void DocumentTypeROR_TestGetById()
        {
            var category = await DocumentTypeROR.GetDocumentTypeROR(1);
            
            Assert.NotNull(category);
            Assert.IsType<DocumentTypeROR>(category);
            Assert.Equal(1, category.Id);
        }
    }
}