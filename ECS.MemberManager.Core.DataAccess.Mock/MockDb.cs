using System;
using System.Collections.Generic;
using ECS.BizBricks.CRM.Core.EF.Domain;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

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
            return new List<Address>();
        }

        private static IList<CategoryOfOrganization> GetCategoryOfOrganizationRecords()
        {
           return new List<CategoryOfOrganization>();
        }

        private static IList<CategoryOfPerson> GetCategoryOfPersons()
        {
            return new List<CategoryOfPerson>();
        }

        private static IList<ContactForSponsor> GetContactForSponsors()
        {
            return new List<ContactForSponsor>();
        }

        private static IList<DocumentType> GetDocumentTypes()
        {
            return new List<DocumentType>();
        }

        private static IList<EMail> GetEmails()
        {
            return new List<EMail>();
        }

        private static IList<EMailType> GetEmailTypes()
        {
            return new List<EMailType>();
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
            return new List<MembershipType>();
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
            return new List<Office>();
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
            return new List<PaymentSource>();
        }

        private static IList<PaymentType> GetPaymentTypes()
        {
            return new List<PaymentType>();
        }

        private static IList<Person> GetPersons()
        {
            return new List<Person>();
        }

        private static IList<PersonalNote> GetPersonalNote()
        {
            return new List<PersonalNote>();
        }

        private static IList<Phone> GetPhones()
        {
            return new List<Phone>();
        }

        private static IList<PrivacyLevel> GetPrivacyLevels()
        {
            return new List<PrivacyLevel>();
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