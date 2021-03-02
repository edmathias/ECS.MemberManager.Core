using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PaymentRORL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public PaymentRORL_Tests()
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
        private async void PaymentRORL_TestGetPaymentRORL()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPaymentDal>();
            var data = await dal.Fetch();

            var organizationList = await PaymentRORL.GetPaymentRORL();
           
            Assert.NotNull(organizationList);
            Assert.True(organizationList.IsReadOnly);
            Assert.Equal(3, organizationList.Count);
        }
    }
}