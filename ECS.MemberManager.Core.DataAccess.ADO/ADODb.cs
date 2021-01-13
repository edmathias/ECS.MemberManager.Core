using System;
using System.IO;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ECS.MemberManager.Core.DataAccess.ADO
{
    public  class ADODb
    {
        private  IConfigurationRoot _config;
        private static SqlConnection _db = null;

        public ADODb()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var cnxnString = _config.GetConnectionString("LocalDbConnection");
            
            _db = new SqlConnection(cnxnString);
        }

        public void BuildMemberManagerADODb()
        {
            _db.Execute(sbDbTearDown());

            DbBuild();
        }
        private static void DbBuild()
        {
            
            InsertEMailType();

            InsertEMail();

            InsertTitles();
          
            InsertAddresses();

            InsertCategoryOfPersons();

            InsertCategoryOfOrganizations();

            InsertPersons();

            InsertDocumentTypes();

        }

        private static void InsertDocumentTypes()
        {
            var sb = new StringBuilder();
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[DocumentTypes] ON;");
            sb.AppendLine("INSERT INTO [dbo].[DocumentTypes] ([Id],[Description],[LastUpdatedBy],[LastUpdatedDate],[Notes])");
            sb.AppendLine("VALUES(1,'Document Type 1','edm','2021-1-1','Notes for 1')");
            sb.AppendLine("INSERT INTO [dbo].[DocumentTypes] ([Id],[Description],[LastUpdatedBy],[LastUpdatedDate],[Notes])");
            sb.AppendLine("VALUES(2,'Document Type 2','edm','2020-4-30','Notes for 2')");
            sb.AppendLine("INSERT INTO [dbo].[DocumentTypes] ([Id],[Description],[LastUpdatedBy],[LastUpdatedDate],[Notes])");
            sb.AppendLine("VALUES(99,'Document to delete','edm','2021-1-1','delete this')");
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[DocumentTypes] OFF;");
            _db.Execute(sb.ToString());
        }
        
        private static void InsertTitles()
        {
            var sb = new StringBuilder();
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[Titles] ON;");
            sb.AppendLine("INSERT INTO Titles(Id,Abbreviation,Description,DisplayOrder)");
            sb.AppendLine("VALUES(1,'Mr','Mister',0)");
            sb.AppendLine("INSERT INTO Titles(Id,Abbreviation,Description,DisplayOrder)");
            sb.AppendLine("VALUES(2,'Mrs','Missus',1)");
            sb.AppendLine("INSERT INTO Titles(Id,Abbreviation,Description,DisplayOrder)");
            sb.AppendLine("VALUES(99,'Mrs','Delete this',1)");
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[Titles] OFF;");
            _db.Execute(sb.ToString());
        }

        private static void InsertPersons()
        {
            var sb = new StringBuilder();
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[Persons] ON;");
            sb.AppendLine("INSERT INTO Persons(Id,TitleId,LastName,MiddleName,FirstName," +
                          "DateOfFirstContact,BirthDate,LastUpdatedBy,LastUpdatedDate,Code,Notes,EMailId)");
            sb.AppendLine("VALUES(1,1,'Blow','A','Joe','2020-10-01','1965-01-28','edm','2021-01-11',");
            sb.AppendLine("'code','notes for person',1)");
            sb.AppendLine("INSERT INTO Persons(Id,TitleId,LastName,MiddleName,FirstName," +
                          "DateOfFirstContact,BirthDate,LastUpdatedBy,LastUpdatedDate,Code,Notes,EMailId)");
            sb.AppendLine("VALUES(2, 2,'Derf','A','Fred','2020-10-01','1965-01-28','edm','2021-01-11',");
            sb.AppendLine("'code','notes for Fred derf',2)");
            sb.AppendLine("INSERT INTO Persons(Id,TitleId,LastName,MiddleName,FirstName," +
                          "DateOfFirstContact,BirthDate,LastUpdatedBy,LastUpdatedDate,Code,Notes,EMailId)");
            sb.AppendLine("VALUES(99,1,'Delete','','Me','2020-10-01','1965-01-28','edm','2021-01-11',");
            sb.AppendLine("'code','delete this record',2)");
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[Persons] OFF;");
            _db.Execute(sb.ToString());
        }

        private static void InsertCategoryOfPersons()
        {
            var sb = new StringBuilder();
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[CategoryOfPersons] ON;");
            sb.AppendLine("INSERT INTO CategoryOfPersons(Id,Category,DisplayOrder)");
            sb.AppendLine($"VALUES(1,'Person Category 1',0)");
            sb.AppendLine("INSERT INTO CategoryOfPersons(Id,Category,DisplayOrder)");
            sb.AppendLine($"VALUES(2,'Person Category 2',1)");
            sb.AppendLine("INSERT INTO CategoryOfPersons(Id,Category,DisplayOrder)");
            sb.AppendLine($"VALUES(99,'Person Category to delete',2)");
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[CategoryOfPersons] OFF;");
            sb.AppendLine("DBCC CHECKIDENT ('CategoryOfPersons', RESEED, 2)");
            _db.Execute(sb.ToString());
        }

        private static void InsertCategoryOfOrganizations()
        {
            var sb = new StringBuilder();
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[CategoryOfOrganizations] ON;");
            sb.AppendLine("INSERT INTO CategoryOfOrganizations(Id,Category,DisplayOrder)");
            sb.AppendLine($"VALUES(1,'Org Category 1',0)");
            sb.AppendLine("INSERT INTO CategoryOfOrganizations(Id,Category,DisplayOrder)");
            sb.AppendLine($"VALUES(2,'Org Category 2',1)");
            sb.AppendLine("INSERT INTO CategoryOfOrganizations(Id,Category,DisplayOrder)");
            sb.AppendLine($"VALUES(99,'Org to delete',2)");
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[CategoryOfOrganizations] OFF;");
            sb.AppendLine("DBCC CHECKIDENT ('CategoryOfOrganizations', RESEED, 2)");
            _db.Execute(sb.ToString());
        }

        private static void InsertAddresses()
        {
            var sb = new StringBuilder();
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[Addresses] ON;");
            sb.AppendLine(
                "INSERT INTO Addresses(Id,Address1,Address2,City,State,PostCode,Notes,LastUpdatedBy,LastUpdatedDate)");
            sb.AppendLine(
                $"VALUES(1,'8321 Oxford Drive','Apt 103','Greendale','WI','53129','edm','some notes','{DateTime.Now}')");
            sb.AppendLine(
                "INSERT INTO Addresses(Id,Address1,Address2,City,State,PostCode,Notes,LastUpdatedBy,LastUpdatedDate)");
            sb.AppendLine($"VALUES(99,'2221 Locust Drive','','Kirtland','OH','44094','edm','delete this','{DateTime.Now}')");
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[Addresses] OFF;");
            sb.AppendLine("DBCC CHECKIDENT ('Addresses', RESEED, 1)");
            _db.Execute(sb.ToString());
        }

        private static void InsertEMail()
        {
            var sb = new StringBuilder();
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[EMails] ON;");
            sb.AppendLine(
                "INSERT INTO [dbo].[EMails]([Id],[EMailTypeId],[EMailAddress],[LastUpdatedBy],[LastUpdatedDate], [Notes])");
            sb.AppendLine($"VALUES(1,1,'edm@ecs.com','edm','{DateTime.Now}','Notes')");
            sb.AppendLine(
                "INSERT INTO [dbo].[EMails]([Id],[EMailTypeId],[EMailAddress],[LastUpdatedBy],[LastUpdatedDate], [Notes])");
            sb.AppendLine($"VALUES(2,2,'joeblow@ecs.com','mary','{DateTime.Now}','')");
            sb.AppendLine(
                "INSERT INTO [dbo].[EMails]([Id],[EMailTypeId],[EMailAddress],[LastUpdatedBy],[LastUpdatedDate], [Notes])");
            sb.AppendLine($"VALUES(99,1,'deleteme@ecs.com','mary','{DateTime.Now}','test a delete')");
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[EMails] OFF;");
            sb.AppendLine("DBCC CHECKIDENT ('EMails', RESEED, 2)");
            _db.Execute(sb.ToString());
        }

        private static void InsertEMailType()
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
            sb.AppendLine("DBCC CHECKIDENT ('EMailTypes', RESEED, 2)");
            _db.Execute(sb.ToString());
        }

        private string sbDbTearDown()
        {
            var sb = new StringBuilder();
            sb.AppendLine("delete from AddressOrganization");
            sb.AppendLine("delete from CategoryOfOrganizationOrganization");
            sb.AppendLine("delete from CategoryOfPersonPerson");
            sb.AppendLine("delete from ContactForSponsors");
        //    sb.AppendLine("delete from EMailOrganization");
            sb.AppendLine("delete from EventDocuments");
            sb.AppendLine("delete from EventMembers");
            sb.AppendLine("delete from EventPerson");
            sb.AppendLine("delete from OrganizationPhone");
            sb.AppendLine("delete from Payments");
            sb.AppendLine("delete from PersonPhone");
            sb.AppendLine("delete from PersonalNotes");
            sb.AppendLine("delete from TaskForEvents");
            sb.AppendLine("delete from TermInOffices");
            sb.AppendLine("delete from EMailTypes");
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
            sb.AppendLine("delete from Addresses");
            sb.AppendLine("delete from EMails");
            sb.AppendLine("delete from DocumentTypes");
            sb.AppendLine("delete from MemberInfo");

            return sb.ToString();
        } 
    }
}