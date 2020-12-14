﻿using System;
using System.Collections.Generic;
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

        public List<MemberStatus> Fetch()
        {
            return MockDb.MemberStatuses.ToList();
        }

        public int Insert( MemberStatus memberStatus)
        {
            var lastMemberStatus = MockDb.MemberStatuses.ToList().OrderByDescending(ms => ms.Id).First();
            memberStatus.Id = 1+lastMemberStatus.Id;
            MockDb.MemberStatuses.Add(memberStatus);
            
            return memberStatus.Id;
        }

        public void Update(MemberStatus memberStatus)
        {
            // mockdb in memory ref list already updated. 
        }

        public void Delete(int id)
        {
            var memberStatusToDelete = MockDb.MemberStatuses.FirstOrDefault(ms => ms.Id == id);
            var listIndex = MockDb.MemberStatuses.IndexOf(memberStatusToDelete);
            if(listIndex > -1)
                MockDb.MemberStatuses.RemoveAt(listIndex);
        }

        public void Dispose()
        {
        }
    }
}