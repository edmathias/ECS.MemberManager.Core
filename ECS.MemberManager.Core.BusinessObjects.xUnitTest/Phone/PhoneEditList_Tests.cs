using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Xunit;
using DalManager = ECS.MemberManager.Core.DataAccess.ADO.DalManager;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PhoneEditList_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public PhoneEditList_Tests()
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
        private async void PhoneEditList_TestNewPhoneList()
        {
            var phoneEdit = await PhoneEditList.NewPhoneEditList();

            Assert.NotNull(phoneEdit);
            Assert.IsType<PhoneEditList>(phoneEdit);
        }
        
        [Fact]
        private async void PhoneEditList_TestGetPhoneEditList()
        {
            var phoneEdit = await PhoneEditList.GetPhoneEditList();

            Assert.NotNull(phoneEdit);
            Assert.Equal(3, phoneEdit.Count);
        }
        
        [Fact]
        private async void PhoneEditList_TestDeletePhonesEntry()
        {
            var phoneEditList = await PhoneEditList.GetPhoneEditList();
            var listCount = phoneEditList.Count;
            var phoneToDelete = phoneEditList.First(et => et.Id == 99);

            // remove is deferred delete
            var isDeleted = phoneEditList.Remove(phoneToDelete); 

            var phoneListAfterDelete = await phoneEditList.SaveAsync();

            Assert.NotNull(phoneListAfterDelete);
            Assert.IsType<PhoneEditList>(phoneListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount,phoneListAfterDelete.Count);
        }

        [Fact]
        private async void PhoneEditList_TestUpdatePhonesEntry()
        {
            const int idToUpdate = 1;
            
            var phoneEditList = await PhoneEditList.GetPhoneEditList();
            var countBeforeUpdate = phoneEditList.Count;
            var phoneToUpdate = phoneEditList.First(a => a.Id == idToUpdate);
            phoneToUpdate.Notes = "This was updated";

            var updatedList = await phoneEditList.SaveAsync();
            
            Assert.Equal("This was updated",updatedList.First(a => a.Id == idToUpdate).Notes);
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void PhoneEditList_TestAddPhonesEntry()
        {
            var phoneEditList = await PhoneEditList.GetPhoneEditList();
            var countBeforeAdd = phoneEditList.Count;
            
            var phoneToAdd = phoneEditList.AddNew();
            BuildPhone(phoneToAdd);

            var updatedPhonesList = await phoneEditList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, updatedPhonesList.Count);
        }

        private void BuildPhone(PhoneEdit phone)
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