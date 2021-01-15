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
            MockDb.MembershipTypes.Add(membershipType);
            
            return membershipType;
        }

        public async Task<MembershipType> Update(MembershipType membershipType)
        {
            return membershipType;
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