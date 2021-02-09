using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MembershipTypeECL_Tests 
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public MembershipTypeECL_Tests()
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
        private async void MembershipTypeECL_TestMembershipTypeECL()
        {
            var eventObjEdit = await MembershipTypeECL.NewMembershipTypeECL();

            Assert.NotNull(eventObjEdit);
            Assert.IsType<MembershipTypeECL>(eventObjEdit);
        }

        
        [Fact]
        private async void MembershipTypeECL_TestGetMembershipTypeECL()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMembershipTypeDal>();
            var childData = await dal.Fetch();
            
            var listToTest = await MembershipTypeECL.GetMembershipTypeECL(childData);
            
            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }
        
        [Fact]
        private async void MembershipTypeECL_TestDeleteMembershipTypeEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMembershipTypeDal>();
            var childData = await dal.Fetch();
            
            var eventObjEditList = await MembershipTypeECL.GetMembershipTypeECL(childData);

            var eventObj = eventObjEditList.First(a => a.Id == 99);

            // remove is deferred delete
            eventObjEditList.Remove(eventObj); 

            var eventObjListAfterDelete = await eventObjEditList.SaveAsync();
            
            Assert.NotEqual(childData.Count,eventObjListAfterDelete.Count);
        }

        [Fact]
        private async void MembershipTypeECL_TestUpdateMembershipTypeEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMembershipTypeDal>();
            var childData = await dal.Fetch();
            
            var eventObjList = await MembershipTypeECL.GetMembershipTypeECL(childData);
            var countBeforeUpdate = eventObjList.Count;
            var idToUpdate = eventObjList.Min(a => a.Id);
            var eventObjToUpdate = eventObjList.First(a => a.Id == idToUpdate);

            eventObjToUpdate.Description = "This was updated";
            await eventObjList.SaveAsync();

            var updatedList = await dal.Fetch();
            var updatedMembershipTypesList = await MembershipTypeECL.GetMembershipTypeECL(updatedList);
            
            Assert.Equal("This was updated",updatedMembershipTypesList.First(a => a.Id == idToUpdate).Description);
            Assert.Equal(countBeforeUpdate, updatedMembershipTypesList.Count);
        }

        [Fact]
        private async void MembershipTypeECL_TestAddMembershipTypeEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMembershipTypeDal>();
            var childData = await dal.Fetch();

            var eventObjList = await MembershipTypeECL.GetMembershipTypeECL(childData);
            var countBeforeAdd = eventObjList.Count;
            
            var eventObjToAdd = eventObjList.AddNew();
            BuildMembershipType(eventObjToAdd); 

            var eventObjEditList = await eventObjList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, eventObjEditList.Count);
        }

        private void BuildMembershipType(MembershipTypeEC eventObj)
        {
            eventObj.Description = "event description";
            eventObj.Level = 1;
            eventObj.LastUpdatedBy = "edm";
            eventObj.LastUpdatedDate = DateTime.Now;
            eventObj.Notes = "event notes";
        }
        
    }
}
