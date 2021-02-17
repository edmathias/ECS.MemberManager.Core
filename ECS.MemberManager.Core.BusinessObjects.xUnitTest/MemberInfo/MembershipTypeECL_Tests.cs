using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MemberInfoECL_Tests 
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public MemberInfoECL_Tests()
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
        private async void MemberInfoECL_TestMemberInfoECL()
        {
            var memberInfoObjEdit = await MemberInfoECL.NewMemberInfoECL();

            Assert.NotNull(memberInfoObjEdit);
            Assert.IsType<MemberInfoECL>(memberInfoObjEdit);
        }

        
        [Fact]
        private async void MemberInfoECL_TestGetMemberInfoECL()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMemberInfoDal>();
            var childData = await dal.Fetch();
            
            var listToTest = await MemberInfoECL.GetMemberInfoECL(childData);
            
            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }
        
        [Fact]
        private async void MemberInfoECL_TestDeleteMemberInfoEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMemberInfoDal>();
            var childData = await dal.Fetch();
            
            var memberInfoObjEditList = await MemberInfoECL.GetMemberInfoECL(childData);

            var memberInfoObj = memberInfoObjEditList.First(a => a.Id == 99);

            // remove is deferred delete
            memberInfoObjEditList.Remove(memberInfoObj); 

            var memberInfoObjListAfterDelete = await memberInfoObjEditList.SaveAsync();
            
            Assert.NotEqual(childData.Count,memberInfoObjListAfterDelete.Count);
        }

        [Fact]
        private async void MemberInfoECL_TestUpdateMemberInfoEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMemberInfoDal>();
            var childData = await dal.Fetch();
            
            var memberInfoObjList = await MemberInfoECL.GetMemberInfoECL(childData);
            var countBeforeUpdate = memberInfoObjList.Count;
            var idToUpdate = memberInfoObjList.Min(a => a.Id);
            var memberInfoObjToUpdate = memberInfoObjList.First(a => a.Id == idToUpdate);

            memberInfoObjToUpdate.MemberNumber = "This was updated";
            await memberInfoObjList.SaveAsync();

            var updatedList = await dal.Fetch();
            var updatedMemberInfosList = await MemberInfoECL.GetMemberInfoECL(updatedList);
            
            Assert.Equal("This was updated",updatedMemberInfosList.First(a => a.Id == idToUpdate).MemberNumber);
            Assert.Equal(countBeforeUpdate, updatedMemberInfosList.Count);
        }

        [Fact]
        private async void MemberInfoECL_TestAddMemberInfoEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMemberInfoDal>();
            var childData = await dal.Fetch();

            var memberInfoObjList = await MemberInfoECL.GetMemberInfoECL(childData);
            var countBeforeAdd = memberInfoObjList.Count;

            var memberInfoObjToAdd = await MemberInfoEC.GetMemberInfoEC(await BuildMemberInfo());
            memberInfoObjList.Add(memberInfoObjToAdd);
            
            var memberInfoObjEditList = await memberInfoObjList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, memberInfoObjEditList.Count);
        }
        

        private async Task<MemberInfo> BuildMemberInfo()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPersonDal>();

            var memberInfo = new MemberInfo();
            memberInfo.Notes = "member info notes";
            memberInfo.MemberNumber = "9876543";
            memberInfo.Person = await dal.Fetch(1);
            var dal2 = dalManager.GetProvider<IMembershipTypeDal>();
            memberInfo.MembershipType = await dal2.Fetch(1);
            var dal3 = dalManager.GetProvider<IMemberStatusDal>();
            memberInfo.MemberStatus = await dal3.Fetch(1);
            var dal4 = dalManager.GetProvider<IPrivacyLevelDal>();
            memberInfo.PrivacyLevel = await dal4.Fetch(1);
            memberInfo.LastUpdatedBy = "edm";
            memberInfo.LastUpdatedDate = DateTime.Now	;

            return memberInfo;
        }
    }
}
