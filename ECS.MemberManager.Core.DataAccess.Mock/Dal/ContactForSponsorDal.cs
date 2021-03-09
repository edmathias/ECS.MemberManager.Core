using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class ContactForSponsorDal : IContactForSponsorDal
    {
        public void Dispose()
        {
        }

        public async Task<ContactForSponsor> Fetch(int id)
        {
            return MockDb.ContactForSponsors.FirstOrDefault(co => co.Id == id);
        }

        public async Task<List<ContactForSponsor>> Fetch()
        {
            return MockDb.ContactForSponsors.ToList();
        }

        public async Task<ContactForSponsor> Insert(ContactForSponsor contactOfPerson)
        {
            var lastContact = MockDb.ContactForSponsors.ToList().OrderByDescending( co => co.Id).First();
            contactOfPerson.Id = lastContact.Id + 1;
            contactOfPerson.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            
            MockDb.ContactForSponsors.Add(contactOfPerson);
            
            return contactOfPerson;
        }

        public async Task<ContactForSponsor> Update(ContactForSponsor contactOfPerson)
        {
            var contactToUpdate =
                MockDb.ContactForSponsors.FirstOrDefault(em => em.Id == contactOfPerson.Id &&
                                                      em.RowVersion.SequenceEqual(contactOfPerson.RowVersion));

            if(contactToUpdate == null)
                throw new Csla.DataPortalException(null);
           
            contactToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return contactToUpdate;
        }

        public async Task Delete(int id)
        {
            var contactToDelete = MockDb.ContactForSponsors.FirstOrDefault(co => co.Id == id);
            var listIndex = MockDb.ContactForSponsors.IndexOf(contactToDelete);
            if(listIndex > -1)
                MockDb.ContactForSponsors.RemoveAt(listIndex);
        }
    }
}