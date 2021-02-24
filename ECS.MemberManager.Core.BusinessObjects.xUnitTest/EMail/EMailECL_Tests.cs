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
    public class EMailECL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public EMailECL_Tests()
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
        private async void EMailECL_TestEMailECL()
        {
            var eMailEdit = await EMailECL.NewEMailECL();

            Assert.NotNull(eMailEdit);
            Assert.IsType<EMailECL>(eMailEdit);
        }


        [Fact]
        private async void EMailECL_TestGetEMailECL()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailDal>();
            var childData = await dal.Fetch();

            var listToTest = await EMailECL.GetEMailECL(childData);

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }

        private async Task BuildEMail(EMailEC eMail)
        {
            eMail.EMailAddress = "email@email.com";
            eMail.EMailType = await EMailTypeEC.GetEMailTypeEC(
                new EMailType()
                {
                    Id = 1,
                    Notes = "EMailType notes",
                    Description = "Email description"
                }
            );
            eMail.Notes = "document type notes";
            eMail.LastUpdatedBy = "edm";
            eMail.LastUpdatedDate = DateTime.Now;
        }
    }
}