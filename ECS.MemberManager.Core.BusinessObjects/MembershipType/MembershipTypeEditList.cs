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
    public class MembershipTypeEditList : BusinessListBase<MembershipTypeEditList,MembershipTypeEdit>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<MembershipTypeEditList> NewMembershipTypeEditList()
        {
            return await DataPortal.CreateAsync<MembershipTypeEditList>();
        }

        public static async Task<MembershipTypeEditList> GetMembershipTypeEditList()
        {
            return await DataPortal.FetchAsync<MembershipTypeEditList>();
        }

        public static async Task<MembershipTypeEditList> GetMembershipTypeEditList(List<MembershipType> childData)
        {
            return await DataPortal.FetchAsync<MembershipTypeEditList>(childData);
        }
        
        
        [RunLocal]
        [Create]
        private void Create()
        {
            base.DataPortal_Create();
        }
        
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMembershipTypeDal>();
            var childData = await dal.Fetch();

            await FetchChild(childData);

        }

        [Fetch]
        private async Task FetchChild(List<MembershipType> childData)
        {
            using (LoadListMode)
            {
                foreach (var membershipType in childData)
                {
                    var membershipTypeToAdd = await MembershipTypeEdit.GetMembershipTypeEdit(membershipType);
                    Add(membershipTypeToAdd);
                }
            }
        }
        
        [Update]
        private void Update()
        {
            Child_Update();
        }
    }
}