using System;
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

        public int Insert( MemberStatus memberStatus)
        {
            var lastMemberStatus = MockDb.MemberStatuses.ToList().OrderByDescending(ms => ms.Id).First();
            var memberStatusToUpdate = new MemberStatus { Id = ++lastMemberStatus.Id, 
                                                          Description = memberStatus.Description, 
                                                          Notes = memberStatus.Notes };
            MockDb.MemberStatuses.Add(memberStatusToUpdate);
            return memberStatusToUpdate.Id;
        }

        public void Update(MemberStatus memberStatus)
        {
            var memberStatusToUpdate = MockDb.MemberStatuses.FirstOrDefault(ms => ms.Id == memberStatus.Id);

            if (memberStatusToUpdate == null) 
                throw new Exception("Record not found");

            memberStatusToUpdate.Description = memberStatus.Description;
            memberStatusToUpdate.Notes = memberStatus.Notes;
        }

        public void Delete(int id)
        {
            var memberStatusToRemove = 
                MockDb.MemberStatuses.FirstOrDefault(ms => ms.Id == id);

            if (memberStatusToRemove != null)
            {
                var indexToRemove = MockDb.MemberStatuses.IndexOf(memberStatusToRemove);

                if (indexToRemove > -1)
                    MockDb.MemberStatuses.RemoveAt(indexToRemove);
                else
                    throw new Exception("MemberStatus not found");
            }
        }

        public void Dispose()
        {
        }
    }
}