using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class TermInOfficeDal : IDal<TermInOffice>
    {
        public Task<TermInOffice> Fetch(int id)
        {
            return Task.FromResult(MockDb.TermInOffices.FirstOrDefault(dt => dt.Id == id));
        }

        public Task<List<TermInOffice>> Fetch()
        {
            return Task.FromResult(MockDb.TermInOffices.ToList());
        }

        public Task<TermInOffice> Insert(TermInOffice termInOffice)
        {
            var lastTermInOffice = MockDb.TermInOffices.ToList().OrderByDescending(dt => dt.Id).First();
            termInOffice.Id = 1 + lastTermInOffice.Id;
            termInOffice.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);

            MockDb.TermInOffices.Add(termInOffice);

            return Task.FromResult(termInOffice);
        }

        public Task<TermInOffice> Update(TermInOffice termInOffice)
        {
            var termInOfficeToUpdate =
                MockDb.TermInOffices.FirstOrDefault(em => em.Id == termInOffice.Id &&
                                                          em.RowVersion.SequenceEqual(termInOffice.RowVersion));

            if (termInOfficeToUpdate == null)
                throw new Csla.DataPortalException(null);

            termInOfficeToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return Task.FromResult(termInOfficeToUpdate);
        }

        public Task Delete(int id)
        {
            var termInOfficesToDelete = MockDb.TermInOffices.FirstOrDefault(dt => dt.Id == id);
            var listIndex = MockDb.TermInOffices.IndexOf(termInOfficesToDelete);
            if (listIndex > -1)
                MockDb.TermInOffices.RemoveAt(listIndex);
            
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}