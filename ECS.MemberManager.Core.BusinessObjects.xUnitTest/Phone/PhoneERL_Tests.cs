using System;
using System.IO;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PhoneERL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public PhoneERL_Tests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var testLibrary = _config.GetValue<string>("TestLibrary");

            if (testLibrary == "Mock")
                MockDb.ResetMockDb();
            else
            {
                if (!IsDatabaseBuilt)
                {
                    var adoDb = new ADODb();
                    adoDb.BuildMemberManagerADODb();
                    IsDatabaseBuilt = true;
                }
            }
        }

        [Fact]
        private async void PhoneERL_TestNewPhoneERL()
        {
            var phoneEdit = await PhoneERL.NewPhoneERL();

            Assert.NotNull(phoneEdit);
            Assert.IsType<PhoneERL>(phoneEdit);
        }

        [Fact]
        private async void PhoneERL_TestGetPhoneERL()
        {
            var phoneEdit =
                await PhoneERL.GetPhoneERL();

            Assert.NotNull(phoneEdit);
            Assert.Equal(3, phoneEdit.Count);
        }

        [Fact]
        private async void PhoneERL_TestDeletePhoneERL()
        {
            const int ID_TO_DELETE = 99;
            var categoryList =
                await PhoneERL.GetPhoneERL();
            var listCount = categoryList.Count;
            var categoryToDelete = categoryList.First(cl => cl.Id == ID_TO_DELETE);
            // remove is deferred delete
            var isDeleted = categoryList.Remove(categoryToDelete);

            var phoneListAfterDelete = await categoryList.SaveAsync();

            Assert.NotNull(phoneListAfterDelete);
            Assert.IsType<PhoneERL>(phoneListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount, phoneListAfterDelete.Count);
        }

        [Fact]
        private async void PhoneERL_TestUpdatePhoneERL()
        {
            const int ID_TO_UPDATE = 1;
            const string NOTES_UPDATE = "Updated Notes";

            var categoryList =
                await PhoneERL.GetPhoneERL();
            var phoneToUpdate = categoryList.First(cl => cl.Id == ID_TO_UPDATE);
            phoneToUpdate.Notes = NOTES_UPDATE;

            var updatedList = await categoryList.SaveAsync();
            var updatedPhone = updatedList.First(el => el.Id == ID_TO_UPDATE);

            Assert.NotNull(updatedList);
            Assert.NotNull(updatedPhone);
            Assert.Equal(NOTES_UPDATE, updatedPhone.Notes);
        }

        [Fact]
        private async void PhoneERL_TestAddPhoneERL()
        {
            var categoryList =
                await PhoneERL.GetPhoneERL();
            var countBeforeAdd = categoryList.Count;

            var phoneToAdd = categoryList.AddNew();
            BuildPhone(phoneToAdd);

            var updatedCategoryList = await categoryList.SaveAsync();

            Assert.NotEqual(countBeforeAdd, updatedCategoryList.Count);
        }

        private void BuildPhone(PhoneEC phone)
        {
            phone.PhoneType = "mobile";
            phone.AreaCode = "303";
            phone.Number = "555-2368";
            phone.Extension = "123";
            phone.DisplayOrder = 1;
            phone.LastUpdatedBy = "Hank";
            phone.LastUpdatedDate = DateTime.Now;
            phone.Notes = "notes for phone";
        }
    }
}