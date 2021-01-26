using System;
using System.IO;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PhoneEditChildList_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public PhoneEditChildList_Tests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var testLibrary = _config.GetValue<string>("TestLibrary");
            
            if(testLibrary == "Mock")
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
        private async void PhoneEditChildList_TestNewPhoneList()
        {
            var phoneEdit = await PhoneEditChildList.NewPhoneEditChildList();

            Assert.NotNull(phoneEdit);
            Assert.IsType<PhoneEditChildList>(phoneEdit);
        }
        
        [Fact]
        private async void PhoneEditChildList_TestGetPhoneEditChildList()
        {
            var phoneEdit = await PhoneEditChildList.GetPhoneEditChildList();

            Assert.NotNull(phoneEdit);
            Assert.Equal(3, phoneEdit.Count);
        }
        
        [Fact]
        private async void PhoneEditChildList_TestDeletePhonesEntry()
        {
            const int ID_TO_DELETE = 99;
            var phoneEditList = await PhoneEditChildList.GetPhoneEditChildList();
            var listCount = phoneEditList.Count;
            
            var phoneToDelete  = phoneEditList.First(pel => pel.Id == ID_TO_DELETE);

            // remove is deferred delete
            var isDeleted = phoneEditList.Remove(phoneToDelete); 

            var phoneListAfterDelete = await phoneEditList.SaveAsync();

            Assert.NotNull(phoneListAfterDelete);
            Assert.IsType<PhoneEditChildList>(phoneListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount,phoneListAfterDelete.Count);
        }

        [Fact]
        private async void PhoneEditChildList_TestUpdatePhonesEntry()
        {
            const int idToUpdate = 1;
            
            var phoneEditList = await PhoneEditChildList.GetPhoneEditChildList();
            var countBeforeUpdate = phoneEditList.Count;
            var phoneToUpdate = phoneEditList.First(a => a.Id == idToUpdate);
            phoneToUpdate.Notes = "This was updated";

            var updatedList = await phoneEditList.SaveAsync();
            
            Assert.Equal("This was updated",updatedList.First(a => a.Id == idToUpdate).Notes);
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void PhoneEditChildList_TestAddPhonesEntry()
        {
            var phoneEditList = await PhoneEditChildList.GetPhoneEditChildList();
            var countBeforeAdd = phoneEditList.Count;
            
            var phoneToAdd = phoneEditList.AddNew();
            BuildPhone(phoneToAdd);

            var updatedPhonesList = await phoneEditList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, updatedPhonesList.Count);
        }

        private void BuildPhone(PhoneEditChild phone)
        {
            phone.AreaCode = "303";
            phone.Extension = "111";
            phone.Number = "555-2368";
            phone.PhoneType = "mobile";
            phone.DisplayOrder = 1;
            phone.LastUpdatedBy = "edm";
            phone.LastUpdatedDate = DateTime.Now;
            phone.Notes = "This person is on standby";
        }
    }
}