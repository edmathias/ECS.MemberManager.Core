using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class MemberStatusECL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public MemberStatusECL_Tests()
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
        private async void MemberStatusECL_TestMemberStatusECL()
        {
            var memberStatusEdit = await MemberStatusECL.NewMemberStatusECL();

            Assert.NotNull(memberStatusEdit);
            Assert.IsType<MemberStatusECL>(memberStatusEdit);
        }


        [Fact]
        private async void MemberStatusECL_TestGetMemberStatusECL()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMemberStatusDal>();
            var childData = await dal.Fetch();

            var listToTest = await MemberStatusECL.GetMemberStatusECL(childData);

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }


        private void BuildMemberStatus(MemberStatusEC memberStatus)
        {
            memberStatus.Description = "doc type description";
            memberStatus.Notes = "document type notes";
        }
    }
}