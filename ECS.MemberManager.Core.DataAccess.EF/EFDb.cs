using System;
using System.IO;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class EFDb
    {
        private IConfigurationRoot _config;
        private static SqlConnection _db = null;

        public EFDb()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var cnxnString = _config.GetConnectionString("LocalDbConnection");

            _db = new SqlConnection(cnxnString);
        }

        public void BuildMemberManagerEFDb()
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

            InsertOrganizationTypes();

            InsertOrganizations();

            InsertPersons();

            InsertSponsors();

            InsertDocumentTypes();

            InsertEvents();

            InsertContactForSponsors();

            InsertMembershipTypes();

            InsertMemberStatuses();

            InsertPrivacyLevels();

            InsertEventDocuments();

            InsertOffices();

            InsertPaymentSources();

            InsertPhones();

            InsertPaymentTypes();

            InsertTasksForEvents();

            InsertMemberInfo();

            InsertPayment();

            InsertTermInOffice();

            InsertEventMembers();

            InsertPersonalNotes();
        }

        private static void InsertPersonalNotes()
        {
            var sb = new StringBuilder();
            sb.AppendLine("SET IDENTITY_INSERT dbo.PersonalNotes ON;");
            sb.AppendLine(
                "INSERT INTO [dbo].[PersonalNotes]([Id], [PersonId], [Description], [StartDate], [DateEnd], [LastUpdatedBy], [LastUpdatedDate], [Note])");
            sb.AppendLine("SELECT 1, 1, N'description 1', '20200615 00:00:00.000', '20210715 00:00:00.000', N'edm', '20210101 00:00:00.000', N'Notes for personal notes 1' UNION ALL");
            sb.AppendLine("SELECT 2, 2, N'description 2', '20200905 00:00:00.000', '20210831 00:00:00.000', N'edm', '20210121 00:00:00.000', N'Notes for personal notes2' UNION ALL");
            sb.AppendLine("SELECT 99, 1, N'description to delete', '20200615 00:00:00.000', '20210715 00:00:00.000', N'edm', '20210101 00:00:00.000', N'delete this'");
            sb.AppendLine("SET IDENTITY_INSERT dbo.PersonalNotes OFF;");
            sb.AppendLine("DBCC CHECKIDENT ('PersonalNotes', RESEED, 2)");

            _db.Execute(sb.ToString());
        }

        private static void InsertEventMembers()
        {
            var sb = new StringBuilder();
            sb.AppendLine("SET IDENTITY_INSERT dbo.EventMembers ON;");
            sb.AppendLine(
                "INSERT INTO [dbo].[EventMembers]([Id], [MemberInfoId], [EventId], [Role], [LastUpdatedBy], [LastUpdatedDate], [Notes])");
            sb.AppendLine("SELECT 1, 1, 1, N'Organizer', N'edm', '20200403 00:00:00.000', N'notes for 1' UNION ALL");
            sb.AppendLine("SELECT 2, 2, 2, N'Helper', N'joeb', '20201201 00:00:00.000', N'notes for 2' UNION ALL");
            sb.AppendLine("SELECT 99, 1, 2, N'Delete this', N'edm', '20180101 00:00:00.000', N'delete this'");
            sb.AppendLine("SET IDENTITY_INSERT dbo.EventMembers OFF;");
            sb.AppendLine("DBCC CHECKIDENT ('EventMembers', RESEED, 2)");

            _db.Execute(sb.ToString());
        }

        private static void InsertTermInOffice()
        {
            var sb = new StringBuilder();
            sb.AppendLine("SET IDENTITY_INSERT dbo.TermInOffices ON;");
            sb.AppendLine(
                "INSERT INTO [dbo].[TermInOffices]([Id], [PersonId], [OfficeId], [StartDate], [LastUpdatedBy], [LastUpdatedDate], [Notes])");
            sb.AppendLine(
                "SELECT 1, 1, 1, '20210101 00:00:00.000', N'edm', '20210115 00:00:00.000', N'notes for 1' UNION ALL");
            sb.AppendLine(
                "SELECT 2, 2, 2, '20201030 00:00:00.000', N'joe', '20210101 00:00:00.000', N'notes for 2' UNION ALL");
            sb.AppendLine("SELECT 99, 2, 1, '20200101 00:00:00.000', N'edm', '20201231 00:00:00.000', N'delete this'");
            sb.AppendLine("SET IDENTITY_INSERT dbo.TermInOffices OFF;");
            sb.AppendLine("DBCC CHECKIDENT ('TermInOffices', RESEED, 2)");

            _db.Execute(sb.ToString());
        }

        private static void InsertPayment()
        {
            var sb = new StringBuilder();
            sb.AppendLine("SET IDENTITY_INSERT dbo.Payments ON;");
            sb.AppendLine(
                "INSERT INTO [dbo].[Payments]([Id], [PersonId], [Amount], [PaymentDate], [PaymentExpirationDate], [PaymentSourceId], [PaymentTypeId], [LastUpdatedBy], [LastUpdatedDate], [Notes])");
            sb.AppendLine(
                "SELECT 1, 1, 79, '20210101 00:00:00.000', '20220101 00:00:00.000', 1, 2, N'edm', '20210301 00:00:00.000', N'notes for payment 1' UNION ALL");
            sb.AppendLine(
                "SELECT 2, 2, 83.65, '20201231 00:00:00.000', '20211231 00:00:00.000', 2, 1, N'joe', '20210101 00:00:00.000', N'notes for 2' UNION ALL");
            sb.AppendLine(
                "SELECT 99, 99, 101.21, '20201015 00:00:00.000', '20211015 00:00:00.000', 2, 1, N'edm', '20210115 00:00:00.000', N'notes to delete'");
            sb.AppendLine("SET IDENTITY_INSERT dbo.Payments OFF;");
            sb.AppendLine("DBCC CHECKIDENT ('Payments', RESEED, 2)");

            _db.Execute(sb.ToString());
        }

        private static void InsertMemberInfo()
        {
            var sb = new StringBuilder();
            sb.AppendLine("SET IDENTITY_INSERT dbo.MemberInfo ON;");
            sb.AppendLine(
                "INSERT INTO [dbo].[MemberInfo]([Id], [PersonId], [MemberNumber], [DateFirstJoined], [PrivacyLevelId], [MemberStatusId], [MembershipTypeId], [LastUpdatedBy], [LastUpdatedDate], [Notes])");
            sb.AppendLine(
                "SELECT 1, 1, N'12345', '20200908 00:00:00.000', 1, 1, 1, N'edm', '20210101 00:00:00.000', N'notes for 1' UNION ALL");
            sb.AppendLine(
                "SELECT 2, 2, N'56789', '20200615 00:00:00.000', 2, 2, 2, N'edm', '20210201 00:00:00.000', N'notes for 2' UNION ALL");
            sb.AppendLine(
                "SELECT 99, 2, N'99999', '20190101 00:00:00.000', 1, 1, 1, N'edm', '20201001 00:00:00.000', N'delete this'");
            sb.AppendLine("SET IDENTITY_INSERT dbo.MemberInfo OFF;");
            sb.AppendLine("DBCC CHECKIDENT ('MemberInfo', RESEED, 2)");

            _db.Execute(sb.ToString());
        }

        private static void InsertTasksForEvents()
        {
            var sb = new StringBuilder();
            sb.AppendLine("SET IDENTITY_INSERT dbo.TaskForEvents ON;");
            sb.AppendLine(
                "INSERT INTO dbo.TaskForEvents(Id, EventId, TaskName, PlannedDate, ActualDate, Information, LastUpdatedBy, LastUpdatedDate, Notes)");
            sb.AppendLine(
                "SELECT 1, 1, N'Task Name 1', '20210331', '20210329', N'information line', N'edm', '20210214', N'notes for 1' UNION ALL");
            sb.AppendLine(
                "SELECT 2, 2, N'Task name 2', '20210415', '20210430', N'information line 2', N'edm', '20210101', N'notes for 2' UNION ALL");
            sb.AppendLine(
                "SELECT 99, 1, N'Task to delete', '20210930', '20200915', N'information to delete', N'edm', '20210115', N'delete this'");
            sb.AppendLine("SET IDENTITY_INSERT dbo.TaskForEvents OFF;");
            sb.AppendLine("DBCC CHECKIDENT ('TaskForEvents', RESEED, 2)");

            _db.Execute(sb.ToString());
        }

        private static void InsertPaymentTypes()
        {
            var sb = new StringBuilder();
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[PaymentTypes] ON;");
            sb.AppendLine("INSERT INTO [dbo].[PaymentTypes]([Id], [Description], [Notes])");
            sb.AppendLine("SELECT 1, N'Payment type #1', N'notes for #1' UNION ALL");
            sb.AppendLine("SELECT 2, N'Payment type #2', N'Notes for #2' UNION ALL");
            sb.AppendLine("SELECT 99, N'Payment type to delete', NULL");
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[PaymentTypes] OFF;");
            sb.AppendLine("DBCC CHECKIDENT ('PaymentTypes', RESEED, 2)");

            _db.Execute(sb.ToString());
        }

        private static void InsertPhones()
        {
            var sb = new StringBuilder();
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[Phones] ON;");
            sb.AppendLine(
                "INSERT INTO [dbo].[Phones]([Id], [PhoneType], [AreaCode], [Number], [Extension], [DisplayOrder], [LastUpdatedBy], [LastUpdatedDate], [Notes])");
            sb.AppendLine(
                "SELECT 1, N'mobile', N'303', N'333-2222', N'111', 0, N'edm', '20210101 00:00:00.000', N'notes for phone #1' UNION ALL");
            sb.AppendLine(
                "SELECT 2, N'work', N'216', N'256-8888', NULL, 0, N'edm', '20200630 00:00:00.000', NULL UNION ALL");
            sb.AppendLine(
                "SELECT 99, N'delete', N'414', N'421-7777', N'12', 1, N'joe', '20200915 00:00:00.000', N'delete this'");
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[Phones] OFF;");
            sb.AppendLine("DBCC CHECKIDENT ('Phones', RESEED, 2)");

            _db.Execute(sb.ToString());
        }

        private static void InsertPaymentSources()
        {
            var sb = new StringBuilder();
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[PaymentSources] ON;");
            sb.AppendLine("INSERT INTO [dbo].[PaymentSources]([Id], [Description], [Notes])");
            sb.AppendLine("SELECT 1, N'Payment source #1', N'notes for #1' UNION ALL");
            sb.AppendLine("SELECT 2, N'Payment source #2', N'Notes for #2' UNION ALL");
            sb.AppendLine("SELECT 99, N'Payment to delete', NULL");
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[PaymentSources] OFF;");
            sb.AppendLine("DBCC CHECKIDENT ('PaymentSources', RESEED, 2)");

            _db.Execute(sb.ToString());
        }

        private static void InsertOffices()
        {
            var sb = new StringBuilder();
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[Offices] ON;");
            sb.AppendLine(
                "INSERT INTO [dbo].[Offices]([Id], [Name], [Term], [CalendarPeriod], [ChosenHow], [Appointer], [LastUpdatedBy], [LastUpdatedDate], [Notes])");
            sb.AppendLine(
                "SELECT 1, N'Office #1', 1, N'Annual', 1, N'Joe Blow', N'edm', '20210119 00:00:00.000', N'notes for office #1' UNION ALL");
            sb.AppendLine(
                "SELECT 2, N'Office #2', 2, N'Decade', 2, N'Fred Derf', N'joe', '20200530 00:00:00.000', N'notes for office #2' UNION ALL");
            sb.AppendLine(
                "SELECT 99, N'Office to delete', 1, N'Annual', 1, N'mary', N'edm', '20200101 00:00:00.000', N'delete this office'");
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[Offices] OFF;");
            sb.AppendLine("DBCC CHECKIDENT ('Offices', RESEED, 2)");

            _db.Execute(sb.ToString());
        }

        private static void InsertEventDocuments()
        {
            var sb = new StringBuilder();
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[EventDocuments] ON;");
            sb.AppendLine(
                "INSERT INTO [dbo].[EventDocuments]([Id], [EventId], [DocumentName], [DocumentTypeId], [PathAndFileName], [LastUpdatedBy], [LastUpdatedDate], [Notes])");
            sb.AppendLine(
                "SELECT 1, 1, N'Event Document #1', 1, N'c:\\directory\\filename.doc', N'edm', '20210117 00:00:00.000', N'notes for #1' UNION ALL");
            sb.AppendLine(
                "SELECT 2, 2, N'Event document #2', 2, N'c:\\anotherdirectory\\test\\filename.doc', N'joe', '20210117 00:00:00.000', N'notes for 2' UNION ALL");
            sb.AppendLine(
                "SELECT 99, 1, N'Event doc to delete', 2, N'c:\\deleteme', N'edm', '20200630 00:00:00.000', N'notes to delete'");
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[EventDocuments] OFF;");
            sb.AppendLine("DBCC CHECKIDENT ('EventDocuments', RESEED, 2)");
            _db.Execute(sb.ToString());
        }

        private static void InsertPrivacyLevels()
        {
            var sb = new StringBuilder();
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[PrivacyLevels] ON;");
            sb.AppendLine("INSERT INTO [dbo].[PrivacyLevels]([Id], [Description], [Notes])");
            sb.AppendLine("SELECT 1, N'Privacy level #1', N'notes for #1' UNION ALL");
            sb.AppendLine("SELECT 2, N'Privacy level #2', N'Notes for #2' UNION ALL");
            sb.AppendLine("SELECT 99, N'Privacy level to delete', N'Notes to delete'");
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[PrivacyLevels] OFF;");
            sb.AppendLine("DBCC CHECKIDENT ('PrivacyLevels', RESEED, 2)");
            _db.Execute(sb.ToString());
        }

        private static void InsertMemberStatuses()
        {
            var sb = new StringBuilder();
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[MemberStatuses] ON;");

            sb.AppendLine("INSERT INTO [dbo].[MemberStatuses]([Id], [Description], [Notes])");
            sb.AppendLine("SELECT 1, N'Member status 1', N'notes for #1' UNION ALL");
            sb.AppendLine("SELECT 2, N'Member status 2', N'notes for #2' UNION ALL");
            sb.AppendLine("SELECT 99, N'Member status to delete', N'notes for delete'");
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[MemberStatuses] OFF;");
            sb.AppendLine("DBCC CHECKIDENT ('MembershipTypes', RESEED, 2)");

            _db.Execute(sb.ToString());
        }

        private static void InsertMembershipTypes()
        {
            var sb = new StringBuilder();
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[MembershipTypes] ON;");
            sb.AppendLine(
                "INSERT INTO ECSMemberManager.dbo.MembershipTypes (Id, Description, Level, LastUpdatedBy, LastUpdatedDate, Notes)");
            sb.AppendLine(
                "SELECT 1, N'Membership Type A', 1, N'edm', '20201231 00:00:00.000', N'notes for A' UNION ALL");
            sb.AppendLine(
                "SELECT 2, N'Membership Type B', 2, N'edm', '20210101 00:00:00.000', N'Notes for B' UNION ALL");
            sb.AppendLine(
                "SELECT 99, N'Membership to delete', 2, N'edm', '20210114 00:00:00.000', N'delete this'");
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[MembershipTypes] OFF;");
            sb.AppendLine("DBCC CHECKIDENT ('MembershipTypes', RESEED, 2)");

            _db.Execute(sb.ToString());
        }

        private static void InsertSponsors()
        {
            var sb = new StringBuilder();
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[Sponsors] ON;");
            sb.AppendLine(
                "INSERT INTO [dbo].[Sponsors]([Id], [PersonId], [OrganizationId], [Status], [DateOfFirstContact], [ReferredBy], [DateSponsorAccepted], [TypeName], [Details], [SponsorUntilDate], [Notes], [LastUpdatedBy], [LastUpdatedDate])");
            sb.AppendLine(
                "SELECT 1, 1, 1, N'Sponsor 1  status', '20200812 00:00:00.000', N'fred derf', '20201230 00:00:00.000', N'sponsor type name', N'details for 1', '20220101 00:00:00.000', N'Notes for 1', N'edm', '20201210 00:00:00.000' UNION ALL");
            sb.AppendLine(
                "SELECT 2, 2, 2, N'Sponsor 2 status', '20200918 00:00:00.000', N'joe blow', '20200922 00:00:00.000', N'sponsor 2 type', N'details for 2', '20210908 00:00:00.000', N'Notes for 2', N'joe', '20210112 00:00:00.000'");
            sb.AppendLine(
                "SELECT 99, 2, 2, N'Sponsor to delete', '20200918 00:00:00.000', N'joe blow', '20200922 00:00:00.000', N'delete', N'delete', '20210908 00:00:00.000', N'Notes for 2', N'joe', '20210112 00:00:00.000'");
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[Sponsors] OFF;");
            sb.AppendLine("DBCC CHECKIDENT ('Sponsors', RESEED, 2)");

            _db.Execute(sb.ToString());
        }

        private static void InsertOrganizations()
        {
            var sb = new StringBuilder();
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[Organizations] ON;");
            sb.AppendLine(
                "INSERT INTO [dbo].[Organizations]([Id], [Name], [OrganizationTypeId], [CategoryOfOrganizationId], [DateOfFirstContact], [LastUpdatedBy], [LastUpdatedDate], [Notes])");
            sb.AppendLine(
                "SELECT 1, N'Organization 1', 1, 1, '20200601 00:00:00.000', N'edm', '20210113 00:00:00.000', N'notes org 1' UNION ALL");
            sb.AppendLine(
                "SELECT 2, N'Organization 2', 2, 2, '20200719 00:00:00.000', N'joe', '20210114 00:00:00.000', N'notes org 2' UNION ALL");
            sb.AppendLine(
                "SELECT 99, N'Organization to delete', 2,1, '20200719 00:00:00.000', N'joe', '20210114 00:00:00.000', N'notes to delete'");
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[Organizations] OFF;");
            sb.AppendLine("DBCC CHECKIDENT ('Organizations', RESEED, 2)");

            _db.Execute(sb.ToString());
        }

        private static void InsertOrganizationTypes()
        {
            var sb = new StringBuilder();

            sb.AppendLine("SET IDENTITY_INSERT [dbo].[OrganizationTypes] ON;");
            sb.AppendLine(
                "INSERT INTO ECSMemberManager.dbo.OrganizationTypes (Id, CategoryOfOrganizationId, Name, Notes)");
            sb.AppendLine("VALUES (1, 1, N'Org Type 1', N'notes for org 1')");
            sb.AppendLine(
                "INSERT INTO ECSMemberManager.dbo.OrganizationTypes (Id, CategoryOfOrganizationId, Name, Notes)");
            sb.AppendLine("VALUES (2, 2, N'Org type 2', N'notes for org 2')");
            sb.AppendLine(
                "INSERT INTO ECSMemberManager.dbo.OrganizationTypes (Id, CategoryOfOrganizationId, Name, Notes)");
            sb.AppendLine("VALUES (99, 2, N'Org type to delete', N'notes to delete')");

            sb.AppendLine("SET IDENTITY_INSERT [dbo].[OrganizationTypes] OFF;");
            sb.AppendLine("DBCC CHECKIDENT ('OrganizationTypes', RESEED, 2)");

            _db.Execute(sb.ToString());
        }

        private static void InsertContactForSponsors()
        {
            var sb = new StringBuilder();
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[ContactForSponsors] ON;");

            sb.AppendLine(
                "INSERT INTO [dbo].[ContactForSponsors] ([Id], [SponsorId], [DateWhenContacted], [Purpose], ");
            sb.AppendLine("[RecordOfDiscussion], [PersonId], [Notes], [LastUpdatedBy], [LastUpdatedDate]  )");
            sb.AppendLine(
                "VALUES(1,1,'2020-6-30','purpose for contact','discussion record',1,'notes','edm','2020-1-1')");

            sb.AppendLine(
                "INSERT INTO [dbo].[ContactForSponsors] ([Id], [SponsorId], [DateWhenContacted], [Purpose], ");
            sb.AppendLine("[RecordOfDiscussion], [PersonId], [Notes], [LastUpdatedBy], [LastUpdatedDate]  )");
            sb.AppendLine(
                "VALUES(2,2,'2020-6-30','purpose for contact 2','discussion record 2',1,'notes 2','edm','2020-1-1')");

            sb.AppendLine(
                "INSERT INTO [dbo].[ContactForSponsors] ([Id], [SponsorId], [DateWhenContacted], [Purpose], ");
            sb.AppendLine("[RecordOfDiscussion], [PersonId], [Notes], [LastUpdatedBy], [LastUpdatedDate]  )");
            sb.AppendLine(
                "VALUES(99,2,'2020-6-30','delete for contact ','discussion record 2',1,'delete me','edm','2020-1-1')");

            sb.AppendLine("SET IDENTITY_INSERT [dbo].[ContactForSponsors] OFF;");
            sb.AppendLine("DBCC CHECKIDENT ('ContactForSponsors', RESEED, 2)");
            _db.Execute(sb.ToString());
        }

        private static void InsertEvents()
        {
            var sb = new StringBuilder();
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[Events] ON;");

            sb.AppendLine("INSERT INTO [dbo].[Events] ([Id],[EventName], [Description], [IsOneTime], [NextDate], ");
            sb.AppendLine(" [LastUpdatedBy], [LastUpdatedDate], [Notes] )");
            sb.AppendLine("VALUES(1,'Event 1','event description','True','2021-6-30','edm','2021-1-1','Notes for 1')");

            sb.AppendLine("INSERT INTO [dbo].[Events] ([Id],[EventName], [Description], [IsOneTime], [NextDate], ");
            sb.AppendLine(" [LastUpdatedBy], [LastUpdatedDate], [Notes] )");
            sb.AppendLine(
                "VALUES(2,'Event 2','event description 2','False','2021-3-30','edm','2021-1-1','Notes for 2')");

            sb.AppendLine("INSERT INTO [dbo].[Events] ([Id],[EventName], [Description], [IsOneTime], [NextDate], ");
            sb.AppendLine(" [LastUpdatedBy], [LastUpdatedDate], [Notes] )");
            sb.AppendLine(
                "VALUES(99,'Event to delete','delete event','False','2021-3-30','edm','2021-1-1','Notes for 2')");

            sb.AppendLine("SET IDENTITY_INSERT [dbo].[Events] OFF;");
            sb.AppendLine("DBCC CHECKIDENT ('Events', RESEED, 2)");
            _db.Execute(sb.ToString());
        }


        private static void InsertDocumentTypes()
        {
            var sb = new StringBuilder();
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[DocumentTypes] ON;");
            sb.AppendLine(
                "INSERT INTO [dbo].[DocumentTypes] ([Id],[Description],[LastUpdatedBy],[LastUpdatedDate],[Notes])");
            sb.AppendLine("VALUES(1,'Document Type 1','edm','2021-1-1','Notes for 1')");
            sb.AppendLine(
                "INSERT INTO [dbo].[DocumentTypes] ([Id],[Description],[LastUpdatedBy],[LastUpdatedDate],[Notes])");
            sb.AppendLine("VALUES(2,'Document Type 2','edm','2020-4-30','Notes for 2')");
            sb.AppendLine(
                "INSERT INTO [dbo].[DocumentTypes] ([Id],[Description],[LastUpdatedBy],[LastUpdatedDate],[Notes])");
            sb.AppendLine("VALUES(99,'Document to delete','edm','2021-1-1','delete this')");
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[DocumentTypes] OFF;");
            sb.AppendLine("DBCC CHECKIDENT ('DocumentTypes', RESEED, 2)");
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
            sb.AppendLine("DBCC CHECKIDENT ('Titles', RESEED, 2)");
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
            sb.AppendLine("DBCC CHECKIDENT ('Persons', RESEED, 2)");
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
            sb.AppendLine(
                $"VALUES(2,'5514 Locust Drive','','Kirtland','OH','44094','edm','some notes','{DateTime.Now}')");
            sb.AppendLine(
                "INSERT INTO Addresses(Id,Address1,Address2,City,State,PostCode,Notes,LastUpdatedBy,LastUpdatedDate)");
            sb.AppendLine(
                $"VALUES(99,'2221 Locust Drive','','Kirtland','OH','44094','edm','delete this','{DateTime.Now}')");
            sb.AppendLine("SET IDENTITY_INSERT [dbo].[Addresses] OFF;");
            sb.AppendLine("DBCC CHECKIDENT ('Addresses', RESEED, 2)");
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
//            sb.AppendLine("delete from CategoryOfOrganizationOrganization");
//            sb.AppendLine("delete from CategoryOfPersonPerson");
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
            sb.AppendLine("delete from MemberInfo");
            sb.AppendLine("delete from TermInOffices");
            sb.AppendLine("delete from MemberStatuses");
            sb.AppendLine("delete from PaymentSources");
            sb.AppendLine("delete from Phones");
            sb.AppendLine("delete from PrivacyLevels");
            sb.AppendLine("delete from Sponsors");
            sb.AppendLine("delete from Organizations");
            sb.AppendLine("delete from Events");
            sb.AppendLine("delete from Offices");
            sb.AppendLine("delete from Persons");
            sb.AppendLine("delete from EMails");
            sb.AppendLine("delete from Titles");
            sb.AppendLine("delete from CategoryOfPersons");
            sb.AppendLine("delete from Addresses");
            sb.AppendLine("delete from DocumentTypes");
            sb.AppendLine("delete from MembershipTypes");
            sb.AppendLine("delete from PaymentTypes");
            sb.AppendLine("delete from OrganizationTypes");
            sb.AppendLine("delete from CategoryOfOrganizations");
            sb.AppendLine("delete from EMailTypes");

            return sb.ToString();
        }
    }
}