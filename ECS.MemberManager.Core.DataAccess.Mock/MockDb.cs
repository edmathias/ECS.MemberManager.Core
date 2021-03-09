using System;
using System.Collections.Generic;
using System.Linq;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class MockDb
    {
        public static IList<Address> Addresses { get; private set; }
        public static IList<CategoryOfOrganization> CategoryOfOrganizations { get; private set; }
        public static IList<CategoryOfPerson> CategoryOfPersons { get; private set; }
        public static IList<ContactForSponsor> ContactForSponsors { get; private set; }
        public static IList<DocumentType> DocumentTypes { get; private set; }
        public static IList<EMail> EMails { get; private set; }
        public static IList<EMailType> EMailTypes { get; private set; }
        public static IList<Event> Events { get; private set; }
        public static IList<EventDocument> EventDocuments { get; private set; }
        public static IList<EventMember> EventMembers { get; private set; }
        public static IList<MemberInfo> MemberInfo { get; private set; }
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
            ResetMockDb();
        }

        public static void ResetMockDb()
        {
            EMailTypes = GetEmailTypes();
            OrganizationTypes = GetOrganizationTypes();
            Organizations = GetOrganizations();
            EMails = GetEmails();
            Titles = GetTitles();
            Addresses = GetAddressRecords();
            Persons = GetPersons();
            Sponsors = GetSponsors();
            CategoryOfPersons = GetCategoryOfPersons();
            CategoryOfOrganizations = GetCategoryOfOrganizationRecords();
            Events = GetEvents();
            ContactForSponsors = GetContactForSponsors();
            DocumentTypes = GetDocumentTypes();
            EventDocuments = GetEventDocuments();
            EventMembers = GetEventMember();
            MemberInfo = GetMemberInfo();
            MembershipTypes = GetMembershipTypes();
            MemberStatuses = GetMemberStatuses();
            Offices = GetOffices();
            Payments = GetPayments();
            PaymentSources = GetPaymentSources();
            PaymentTypes = GetPaymentTypes();
            PersonalNotes = GetPersonalNote();
            Phones = GetPhones();
            PrivacyLevels = GetPrivacyLevels();
            TaskForEvents = GetTaskForEvents();
            TermInOffices = GetTermInOffices();
        }

        private static IList<Address> GetAddressRecords()
        {
            return new List<Address>
            {
                new Address()
                {
                    Id = 1, Address1 = "8321 Oxford Drive", Address2 = "Apt 103", City = "Greendale",
                    State = "WI", PostCode = "53129", LastUpdatedBy = "edm", Notes = "some notes",
                    LastUpdatedDate = DateTime.Now,
                    Organizations = new List<Organization>()
                    {
                        new Organization()
                        {
                            Id = 1,
                            Name = "test org",
                            DateOfFirstContact = DateTime.Now,
                            LastUpdatedBy = "edm",
                            LastUpdatedDate = DateTime.Now,
                            OrganizationType = GetOrganizationTypes().First(ot => ot.Id == 1)
                        }
                    },
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new Address()
                {
                    Id = 2, Address1 = "921 S. Brittany Way", City = "Englewood",
                    State = "CO", PostCode = "80112", LastUpdatedBy = "edm", Notes = "notes",
                    LastUpdatedDate = DateTime.Now,
                    Organizations = GetOrganizations(),
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                // use this record for delete only otherwise xunit will contend
                new Address()
                {
                    Id = 99, Address1 = "921 Delete St.", City = "Kirtland",
                    State = "OH", PostCode = "44094", LastUpdatedBy = "edm", Notes = "more notes",
                    LastUpdatedDate = DateTime.Now,
                    Organizations = GetOrganizations(),
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
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
                    DisplayOrder = 0,
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new CategoryOfOrganization()
                {
                    Id = 2,
                    Category = "Org Category 2",
                    DisplayOrder = 1,
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new CategoryOfOrganization()
                {
                    Id = 99,
                    Category = "Org to delete",
                    DisplayOrder = 1,
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
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
                    DisplayOrder = 0,
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new CategoryOfPerson()
                {
                    Id = 2,
                    Category = "Org Category 2",
                    DisplayOrder = 1,
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new CategoryOfPerson()
                {
                    Id = 99,
                    Category = "Org Category 2",
                    DisplayOrder = 1,
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                }
            };
        }

        private static IList<ContactForSponsor> GetContactForSponsors()
        {
            return new List<ContactForSponsor>()
            {
                new ContactForSponsor()
                {
                    Id = 1, Purpose = "purpose for contact", DateWhenContacted = DateTime.Now,
                    Notes = "Notes here", RecordOfDiscussion = "record of discussion here", LastUpdatedBy = "edm",
                    LastUpdatedDate = DateTime.Now,
                    Person = MockDb.GetPersons().First(),
                    Sponsor = MockDb.GetSponsors().Single(s => s.Id == 2),
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new ContactForSponsor()
                {
                    Id = 2, Purpose = "purpose for contact 2", DateWhenContacted = DateTime.Now,
                    Notes = "Notes here", RecordOfDiscussion = "record of discussion here", LastUpdatedBy = "edm",
                    LastUpdatedDate = DateTime.Now,
                    Person = MockDb.GetPersons().Single(p => p.Id == 2),
                    Sponsor = MockDb.GetSponsors().Single(s => s.Id == 2),
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new ContactForSponsor()
                {
                    Id = 99, Purpose = "delete this contact", DateWhenContacted = DateTime.Now,
                    Notes = "Deleted Notes here", RecordOfDiscussion = "deleted record", LastUpdatedBy = "edm",
                    LastUpdatedDate = DateTime.Now,
                    Person = MockDb.GetPersons().Single(p => p.Id == 1),
                    Sponsor = MockDb.GetSponsors().Single(s => s.Id == 2),
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                }
            };
        }

        private static IList<DocumentType> GetDocumentTypes()
        {
            return new List<DocumentType>
            {
                new DocumentType()
                {
                    Id = 1, Description = "Document Type A", Notes = String.Empty,
                    LastUpdatedBy = "edm", LastUpdatedDate = DateTime.Now,
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new DocumentType()
                {
                    Id = 2, Description = "Document Type B", Notes = "some notes",
                    LastUpdatedBy = "edm", LastUpdatedDate = DateTime.Now,
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new DocumentType()
                {
                    Id = 3, Description = "Document Type C", Notes = String.Empty,
                    LastUpdatedBy = "edm", LastUpdatedDate = DateTime.Now,
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new DocumentType()
                {
                    Id = 99, Description = "Document Type to Delete", Notes = String.Empty,
                    LastUpdatedBy = "edm", LastUpdatedDate = DateTime.Now,
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
            };
        }

        private static IList<EMail> GetEmails()
        {
            return new List<EMail>()
            {
                new EMail()
                {
                    Id = 1, EMailType = EMailTypes.First(),
                    EMailAddress = "edm@ecs.com", LastUpdatedBy = "edm", LastUpdatedDate = DateTime.Now,
                    Notes = "some notes", Organizations = GetOrganizations(), Persons = GetPersons(),
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new EMail()
                {
                    Id = 2, EMailType = EMailTypes.First(),
                    EMailAddress = "edm@ecs.com", LastUpdatedBy = "edm", LastUpdatedDate = DateTime.Now,
                    Notes = "some notes", Organizations = GetOrganizations(), Persons = GetPersons(),
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new EMail()
                {
                    Id = 99, EMailType = EMailTypes.First(),
                    EMailAddress = "edm@ecs.com", LastUpdatedBy = "edm", LastUpdatedDate = DateTime.Now,
                    Notes = "test the delete", Organizations = GetOrganizations(), Persons = GetPersons(),
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                }
            };
        }

        private static IList<EMailType> GetEmailTypes()
        {
            return new List<EMailType>
            {
                new EMailType() {Id = 1, Description = "work", Notes = String.Empty,RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)},
                new EMailType() {Id = 2, Description = "home", Notes = "notes for home",RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)},
                new EMailType() {Id = 99, Description = "delete this", Notes = "notes for home",RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)}
            };
        }

        private static IList<Event> GetEvents()
        {
            return new List<Event>()
            {
                new Event()
                {
                    Description = "My new event",
                    EventName = "Once in a lifetime event",
                    Id = 1,
                    IsOneTime = true,
                    LastUpdatedBy = "edm",
                    LastUpdatedDate = DateTime.Now,
                    NextDate = DateTime.Now.AddMonths(3),
                    Notes = "notes for this",
                    Persons = new List<Person>(),
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new Event()
                {
                    Description = "Another new event",
                    EventName = "Another once in a lifetime event",
                    Id = 2,
                    IsOneTime = false,
                    LastUpdatedBy = "edm",
                    LastUpdatedDate = DateTime.Now,
                    NextDate = DateTime.Now.AddDays(14),
                    Notes = "notes for this",
                    Persons = new List<Person>(),
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new Event()
                {
                    Description = "event to delete",
                    EventName = "Another once in a lifetime event",
                    Id = 99,
                    IsOneTime = false,
                    LastUpdatedBy = "edm",
                    LastUpdatedDate = DateTime.Now,
                    NextDate = DateTime.Now.AddDays(14),
                    Notes = "notes for this",
                    Persons = new List<Person>(),
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                }
            };
        }

        private static IList<EventDocument> GetEventDocuments()
        {
            return new List<EventDocument>()
            {
                new EventDocument()
                {
                    Id = 1,
                    DocumentName = "event document 1",
                    DocumentType = GetDocumentTypes().First(dt => dt.Id == 1),
                    Event = GetEvents().First(ev => ev.Id == 2),
                    LastUpdatedBy = "edm",
                    LastUpdatedDate = DateTime.Now,
                    Notes = "event document notes 1",
                    PathAndFileName = "c:\\pathandfilename\\file.nam",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new EventDocument()
                {
                    Id = 2,
                    DocumentName = "event document 2",
                    DocumentType = GetDocumentTypes().First(dt => dt.Id == 1),
                    Event = GetEvents().First(ev => ev.Id == 1),
                    LastUpdatedBy = "edm",
                    LastUpdatedDate = DateTime.Now,
                    Notes = "event document notes 2",
                    PathAndFileName = "c:\\pathandfilename\\file.nam",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new EventDocument()
                {
                    Id = 99,
                    DocumentName = "event document 99",
                    DocumentType = GetDocumentTypes().First(dt => dt.Id == 1),
                    Event = GetEvents().First(ev => ev.Id == 1),
                    LastUpdatedBy = "edm",
                    LastUpdatedDate = DateTime.Now,
                    Notes = "event document notes 99",
                    PathAndFileName = "c:\\pathandfilename\\file.nam",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                }
            };
        }

        private static IList<EventMember> GetEventMember()
        {
            return new List<EventMember>();
        }

        private static IList<MemberInfo> GetMemberInfo()
        {
            return new List<MemberInfo>()
            {
                new MemberInfo()
                {
                    Id = 1,
                    DateFirstJoined = DateTime.Now,
                    LastUpdatedDate = DateTime.Now,
                    LastUpdatedBy = "joe",
                    MemberNumber = "12345",
                    MembershipType = GetMembershipTypes().First(mt => mt.Id == 1),
                    MemberStatus = GetMemberStatuses().First(ms => ms.Id == 1),
                    Notes = "notes 1",
                    Person = GetPersons().First(p => p.Id == 2),
                    PrivacyLevel = new PrivacyLevel() { Id =1, Description = "priv level 1", Notes = "notes 1"},
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new MemberInfo()
                {
                    Id = 2,
                    DateFirstJoined = DateTime.Now,
                    LastUpdatedDate = DateTime.Now,
                    LastUpdatedBy = "joe",
                    MemberNumber = "12345",
                    MembershipType = GetMembershipTypes().First(mt => mt.Id == 2),
                    MemberStatus = GetMemberStatuses().First(ms => ms.Id == 2),
                    Notes = "notes 2",
                    Person = GetPersons().First(p => p.Id == 2),
                    PrivacyLevel = new PrivacyLevel() { Id =2, Description = "priv level 2", Notes = "notes 1"},
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new MemberInfo()
                {
                    Id = 99,
                    DateFirstJoined = DateTime.Now,
                    LastUpdatedDate = DateTime.Now,
                    LastUpdatedBy = "joe",
                    MemberNumber = "99999",
                    MembershipType = GetMembershipTypes().First(mt => mt.Id == 2),
                    MemberStatus = GetMemberStatuses().First(ms => ms.Id == 2),
                    Notes = "notes 99",
                    Person = GetPersons().First(p => p.Id == 2),
                    PrivacyLevel = new PrivacyLevel() { Id =2, Description = "priv level 2", Notes = "notes 1"},
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                }
            };
        }

        private static IList<MembershipType> GetMembershipTypes()
        {
            return new List<MembershipType>()
            {
                new MembershipType()
                {
                    Id = 1, Description = "Membership Type A", LastUpdatedBy = "edm",
                    LastUpdatedDate = DateTime.Now, Level = 1, Notes = String.Empty,
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new MembershipType()
                {
                    Id = 2, Description = "Membership Type B", LastUpdatedBy = "edm",
                    LastUpdatedDate = DateTime.Now, Level = 2, Notes = "some notes",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new MembershipType()
                {
                    Id = 99, Description = "Membership Type to delete", LastUpdatedBy = "edm",
                    LastUpdatedDate = DateTime.Now, Level = 2, Notes = "some notes",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                }
            };
        }

        private static IList<MemberStatus> GetMemberStatuses()
        {
            return new List<MemberStatus>
            {
                new MemberStatus {Id = 1, 
                    Description = "Active", 
                    Notes = "This member is active.",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new MemberStatus {Id = 2, 
                    Description = "Inactive", 
                    Notes = "This member is inactive",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new MemberStatus {Id = 99, 
                    Description = "Inactive", 
                    Notes = "This member is deleted",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
            };
        }

        private static IList<Office> GetOffices()
        {
            return new List<Office>()
            {
                new Office
                {
                    Id = 1, Name = "President", Term = 6, CalendarPeriod = "months", ChosenHow = 1,
                    Appointer = "voters", LastUpdatedDate = DateTime.Now, LastUpdatedBy = "edm", Notes = "notes",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new Office
                {
                    Id = 2, Name = "Vice President", Term = 12, CalendarPeriod = "months", ChosenHow = 2,
                    Appointer = "Dave", LastUpdatedDate = DateTime.Now, LastUpdatedBy = "edm", Notes = "notes",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new Office
                {
                    Id = 99, Name = "delete this", Term = 12, CalendarPeriod = "months", ChosenHow = 2,
                    Appointer = "Dave", LastUpdatedDate = DateTime.Now, LastUpdatedBy = "edm", Notes = "notes",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                }
            };
        }

        private static IList<Organization> GetOrganizations()
        {
            return new List<Organization>()
            {
                new Organization()
                {
                    Id = 1,
                    Addresses = new List<Address>(),
                    OrganizationType = GetOrganizationTypes().First(ot => ot.Id == 1),
                    DateOfFirstContact = DateTime.Now,
                    EMails = new List<EMail>(),
                    LastUpdatedBy = "edm",
                    LastUpdatedDate = DateTime.Now,
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new Organization()
                {
                    Id = 2,
                    Addresses = new List<Address>(),
                    OrganizationType = GetOrganizationTypes().First(ot => ot.Id == 2),
                    DateOfFirstContact = DateTime.Now,
                    EMails = new List<EMail>(),
                    LastUpdatedBy = "edm",
                    LastUpdatedDate = DateTime.Now,
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new Organization()
                {
                    Id = 99,
                    Addresses = new List<Address>(),
                    OrganizationType = GetOrganizationTypes().First(ot => ot.Id == 2),
                    DateOfFirstContact = DateTime.Now,
                    EMails = new List<EMail>(),
                    LastUpdatedBy = "edm",
                    LastUpdatedDate = DateTime.Now,
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                }
            };
        }

        private static IList<OrganizationType> GetOrganizationTypes()
        {
            return new List<OrganizationType>()
            {
                new OrganizationType()
                {
                    Id = 1,
                    Name = "Organization type 1",
                    Notes = "notes for #1",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new OrganizationType()
                {
                    Id = 2,
                    Name = "Organization type 2",
                    Notes = "",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new OrganizationType()
                {
                    Id = 99,
                    Name = "Organization to delete",
                    Notes = "",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                }
            };
        }

        private static IList<Payment> GetPayments()
        {
            return new List<Payment>()
            {
                new Payment()
                {
                    Id = 1, Amount = 39.99d, PaymentDate = DateTime.Now,
                    PaymentExpirationDate = DateTime.Now.AddDays(365), LastUpdatedBy = "edm",
                    LastUpdatedDate = DateTime.Now, Notes = "note",
                    PaymentSource = new PaymentSource() {Id = 1, Description = "self"},
                    PaymentType = new PaymentType() {Id = 1, Description = "check"},
                    Person = GetPersons()[1],
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new Payment()
                {
                    Id = 2, Amount = 83.61d, PaymentDate = DateTime.Now,
                    PaymentExpirationDate = DateTime.Now.AddYears(2), LastUpdatedBy = "edm",
                    LastUpdatedDate = DateTime.Now, Notes = "note",
                    PaymentSource = new PaymentSource() {Id = 2, Description = "spouse"},
                    PaymentType = new PaymentType() {Id = 2, Description = "credit card"},
                    Person = GetPersons()[2],
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new Payment()
                {
                    Id = 99, Amount = 83.61d, PaymentDate = DateTime.Now,
                    PaymentExpirationDate = DateTime.Now.AddYears(2), LastUpdatedBy = "edm",
                    LastUpdatedDate = DateTime.Now, Notes = "delete this record",
                    PaymentSource = new PaymentSource() {Id = 2, Description = "spouse"},
                    PaymentType = new PaymentType() {Id = 2, Description = "credit card"},
                    Person = GetPersons()[1],
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                }
            };
        }

        private static IList<PaymentSource> GetPaymentSources()
        {
            return new List<PaymentSource>()
            {
                new PaymentSource() {Id = 1, 
                    Description = "Source 1", 
                    Notes = "notes here",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new PaymentSource()
                {
                    Id = 2, 
                    Description = "Payment Source 2", 
                    Notes = "more notes",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new PaymentSource()
                {
                    Id = 99, 
                    Description = "Payment to Delete", 
                    Notes = "more notes",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                }
            };
        }

        private static IList<PaymentType> GetPaymentTypes()
        {
            return new List<PaymentType>()
            {
                new PaymentType()
                {
                    Id = 1, 
                    Description = "Payment Type 1", 
                    Notes = "notes here",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new PaymentType()
                {
                    Id = 2, 
                    Description = "Payment Type 2", 
                    Notes = "more notes here",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new PaymentType()
                {
                    Id = 99, 
                    Description = "Payment Type to delete", 
                    Notes = "more notes here",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                }
            };
        }

        private static IList<Person> GetPersons()
        {
            return new List<Person>
            {
                new Person()
                {
                    Id = 1,
                    Addresses = GetAddressRecords(),
                    BirthDate = DateTime.Now.Subtract(new TimeSpan(9125, 0, 0, 0)),
                    CategoryOfPersons = GetCategoryOfPersons(),
                    Code = "n/a",
                    DateOfFirstContact = DateTime.Now.Subtract(new TimeSpan(200, 0, 0, 0)),
                    Events = GetEvents(),
                    FirstName = "Joe",
                    MiddleName = "E",
                    LastName = "Blow",
                    Phones = GetPhones(),
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new Person()
                {
                    Id = 2,
                    Addresses = GetAddressRecords(),
                    BirthDate = DateTime.Now.Subtract(new TimeSpan(9125, 0, 0, 0)),
                    CategoryOfPersons = GetCategoryOfPersons(),
                    Code = "n/a",
                    DateOfFirstContact = DateTime.Now.Subtract(new TimeSpan(200, 0, 0, 0)),
                    Events = GetEvents(),
                    FirstName = "Fred",
                    MiddleName = String.Empty,
                    LastName = "Derf",
                    Phones = GetPhones(),
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new Person() // person for delete
                {
                    Id = 99,
                    Addresses = GetAddressRecords(),
                    BirthDate = DateTime.Now.Subtract(new TimeSpan(9125, 0, 0, 0)),
                    CategoryOfPersons = GetCategoryOfPersons(),
                    Code = "n/a",
                    DateOfFirstContact = DateTime.Now.Subtract(new TimeSpan(200, 0, 0, 0)),
                    Events = GetEvents(),
                    FirstName = "Fred",
                    MiddleName = String.Empty,
                    LastName = "Derf",
                    Phones = GetPhones(),
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                }
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
                    Id = 1, AreaCode = "303", DisplayOrder = 0, Extension = "111", LastUpdatedBy = "edm",
                    LastUpdatedDate = DateTime.Now, Number = "333-2222", PhoneType = "mobile",
                    Notes = "notes for phone #1",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new Phone
                {
                    Id = 2, AreaCode = "216", DisplayOrder = 0, Extension = null, LastUpdatedBy = "edm",
                    LastUpdatedDate = DateTime.Now, Number = "256-8888", Notes = null, PhoneType = "work",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new Phone
                {
                    Id = 99, AreaCode = "414", DisplayOrder = 1, Extension = "12", LastUpdatedBy = "joe",
                    LastUpdatedDate = DateTime.Now, Number = "421-7777", PhoneType = "delete",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
            };
        }

        private static IList<PrivacyLevel> GetPrivacyLevels()
        {
            return new List<PrivacyLevel>
            {
                new PrivacyLevel()
                {
                    Id = 1, 
                    Description = "privacy level 1", 
                    Notes = "notes to save",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new PrivacyLevel()
                {
                    Id = 2, 
                    Description = "privacy level 2", 
                    Notes = "more notes",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new PrivacyLevel()
                {
                    Id = 99, 
                    Description = "privacy level 99", 
                    Notes = "privacy to delete",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                }
            };
        }

        private static IList<Sponsor> GetSponsors()
        {
            return new List<Sponsor>()
            {
                new Sponsor()
                {
                    DateOfFirstContact = DateTime.Now,
                    DateSponsorAccepted = DateTime.Now,
                    Details = "sponsor details",
                    Id = 1,
                    LastUpdatedBy = "edm",
                    LastUpdatedDate = DateTime.Now,
                    Notes = "notes",
                    Organization = GetOrganizations().First(o => o.Id == 1),
                    Person = GetPersons().First(p => p.Id == 1),
                    ReferredBy = "referrer",
                    Status = "status",
                    TypeName = "type name",
                    SponsorUntilDate = DateTime.Now
                },
                new Sponsor()
                {
                    DateOfFirstContact = DateTime.Now,
                    DateSponsorAccepted = DateTime.Now,
                    Details = "sponsor details 2",
                    Id = 2,
                    LastUpdatedBy = "edm",
                    LastUpdatedDate = DateTime.Now,
                    Notes = "notes 2",
                    Organization = GetOrganizations().First(o => o.Id == 1),
                    Person = GetPersons().First(p => p.Id == 1),
                    ReferredBy = "referrer 2",
                    Status = "status 2",
                    TypeName = "type name 2",
                    SponsorUntilDate = DateTime.Now
                },
                new Sponsor()
                {
                    DateOfFirstContact = DateTime.Now,
                    DateSponsorAccepted = DateTime.Now,
                    Details = "sponsor details 99",
                    Id = 99,
                    LastUpdatedBy = "edm",
                    LastUpdatedDate = DateTime.Now,
                    Notes = "notes 99",
                    Organization = GetOrganizations().First(o => o.Id == 1),
                    Person = GetPersons().First(p => p.Id == 1),
                    ReferredBy = "referrer 99",
                    Status = "status 99",
                    TypeName = "type name 99",
                    SponsorUntilDate = DateTime.Now
                }
            };
        }

        private static IList<TaskForEvent> GetTaskForEvents()
        {
            return new List<TaskForEvent>()
            {
                new TaskForEvent()
                {
                    Id = 1,
                    ActualDate = DateTime.Now,
                    Event = GetEvents().Single(ev => ev.Id == 1),
                    Information = "info for task event 1",
                    LastUpdatedBy = "joe",
                    LastUpdatedDate = DateTime.Now,
                    Notes = "notes 1",
                    PlannedDate = DateTime.Now,
                    TaskName = "task name 1",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new TaskForEvent()
                {
                    Id = 2,
                    ActualDate = DateTime.Now,
                    Event = GetEvents().Single(ev => ev.Id == 2),
                    Information = "info for task event 2",
                    LastUpdatedBy = "joe",
                    LastUpdatedDate = DateTime.Now,
                    Notes = "notes 2",
                    PlannedDate = DateTime.Now,
                    TaskName = "task name 2",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new TaskForEvent()
                {
                    Id = 99,
                    ActualDate = DateTime.Now,
                    Event = GetEvents().Single(ev => ev.Id == 2),
                    Information = "info for task event 99",
                    LastUpdatedBy = "joe",
                    LastUpdatedDate = DateTime.Now,
                    Notes = "notes 99",
                    PlannedDate = DateTime.Now,
                    TaskName = "task name 99",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                }
            };
        }

        private static IList<TermInOffice> GetTermInOffices()
        {
            return new List<TermInOffice>()
            {
                new TermInOffice()
                {
                    Id = 1,
                    Person = GetPersons().Single(p => p.Id == 1),
                    Office = GetOffices().Single(o => o.Id == 1),
                    StartDate = DateTime.Now,
                    LastUpdatedBy = "edm",
                    LastUpdatedDate = DateTime.Now,
                    Notes = "notes for 1",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new TermInOffice()
                {
                    Id = 2,
                    Person = GetPersons().Single(p => p.Id == 2),
                    Office = GetOffices().Single(o => o.Id == 2),
                    StartDate = DateTime.Now,
                    LastUpdatedBy = "edm",
                    LastUpdatedDate = DateTime.Now,
                    Notes = "notes for 2",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new TermInOffice()
                {
                    Id = 99,
                    Person = GetPersons().Single(p => p.Id == 2),
                    Office = GetOffices().Single(o => o.Id == 1),
                    StartDate = DateTime.Now,
                    LastUpdatedBy = "edm",
                    LastUpdatedDate = DateTime.Now,
                    Notes = "delete this 99",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                }
            };
        }

        private static IList<Title> GetTitles()
        {
            return new List<Title>()
            {
                new Title()
                {
                    Id = 1,
                    Abbreviation = "Mr",
                    Description = "Mister",
                    DisplayOrder = 0,
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new Title()
                {
                    Id = 2,
                    Abbreviation = "Mrs.",
                    Description = "Missus",
                    DisplayOrder = 1,
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new Title()
                {
                    Id = 99,
                    Abbreviation = "XX",
                    Description = "delete me",
                    RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                }
            };
        }
    }
}