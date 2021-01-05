using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using Dapper;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EMailER_Tests 
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public EMailER_Tests()
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
                    ADODb.BuildMemberManagerADODb();
                    IsDatabaseBuilt = true;
                }
            }
        }
      
        [Fact]
        public async Task TestEMailER_GetEMail()
        {
            var eMail = await EMailER.GetEMail(1);

            Assert.Equal(1,eMail.Id);
            Assert.NotNull(eMail.EMailType);
            Assert.True(eMail.IsValid);
        }

        
        [Fact]
        public async Task TestEMailER_New()
        {
            var eMail = await EMailER.NewEMail();

            Assert.NotNull(eMail);
            Assert.False(eMail.IsValid);
        }

        [Fact]
        public async Task TestEMailER_Update()
        {
            var eMail = await EMailER.GetEMail(1);
            eMail.Notes = "These are updated Notes";
            
            var result = eMail.Save();

            Assert.NotNull(result);
            Assert.Equal("These are updated Notes",result.Notes );
        }

        [Fact]
        public async Task TestEMailER_Insert()
        {
            var eMail = await EMailER.NewEMail();
            eMail.EMailAddress = "billg@microsoft.com";
            eMail.LastUpdatedBy = "edm";
            eMail.LastUpdatedDate = DateTime.Now;
            eMail.Notes = "This person is on standby";
            var eMailTypeDto = new EMailType()
            {
                Id = 4,
                Description = "secondary email",
                Notes = "nothing to see here"
            };
            eMail.EMailType = await DataPortal.FetchChildAsync<EMailTypeEC>(eMailTypeDto);
            var savedEMail = await eMail.SaveAsync();
           
            Assert.NotNull(savedEMail);
            Assert.IsType<EMailER>(savedEMail);
            Assert.True( savedEMail.Id > 0 );
        }

        [Fact]
        public async Task TestEMailER_Delete()
        {
            var emailToDelete = await EMailER.GetEMail(99);
            
            await EMailER.DeleteEMail(emailToDelete.Id);
            
            var emailTypeToCheck = await Assert.ThrowsAsync<Csla.DataPortalException>
                (() => EMailER.GetEMail(emailToDelete.Id));        
        }
        
        // test invalid state 
        [Fact]
        public async Task TestEMailER_EMailAddressRequired()
        {
            var eMail = await EMailER.NewEMail();
            eMail.EMailAddress = "person@emailaddress.com";
            eMail.EMailType = await DataPortal.CreateChildAsync<EMailTypeEC>();
            eMail.EMailType.Description = "EmailType description";
            eMail.LastUpdatedDate = DateTime.Now;
            eMail.LastUpdatedBy = "edm";
            var isObjectValidInit = eMail.IsValid;
            eMail.EMailAddress = string.Empty;

            Assert.NotNull(eMail);
            Assert.True(isObjectValidInit);
            Assert.False(eMail.IsValid);
 
        }
    
        // test invalid state 
        [Fact]
        public async Task TestEMailER_EMailTypeRequired()
        {
            var eMail = await EMailER.NewEMail();
            eMail.EMailAddress = "person@emailadddress.com";
            eMail.LastUpdatedDate = DateTime.Now;
            eMail.LastUpdatedBy = "edm";
            eMail.EMailType = await DataPortal.CreateChildAsync<EMailTypeEC>();
            eMail.EMailType.Description = "EMailType description";
            var isObjectValidInit = eMail.IsValid;
            eMail.EMailType = null;

            Assert.NotNull(eMail);
            Assert.True(isObjectValidInit);
            Assert.False(eMail.IsValid);
            Assert.Equal("The EMailType field is required.",eMail.GetBrokenRules()[0].Description);
 
        }
        
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task TestEMailER_TestInvalidSave()
        {
            var eMail = await EMailER.NewEMail();
            eMail.EMailAddress = String.Empty;
            EMailER savedEMail = null;
            
            Assert.False(eMail.IsValid);
            Assert.Throws<ValidationException>(() => savedEMail =  eMail.Save() );
        }

        
    }
}
