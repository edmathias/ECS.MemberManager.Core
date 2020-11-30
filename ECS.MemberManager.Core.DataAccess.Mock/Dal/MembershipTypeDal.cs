﻿using System;
using System.Collections.Generic;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class MembershipTypeDal : IMembershipTypeDal
    {
        public MembershipType Fetch(int id)
        {
            return MockDb.MembershipTypes.FirstOrDefault(dt => dt.Id == id);
        }

        public List<MembershipType> Fetch()
        {
            return MockDb.MembershipTypes.ToList();
        }

        public int Insert( MembershipType membershipType)
        {
            var lastMembershipType = MockDb.MembershipTypes.ToList().OrderByDescending(dt => dt.Id).First();
            membershipType.Id = ++lastMembershipType.Id;
            MockDb.MembershipTypes.Add(membershipType);
            
            return membershipType.Id;
        }

        public void Update(MembershipType membershipType)
        {
            // mockdb in memory list reference already updated
        }

        public void Delete(int id)
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