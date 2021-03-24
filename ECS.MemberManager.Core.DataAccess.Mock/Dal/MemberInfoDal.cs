using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class MemberInfoDal : IMemberInfoDal
    {
        public async Task<MemberInfo> Fetch(int id)
        {
            return MockDb.MemberInfo.FirstOrDefault(ms => ms.Id == id);
        }

        public async Task<List<MemberInfo>> Fetch()
        {
            return MockDb.MemberInfo.ToList();
        }

        public async Task<MemberInfo> Insert(MemberInfo memberStatus)
        {
            var lastMemberInfo = MockDb.MemberInfo.ToList().OrderByDescending(ms => ms.Id).First();
            memberStatus.Id = 1 + lastMemberInfo.Id;
            memberStatus.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);

            MockDb.MemberInfo.Add(memberStatus);

            return memberStatus;
        }

        public async Task<MemberInfo> Update(MemberInfo memberInfo)
        {
            var memberInfoToUpdate =
                MockDb.MemberInfo.FirstOrDefault(em => em.Id == memberInfo.Id &&
                                                       em.RowVersion.SequenceEqual(memberInfo.RowVersion));

            if (memberInfoToUpdate == null)
                throw new Csla.DataPortalException(null);

            memberInfoToUpdate.MemberNumber = memberInfo.MemberNumber;
            memberInfoToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return memberInfoToUpdate;
        }

        public async Task Delete(int id)
        {
            var memberStatusToDelete = MockDb.MemberInfo.FirstOrDefault(ms => ms.Id == id);
            var listIndex = MockDb.MemberInfo.IndexOf(memberStatusToDelete);
            if (listIndex > -1)
                MockDb.MemberInfo.RemoveAt(listIndex);
        }

        public void Dispose()
        {
        }
    }
}