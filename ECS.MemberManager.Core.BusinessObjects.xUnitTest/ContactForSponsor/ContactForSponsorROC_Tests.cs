﻿using System;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class ContactForSponsorROC_Tests : CslaBaseTest
    {
        [Fact]
        public async void ContactForSponsorROC_TestGetChild()
        {
            const int ID_VALUE = 999;

            var docType = BuildContactForSponsor();
            docType.Id = ID_VALUE;

            var contactForSponsor = await ContactForSponsorROC.GetContactForSponsorROC(docType);

            Assert.NotNull(contactForSponsor);
            Assert.IsType<ContactForSponsorROC>(contactForSponsor);
            Assert.Equal(contactForSponsor.Id, contactForSponsor.Id);
            Assert.Equal(contactForSponsor.Person.Id, contactForSponsor.Person.Id);
            Assert.Equal(contactForSponsor.Sponsor.Id, contactForSponsor.Sponsor.Id);
            Assert.Equal(contactForSponsor.Purpose, contactForSponsor.Purpose);
            Assert.Equal(contactForSponsor.Notes, contactForSponsor.Notes);
            Assert.Equal(contactForSponsor.RecordOfDiscussion, contactForSponsor.RecordOfDiscussion);
            Assert.Equal(contactForSponsor.LastUpdatedBy, contactForSponsor.LastUpdatedBy);
            Assert.Equal(contactForSponsor.LastUpdatedDate, contactForSponsor.LastUpdatedDate);
        }

        private ContactForSponsor BuildContactForSponsor()
        {
            var contactForSponsor = new ContactForSponsor();
            contactForSponsor.Id = 1;
            contactForSponsor.Person = new Person() {Id = 1};
            contactForSponsor.Sponsor = new Sponsor() {Id = 1};
            contactForSponsor.Purpose = "purpose of contact";
            contactForSponsor.RecordOfDiscussion = "discussion record";
            contactForSponsor.LastUpdatedBy = "edm";
            contactForSponsor.LastUpdatedDate = DateTime.Now;
            contactForSponsor.Notes = "notes for doctype";

            return contactForSponsor;
        }
    }
}