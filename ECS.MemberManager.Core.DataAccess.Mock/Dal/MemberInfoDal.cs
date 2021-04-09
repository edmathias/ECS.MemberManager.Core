using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class MemberInfoDal : IDal<MemberInfo>
    {
        public Task<MemberInfo> Fetch(int id)
        {
            return Task.FromResult(MockDb.MemberInfo.FirstOrDefault(ms => ms.Id == id));
        }

        public Task<List<MemberInfo>> Fetch()
        {
            return Task.FromResult(MockDb.MemberInfo.ToList());
        }

        public Task<MemberInfo> Insert(MemberInfo memberStatus)
        {
            var lastMemberInfo = MockDb.MemberInfo.ToList().OrderByDescending(ms => ms.Id).First();
            memberStatus.Id = 1 + lastMemberInfo.Id;
            memberStatus.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);

            MockDb.MemberInfo.Add(memberStatus);

            return Task.FromResult(memberStatus);
        }

        public Task<MemberInfo> Update(MemberInfo memberInfo)
        {
            var memberInfoToUpdate =
                MockDb.MemberInfo.FirstOrDefault(em => em.Id == memberInfo.Id &&
                                                       em.RowVersion.SequenceEqual(memberInfo.RowVersion));

            if (memberInfoToUpdate == null)
                throw new Csla.DataPortalException(null);

            memberInfoToUpdate.MemberNumber = memberInfo.MemberNumber;
            memberInfoToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return Task.FromResult(memberInfoToUpdate);
        }

        public Task Delete(int id)
        {
            var memberStatusToDelete = MockDb.MemberInfo.FirstOrDefault(ms => ms.Id == id);
            var listIndex = MockDb.MemberInfo.IndexOf(memberStatusToDelete);
            if (listIndex > -1)
                MockDb.MemberInfo.RemoveAt(listIndex);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}