using System;
using System.Collections.Generic;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class MockDb
    {
        public static IList<Address> Addresses { get; }
        public static IList<CategoryOfOrganization> CategoryOfOrganizations { get; private set; }
        public static IList<CategoryOfPerson> CategoryOfPersons { get; private set; }
        public static IList<ContactForSponsor> ContactForSponsors { get; private set; }
        public static IList<DocumentType> DocumentTypes { get; private set; }
        public static IList<EMail> EMails { get; private set; }
        public static IList<EMailType> EMailTypes { get; private set; }
        public static IList<Event> Events { get; private set; }
        public static IList<EventDocument> EventDocuments { get; private set; }
        public static IList<EventMember> EventMembers { get; private set; }
        public static IList<MemberInfo> MemberInfos { get; private set; }
        public static IList<MembershipType> MembershipTypes { get; private set; }
        public static IList<MemberStatus> MemberStatuses { get; private set; }
        public static IList<Office> Offices { get; private set; }
        public static IList<Organization> Organizations { get; private set; }
        public static IList<OrganizationType> OrganizationTypes { get; private set; }
        public static IList<Payment> Payments { get; private set; }
        public static IList<PaymentSource> PaymentSources { get; private set; }
        public static IList<PaymentType> PaymentTypes { get; private set; }
        public static IList<Person> Persons { get; private set; }
        public static IList<PersonalNote> PersonalNotes { get; private set; }
        public static IList<Phone> Phones { get; private set; }
        public static IList<PrivacyLevel> PrivacyLevels { get; private set; }
        public static IList<Sponsor> Sponsors { get; private set; }
        public static IList<TaskForEvent> TaskForEvents { get; private set; }
        public static IList<TermInOffice> TermInOffices { get; private set; }
        public static IList<Title> Titles { get; private set; }
        
        static MockDb()
        {
            Addresses = GetAddressRecords();
            CategoryOfOrganizations = GetCategoryOfOrganizationRecords();
            CategoryOfPersons = GetCategoryOfPersons();
            ContactForSponsors = GetContactForSponsors();
            DocumentTypes = GetDocumentTypes();
            EMails = GetEmails();
            EMailTypes = GetEmailTypes();
            Events = GetEvents();
            EventDocuments = GetEventDocuments();
            EventMembers = GetEventMember();
            MemberInfos = GetMemberInfo();
            MembershipTypes = GetMembershipTypes();
            MemberStatuses = GetMemberStatuses();
            Offices = GetOffices();
            Organizations = GetOrganizations();
            OrganizationTypes = GetOrganizationTypes();
            Payments = GetPayments();
            PaymentSources = GetPaymentSources();
            PaymentTypes = GetPaymentTypes();
            Persons = GetPersons();
            PersonalNotes = GetPersonalNote();
            Phones = GetPhones();
            PrivacyLevels = GetPrivacyLevels();
            Sponsors = GetSponsors();
            TaskForEvents = GetTaskForEvents();
            TermInOffices = GetTermInOffices();
            Titles = GetTitles();
        }    
        
        private static IList<Address> GetAddressRecords()
        {
            return new List<Address>
            {
                new Address()
                {
                    Id = 1, Address1 = "8321 Oxford Drive", Address2 = "Apt 103", City = "Greendale",
                    State = "WI", PostCode = "53129", LastUpdatedBy = "edm",
                    LastUpdatedDate = DateTime.Now
                },
                new Address()
                {
                    Id = 2, Address1 = "921 S. Brittany Way", City = "Englewood",
                    State = "CO", PostCode = "80112", LastUpdatedBy = "edm",
                    LastUpdatedDate = DateTime.Now
                }
            };
        }

        private static IList<CategoryOfOrganization> GetCategoryOfOrganizationRecords()
        {
            return new List<CategoryOfOrganization>()
            {
                new CategoryOfOrganization()
                {
                    Id = 1,
                    Category = "Org Category 1",
                    DisplayOrder = 0
                },
                new CategoryOfOrganization()
                {
                    Id = 2,
                    Category = "Org Category 2",
                    DisplayOrder = 1
                }
            };
        }

        private static IList<CategoryOfPerson> GetCategoryOfPersons()
        {
            return new List<CategoryOfPerson>()
            {
                new CategoryOfPerson()
                {
                    Id = 1,
                    Category = "Person Category 1",
                    DisplayOrder = 0
                },
                new CategoryOfPerson()
                {
                    Id = 2,
                    Category = "Org Category 2",
                    DisplayOrder = 1
                }
            };
        }

        private static IList<ContactForSponsor> GetContactForSponsors()
        {
            return new List<ContactForSponsor>();
        }

        private static IList<DocumentType> GetDocumentTypes()
        {
            return new List<DocumentType>
            {
                new DocumentType() {Id = 1, Description = "Document Type A", Notes = String.Empty, 
                    LastUpdatedBy = "edm", LastUpdatedDate = DateTime.Now},
                new DocumentType() {Id = 2, Description = "Document Type B", Notes = "some notes",
                    LastUpdatedBy = "edm", LastUpdatedDate = DateTime.Now},
                new DocumentType() {Id = 3, Description = "Document Type C", Notes = String.Empty,
                    LastUpdatedBy = "edm", LastUpdatedDate = DateTime.Now},
            };
        }

        private static IList<EMail> GetEmails()
        {
            return new List<EMail>()
            {
                new EMail()
                {
                    Id = 1, EMailType = new EMailType() {Id = 1, Description = "personal", Notes = ""},
                    EMailAddress = "edm@ecs.com", LastUpdatedBy = "edm", LastUpdatedDate = DateTime.Now,
                    Notes = "some notes", Organizations = new List<Organization>(), Persons = new List<Person>()
                },
                new EMail()
                {
                    Id = 2, EMailType = new EMailType() {Id = 2, Description = "work", Notes = ""},
                    EMailAddress = "edm@ecs.com", LastUpdatedBy = "edm", LastUpdatedDate = DateTime.Now,
                    Notes = "some notes", Organizations = new List<Organization>(), Persons = new List<Person>()
                }
                
            };
        }

        private static IList<EMailType> GetEmailTypes()
        {
            return new List<EMailType>
            {
                new EMailType() { Id = 1, Description = "work", Notes = String.Empty},
                new EMailType() { Id = 2,Description = "home", Notes = "notes for home" }
            };
        }

        private static IList<Event> GetEvents()
        {
            return new List<Event>();
        }

        private static IList<EventDocument> GetEventDocuments()
        {
            return new List<EventDocument>();
        }

        private static IList<EventMember> GetEventMember()
        {
            return new List<EventMember>();
        }

        private static IList<MemberInfo> GetMemberInfo()
        {
            return new List<MemberInfo>();
        }

        private static IList<MembershipType> GetMembershipTypes()
        {
            return new List<MembershipType>()
            {
                new MembershipType() { Id =1,Description = "Membership Type A",LastUpdatedBy = "edm", 
                    LastUpdatedDate = DateTime.Now, Level=1, Notes=String.Empty},
                new MembershipType() { Id =2,Description = "Membership Type B",LastUpdatedBy = "edm", 
                    LastUpdatedDate = DateTime.Now, Level=2, Notes="some notes"}
            };
        }

        private static IList<MemberStatus> GetMemberStatuses()
        {
            return new List<MemberStatus>
            {
                new MemberStatus {Id = 1, Description = "Active", Notes = "This member is active."},
                new MemberStatus {Id = 2, Description = "Inactive", Notes = "This member is inactive"},
            };
        }

        private static IList<Office> GetOffices()
        {
            return new List<Office>()
            {
                new Office { Id =1, Name = "President", Term=6,CalendarPeriod = "months",ChosenHow = 1, 
                    Appointer = "voters", LastUpdatedDate = DateTime.Now, LastUpdatedBy = "edm", Notes = "notes"},
                new Office { Id =2, Name = "Vice President", Term=12,CalendarPeriod = "months",ChosenHow = 2, 
                    Appointer = "Dave", LastUpdatedDate = DateTime.Now, LastUpdatedBy = "edm", Notes = "notes"}
            };
        }

        private static IList<Organization> GetOrganizations()
        {
            return new List<Organization>();
        }

        private static IList<OrganizationType> GetOrganizationTypes()
        {
            return new List<OrganizationType>();
        }

        private static IList<Payment> GetPayments()
        {
            return new List<Payment>();
        }

        private static IList<PaymentSource> GetPaymentSources()
        {
            return new List<PaymentSource>()
            {
                new PaymentSource() {Id = 1, Description = "Source 1", Notes = "notes here"},
                new PaymentSource() {Id = 2, Description = "Payment Source 2", Notes = "more notes"}
            };
        }

        private static IList<PaymentType> GetPaymentTypes()
        {
            return new List<PaymentType>()
            {
                new PaymentType() {Id = 1, Description = "Payment Type 1", Notes = "notes here"},
                new PaymentType() {Id = 2, Description = "Payment Type 2", Notes = "more notes here"}
            };
        }

        private static IList<Person> GetPersons()
        {
            return new List<Person>
            {
                new Person() {}
            };
        }

        private static IList<PersonalNote> GetPersonalNote()
        {
            return new List<PersonalNote>();
        }

        private static IList<Phone> GetPhones()
        {
            return new List<Phone>
            {
                new Phone
                {
                    Id = 1, AreaCode = "216", DisplayOrder = 0, Extension = "", LastUpdatedBy = "edm",
                    LastUpdatedDate = DateTime.Now, Number = "256-8082", PhoneType = "Home"
                },
                new Phone
                {
                    Id = 1, AreaCode = "414", DisplayOrder = 0, Extension = "", LastUpdatedBy = "edm",
                    LastUpdatedDate = DateTime.Now, Number = "421-1634", PhoneType = "Work"
                },
            };
        }

        private static IList<PrivacyLevel> GetPrivacyLevels()
        {
            return new List<PrivacyLevel>
            {
                new PrivacyLevel() {Id = 1, Description = "privacy level 1", Notes = "notes to save"},
                new PrivacyLevel() {Id = 2, Description = "privacy level 2", Notes = "more notes"}
            };
        }

        private static IList<Sponsor> GetSponsors()
        {
            return new List<Sponsor>();
        }

        private static IList<TaskForEvent> GetTaskForEvents()
        {
            return  new List<TaskForEvent>();
        }

        private static IList<TermInOffice> GetTermInOffices()
        {
            return new List<TermInOffice>();
        }

        private static IList<Title> GetTitles()
        {
            return new List<Title>();
        }
        
    }
}