using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class MembershipTypeDal : IMembershipTypeDal
    {
        public async Task<MembershipType> Fetch(int id)
        {
            return MockDb.MembershipTypes.FirstOrDefault(dt => dt.Id == id);
        }

        public async Task<List<MembershipType>> Fetch()
        {
            return MockDb.MembershipTypes.ToList();
        }

        public async Task<MembershipType> Insert( MembershipType membershipType)
        {
            var lastMembershipType = MockDb.MembershipTypes.ToList().OrderByDescending(dt => dt.Id).First();
            membershipType.Id = 1+lastMembershipType.Id;
            membershipType.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            
            MockDb.MembershipTypes.Add(membershipType);
            
            return membershipType;
        }

        public async Task<MembershipType> Update(MembershipType membershipType)
        {
            var membershipTypeToUpdate =
                MockDb.MembershipTypes.FirstOrDefault(em => em.Id == membershipType.Id &&
                                                            em.RowVersion.SequenceEqual(membershipType.RowVersion));

            if(membershipTypeToUpdate == null)
                throw new Csla.DataPortalException(null);
           
            membershipTypeToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return membershipTypeToUpdate;        
        }

        public async Task Delete(int id)
        {
            var membershipTypesToDelete = MockDb.MembershipTypes.FirstOrDefault(dt => dt.Id == id);
            var listIndex = MockDb.MembershipTypes.IndexOf(membershipTypesToDelete);
            if(listIndex > -1)
                MockDb.MembershipTypes.RemoveAt(listIndex);
        }

        public void Dispose()
        {
        }
    }
}