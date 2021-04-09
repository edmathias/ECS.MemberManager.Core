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
        public Task<MemberStatus> Fetch(int id)
        {
            return Task.FromResult(MockDb.MemberStatuses.FirstOrDefault(ms => ms.Id == id));
        }

        public Task<List<MemberStatus>> Fetch()
        {
            return Task.FromResult(MockDb.MemberStatuses.ToList());
        }

        public Task<MemberStatus> Insert(MemberStatus memberStatus)
        {
            var lastMemberStatus = MockDb.MemberStatuses.ToList().OrderByDescending(ms => ms.Id).First();
            memberStatus.Id = 1 + lastMemberStatus.Id;
            memberStatus.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);

            MockDb.MemberStatuses.Add(memberStatus);

            return Task.FromResult(memberStatus);
        }

        public Task<MemberStatus> Update(MemberStatus memberStatus)
        {
            var memberStatusToUpdate =
                MockDb.MemberStatuses.FirstOrDefault(em => em.Id == memberStatus.Id &&
                                                           em.RowVersion.SequenceEqual(memberStatus.RowVersion));

            if (memberStatusToUpdate == null)
                throw new Csla.DataPortalException(null);

            memberStatusToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return Task.FromResult(memberStatusToUpdate);
        }

        public Task Delete(int id)
        {
            var memberStatusToDelete = MockDb.MemberStatuses.FirstOrDefault(ms => ms.Id == id);
            var listIndex = MockDb.MemberStatuses.IndexOf(memberStatusToDelete);
            if (listIndex > -1)
                MockDb.MemberStatuses.RemoveAt(listIndex);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}