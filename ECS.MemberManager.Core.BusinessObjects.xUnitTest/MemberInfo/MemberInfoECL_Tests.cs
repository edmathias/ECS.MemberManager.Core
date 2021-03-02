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
