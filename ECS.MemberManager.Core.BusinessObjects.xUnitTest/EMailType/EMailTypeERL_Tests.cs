﻿using System.IO;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EMailTypeERL_Tests : CslaBaseTest
    {
        [Fact]
        private async void EMailTypeERL_TestNewEMailTypeERL()
        {
            var categoryOfPersonEdit = await EMailTypeERL.NewEMailTypeERL();

            Assert.NotNull(categoryOfPersonEdit);
            Assert.IsType<EMailTypeERL>(categoryOfPersonEdit);
        }

        [Fact]
        private async void EMailTypeERL_TestGetEMailTypeERL()
        {
            var categoryOfPersonEdit =
                await EMailTypeERL.GetEMailTypeERL();

            Assert.NotNull(categoryOfPersonEdit);
            Assert.Equal(3, categoryOfPersonEdit.Count);
        }

        [Fact]
        private async void EMailTypeERL_TestDeleteEMailTypeERL()
        {
            const int ID_TO_DELETE = 99;
            var categoryList =
                await EMailTypeERL.GetEMailTypeERL();
            var listCount = categoryList.Count;
            var categoryToDelete = categoryList.First(cl => cl.Id == ID_TO_DELETE);
            // remove is deferred delete
            var isDeleted = categoryList.Remove(categoryToDelete);

            var categoryOfPersonListAfterDelete = await categoryList.SaveAsync();

            Assert.NotNull(categoryOfPersonListAfterDelete);
            Assert.IsType<EMailTypeERL>(categoryOfPersonListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount, categoryOfPersonListAfterDelete.Count);
        }

        [Fact]
        private async void EMailTypeERL_TestUpdateEMailTypeERL()
        {
            const int ID_TO_UPDATE = 1;
            const string NOTES_UPDATE = "Updated Notes";

            var categoryList =
                await EMailTypeERL.GetEMailTypeERL();
            var categoryOfPersonToUpdate = categoryList.First(cl => cl.Id == ID_TO_UPDATE);
            categoryOfPersonToUpdate.Notes = NOTES_UPDATE;

            var updatedList = await categoryList.SaveAsync();
            var updatedEMail = updatedList.First(el => el.Id == ID_TO_UPDATE);

            Assert.NotNull(updatedList);
            Assert.NotNull(updatedEMail);
            Assert.Equal(NOTES_UPDATE, updatedEMail.Notes);
        }

        [Fact]
        private async void EMailTypeERL_TestAddEMailTypeERL()
        {
            var categoryList =
                await EMailTypeERL.GetEMailTypeERL();
            var countBeforeAdd = categoryList.Count;

            var categoryOfPersonToAdd = categoryList.AddNew();
            BuildEMailType(categoryOfPersonToAdd);

            var updatedCategoryList = await categoryList.SaveAsync();

            Assert.NotEqual(countBeforeAdd, updatedCategoryList.Count);
        }

        private void BuildEMailType(EMailTypeEC categoryToBuild)
        {
            categoryToBuild.Description = "description for doctype";
            categoryToBuild.Notes = "notes for doctype";
        }
    }
}