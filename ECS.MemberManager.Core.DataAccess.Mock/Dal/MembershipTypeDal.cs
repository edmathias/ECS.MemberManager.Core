using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class MembershipTypeDal : IDal<MembershipType>
    {
        public Task<MembershipType> Fetch(int id)
        {
            return Task.FromResult(MockDb.MembershipTypes.FirstOrDefault(dt => dt.Id == id));
        }

        public Task<List<MembershipType>> Fetch()
        {
            return Task.FromResult(MockDb.MembershipTypes.ToList());
        }

        public Task<MembershipType> Insert(MembershipType membershipType)
        {
            var lastMembershipType = MockDb.MembershipTypes.ToList().OrderByDescending(dt => dt.Id).First();
            membershipType.Id = 1 + lastMembershipType.Id;
            membershipType.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);

            MockDb.MembershipTypes.Add(membershipType);

            return Task.FromResult(membershipType);
        }

        public Task<MembershipType> Update(MembershipType membershipType)
        {
            var membershipTypeToUpdate =
                MockDb.MembershipTypes.FirstOrDefault(em => em.Id == membershipType.Id &&
                                                            em.RowVersion.SequenceEqual(membershipType.RowVersion));

            if (membershipTypeToUpdate == null)
                throw new Csla.DataPortalException(null);

            membershipTypeToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return Task.FromResult(membershipTypeToUpdate);
        }

        public Task Delete(int id)
        {
            var membershipTypesToDelete = MockDb.MembershipTypes.FirstOrDefault(dt => dt.Id == id);
            var listIndex = MockDb.MembershipTypes.IndexOf(membershipTypesToDelete);
            if (listIndex > -1)
                MockDb.MembershipTypes.RemoveAt(listIndex);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}