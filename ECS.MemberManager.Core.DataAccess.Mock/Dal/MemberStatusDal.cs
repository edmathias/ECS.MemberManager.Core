using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class MemberStatusDal : IDal<MemberStatus>
    {
        public async Task<MemberStatus> Fetch(int id)
        {
            return MockDb.MemberStatuses.FirstOrDefault(ms => ms.Id == id);
        }

        public async Task<List<MemberStatus>> Fetch()
        {
            return MockDb.MemberStatuses.ToList();
        }

        public async Task<MemberStatus> Insert(MemberStatus memberStatus)
        {
            var lastMemberStatus = MockDb.MemberStatuses.ToList().OrderByDescending(ms => ms.Id).First();
            memberStatus.Id = 1 + lastMemberStatus.Id;
            memberStatus.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);

            MockDb.MemberStatuses.Add(memberStatus);

            return memberStatus;
        }

        public async Task<MemberStatus> Update(MemberStatus memberStatus)
        {
            var memberStatusToUpdate =
                MockDb.MemberStatuses.FirstOrDefault(em => em.Id == memberStatus.Id &&
                                                           em.RowVersion.SequenceEqual(memberStatus.RowVersion));

            if (memberStatusToUpdate == null)
                throw new Csla.DataPortalException(null);

            memberStatusToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return memberStatusToUpdate;
        }

        public async Task Delete(int id)
        {
            var memberStatusToDelete = MockDb.MemberStatuses.FirstOrDefault(ms => ms.Id == id);
            var listIndex = MockDb.MemberStatuses.IndexOf(memberStatusToDelete);
            if (listIndex > -1)
                MockDb.MemberStatuses.RemoveAt(listIndex);
        }

        public void Dispose()
        {
        }
    }
}