using System.IO;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.EF;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class CslaBaseTest
    {
        private bool IsDatabaseBuilt = false;

        public CslaBaseTest()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var _config = builder.Build();
            var testLibrary = _config.GetValue<string>("TestLibrary");

            if (testLibrary == "Mock")
            {
                MockDb.ResetMockDb();
            }
            else if (testLibrary == "ADO")
            {
                if (!IsDatabaseBuilt)
                {
                    var adoDb = new ADODb();
                    adoDb.BuildMemberManager();
                }
            }
            else if (testLibrary == "EF")
            {
                if (!IsDatabaseBuilt)
                {
                    var efDb = new EFDb();
                    efDb.BuildMemberManager();
                }
            }

            IsDatabaseBuilt = true;
        }
    }
}