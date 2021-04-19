using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class ContactForSponsorDal : IDal<ContactForSponsor>
    {
        public void Dispose()
        {
        }

        public Task<ContactForSponsor> Fetch(int id)
        {
            return Task.FromResult(MockDb.ContactForSponsors.FirstOrDefault(co => co.Id == id));
        }

        public Task<List<ContactForSponsor>> Fetch()
        {
            return Task.FromResult(MockDb.ContactForSponsors.ToList());
        }

        public Task<ContactForSponsor> Insert(ContactForSponsor contactOfPerson)
        {
            var lastContact = MockDb.ContactForSponsors.ToList().OrderByDescending(co => co.Id).First();
            contactOfPerson.Id = lastContact.Id + 1;
            contactOfPerson.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);

            MockDb.ContactForSponsors.Add(contactOfPerson);

            return Task.FromResult(contactOfPerson);
        }

        public Task<ContactForSponsor> Update(ContactForSponsor contactOfPerson)
        {
            var contactToUpdate =
                MockDb.ContactForSponsors.FirstOrDefault(em => em.Id == contactOfPerson.Id &&
                                                               em.RowVersion.SequenceEqual(contactOfPerson.RowVersion));

            if (contactToUpdate == null)
                throw new Csla.DataPortalException(null);

            contactToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return Task.FromResult(contactToUpdate);
        }

        public Task Delete(int id)
        {
            var contactToDelete = MockDb.ContactForSponsors.FirstOrDefault(co => co.Id == id);
            var listIndex = MockDb.ContactForSponsors.IndexOf(contactToDelete);
            if (listIndex > -1)
                MockDb.ContactForSponsors.RemoveAt(listIndex);
            
            return Task.CompletedTask;
        }
    }
}