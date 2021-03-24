using System;
using System.Linq;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EMailERL_Tests : CslaBaseTest
    {
        [Fact]
        private async void EMailERL_TestNewEMailList()
        {
            var eMailErl = await EMailERL.NewEMailERL();

            Assert.NotNull(eMailErl);
            Assert.IsType<EMailERL>(eMailErl);
        }

        [Fact]
        private async void EMailERL_TestGetEMailERL()
        {
            var eMailEditList = await EMailERL.GetEMailERL();

            Assert.NotNull(eMailEditList);
            Assert.Equal(3, eMailEditList.Count);
        }

        [Fact]
        private async void EMailERL_TestDeleteEMailsEntry()
        {
            var eMailErl = await EMailERL.GetEMailERL();
            var listCount = eMailErl.Count;
            var eMailToDelete = eMailErl.First(et => et.Id == 99);

            // remove is deferred delete
            var isDeleted = eMailErl.Remove(eMailToDelete);

            var eMailListAfterDelete = await eMailErl.SaveAsync();

            Assert.NotNull(eMailListAfterDelete);
            Assert.IsType<EMailERL>(eMailListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount, eMailListAfterDelete.Count);
        }

        [Fact]
        private async void EMailERL_TestUpdateEMailsEntry()
        {
            const int idToUpdate = 1;

            var eMailEditList = await EMailERL.GetEMailERL();
            var countBeforeUpdate = eMailEditList.Count;
            var eMailToUpdate = eMailEditList.First(a => a.Id == idToUpdate);
            eMailToUpdate.Notes = "This was updated";

            var updatedList = await eMailEditList.SaveAsync();

            Assert.Equal("This was updated", updatedList.First(a => a.Id == idToUpdate).Notes);
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void EMailERL_TestAddEMailsEntry()
        {
            var eMailEditList = await EMailERL.GetEMailERL();
            var countBeforeAdd = eMailEditList.Count;

            var eMailToAdd = eMailEditList.AddNew();
            eMailToAdd.EMailAddress = "email address to test";
            eMailToAdd.LastUpdatedBy = "edm";
            eMailToAdd.LastUpdatedDate = DateTime.Now;
            eMailToAdd.EMailType = await EMailTypeEC.GetEMailTypeEC(
                new EMailType()
                {
                    Id = 1,
                    Notes = "EMailType notes",
                    Description = "Email description"
                }
            );

            var updatedEMailsList = await eMailEditList.SaveAsync();

            Assert.NotEqual(countBeforeAdd, updatedEMailsList.Count);
        }
    }
}