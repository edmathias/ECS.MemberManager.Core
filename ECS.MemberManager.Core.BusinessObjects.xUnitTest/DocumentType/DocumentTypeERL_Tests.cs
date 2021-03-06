﻿using System;
using System.Linq;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class DocumentTypeERL_Tests : CslaBaseTest
    {
        [Fact]
        private async void DocumentTypeERL_TestNewDocumentTypeERL()
        {
            var categoryOfPersonEdit = await DocumentTypeERL.NewDocumentTypeERL();

            Assert.NotNull(categoryOfPersonEdit);
            Assert.IsType<DocumentTypeERL>(categoryOfPersonEdit);
        }

        [Fact]
        private async void DocumentTypeERL_TestGetDocumentTypeERL()
        {
            var categoryOfPersonEdit =
                await DocumentTypeERL.GetDocumentTypeERL();

            Assert.NotNull(categoryOfPersonEdit);
            Assert.Equal(3, categoryOfPersonEdit.Count);
        }

        [Fact]
        private async void DocumentTypeERL_TestDeleteDocumentTypeERL()
        {
            const int ID_TO_DELETE = 99;
            var categoryList =
                await DocumentTypeERL.GetDocumentTypeERL();
            var listCount = categoryList.Count;
            var categoryToDelete = categoryList.First(cl => cl.Id == ID_TO_DELETE);
            // remove is deferred delete
            var isDeleted = categoryList.Remove(categoryToDelete);

            var categoryOfPersonListAfterDelete = await categoryList.SaveAsync();

            Assert.NotNull(categoryOfPersonListAfterDelete);
            Assert.IsType<DocumentTypeERL>(categoryOfPersonListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount, categoryOfPersonListAfterDelete.Count);
        }

        [Fact]
        private async void DocumentTypeERL_TestUpdateDocumentTypeERL()
        {
            const int ID_TO_UPDATE = 1;

            var categoryList =
                await DocumentTypeERL.GetDocumentTypeERL();
            var countBeforeUpdate = categoryList.Count;
            var categoryOfPersonToUpdate = categoryList.First(cl => cl.Id == ID_TO_UPDATE);
            categoryOfPersonToUpdate.Notes = "Updated Notes";

            var updatedList = await categoryList.SaveAsync();

            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void DocumentTypeERL_TestAddDocumentTypeERL()
        {
            var categoryList =
                await DocumentTypeERL.GetDocumentTypeERL();
            var countBeforeAdd = categoryList.Count;

            var categoryOfPersonToAdd = categoryList.AddNew();
            BuildDocumentType(categoryOfPersonToAdd);

            var updatedCategoryList = await categoryList.SaveAsync();

            Assert.NotEqual(countBeforeAdd, updatedCategoryList.Count);
        }

        private void BuildDocumentType(DocumentTypeEC categoryToBuild)
        {
            categoryToBuild.Description = "description for doctype";
            categoryToBuild.LastUpdatedBy = "test";
            categoryToBuild.LastUpdatedDate = DateTime.Now;
            categoryToBuild.Notes = "notes for doctype";
        }
    }
}