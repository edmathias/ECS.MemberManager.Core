using System.Linq;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class MemberStatusDal : IMemberStatusDal
    {
        public MemberStatus Fetch(int id)
        {
            return MockDb.MemberStatuses.FirstOrDefault(ms => ms.Id == id);
        }

        public int Insert(MemberStatus memberStatus)
        {
            var lastMemberStatus = MockDb.MemberStatuses.ToList().OrderByDescending(ms => ms.Id).First();
            memberStatus.Id = lastMemberStatus.Id + 1;
            MockDb.MemberStatuses.Add(memberStatus);
            return memberStatus.Id;
        }

        public void Update(MemberStatus memberStatus)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(MemberStatus memberStatus)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}