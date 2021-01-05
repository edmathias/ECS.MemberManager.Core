using System;
using System.IO;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ECS.MemberManager.Core.DataAccess.ADO
{
    public static class ADODb
    {
        private static IConfigurationRoot _config;
        private static SqlConnection _db = null;

        static ADODb()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var cnxnString = _config.GetConnectionString("LocalDbConnection");
            
            _db = new SqlConnection(cnxnString);
        }

        public static void BuildMemberManagerADODb()
        {
            _db.Execute(sbDbTearDown());

            _db.Execute(sqlDbBuild());
        }
        private static string sqlDbBuild()
        {
            var sb = new StringBuilder();
            
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[EMailTypes] ON;");
            sb.AppendLine("INSERT INTO [dbo].[EMailTypes]([Id], [Description], [Notes])");
            sb.AppendLine("VALUES(1,'work','work notes')");
            sb.AppendLine("INSERT INTO [dbo].[EMailTypes]([Id], [Description], [Notes])");
            sb.AppendLine("VALUES(2,'home','home notes')");
            sb.AppendLine("INSERT INTO [dbo].[EMailTypes]([Id], [Description], [Notes])");
            sb.AppendLine("VALUES(99,'delete','delete this')");
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[EMailTypes] OFF;");
            
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[EMails] ON;");
            sb.AppendLine("INSERT INTO [dbo].[EMails]([Id],[EMailTypeId],[EMailAddress],[LastUpdatedBy],[LastUpdatedDate], [Notes])");
            sb.AppendLine($"VALUES(1,1,'edm@ecs.com','edm','{DateTime.Now}','Notes')");
            sb.AppendLine("INSERT INTO [dbo].[EMails]([Id],[EMailTypeId],[EMailAddress],[LastUpdatedBy],[LastUpdatedDate], [Notes])");
            sb.AppendLine($"VALUES(2,2,'joeblow@ecs.com','mary','{DateTime.Now}','')");
            sb.AppendLine("INSERT INTO [dbo].[EMails]([Id],[EMailTypeId],[EMailAddress],[LastUpdatedBy],[LastUpdatedDate], [Notes])");
            sb.AppendLine($"VALUES(99,1,'deleteme@ecs.com','mary','{DateTime.Now}','test a delete')");
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[EMails] OFF;");

            return sb.ToString();
        }

        private static string sbDbTearDown()
        {
            var sb = new StringBuilder();
            sb.AppendLine("delete from AddressOrganization");
            sb.AppendLine("delete from CategoryOfOrganizationOrganization");
            sb.AppendLine("delete from CategoryOfPersonPerson");
            sb.AppendLine("delete from ContactForSponsors");
            sb.AppendLine("delete from EMailOrganization");
            sb.AppendLine("delete from EventDocuments");
            sb.AppendLine("delete from EventMembers");
            sb.AppendLine("delete from EventPerson");
            sb.AppendLine("delete from OrganizationPhone");
            sb.AppendLine("delete from Payments");
            sb.AppendLine("delete from PersonPhone");
            sb.AppendLine("delete from PersonalNotes");
            sb.AppendLine("delete from TaskForEvents");
            sb.AppendLine("delete from TermInOffices");
            sb.AppendLine("delete from Addresses");
            sb.AppendLine("delete from EMails");
            sb.AppendLine("delete from EMailTypes");
            sb.AppendLine("delete from DocumentTypes");
            sb.AppendLine("delete from MemberInfo");
            sb.AppendLine("delete from MemberStatuses");
            sb.AppendLine("delete from MembershipTypes");
            sb.AppendLine("delete from PaymentSources");
            sb.AppendLine("delete from PaymentTypes");
            sb.AppendLine("delete from Phones");
            sb.AppendLine("delete from PrivacyLevels");
            sb.AppendLine("delete from Sponsors");
            sb.AppendLine("delete from Organizations");
            sb.AppendLine("delete from OrganizationTypes");
            sb.AppendLine("delete from CategoryOfOrganizations");
            sb.AppendLine("delete from Events");
            sb.AppendLine("delete from Offices");
            sb.AppendLine("delete from Persons");
            sb.AppendLine("delete from Titles");
            sb.AppendLine("delete from CategoryOfPersons");

            return sb.ToString();
        } 
    }
}