using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class MembershipTypeInfoList : ReadOnlyListBase<MembershipTypeInfoList,MembershipTypeInfo>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<MembershipTypeInfoList> GetMembershipTypeInfoList()
        {
            return await DataPortal.FetchAsync<MembershipTypeInfoList>();
        }

        public static async Task<MembershipTypeInfoList> GetMembershipTypeInfoList(List<MembershipType> childData)
        {
            return await DataPortal.FetchAsync<MembershipTypeInfoList>(childData);
        }
        
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMembershipTypeDal>();
            var childData = await dal.Fetch();

            await FetchChild(childData);

        }

        [FetchChild]
        private async Task FetchChild(List<MembershipType> childData)
        {
            using (LoadListMode)
            {
                foreach (var membershipType in childData)
                {
                    var membershipTypeToAdd = await MembershipTypeInfo.GetMembershipTypeInfo(membershipType);
                    Add(membershipTypeToAdd);
                }
            }
        }

    }
}