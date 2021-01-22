using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class MemberStatusEditList : BusinessListBase<MemberStatusEditList,MemberStatusEdit>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<MemberStatusEditList> NewMemberStatusEditList()
        {
            return await DataPortal.CreateAsync<MemberStatusEditList>();
        }

        public static async Task<MemberStatusEditList> GetMemberStatusEditList()
        {
            return await DataPortal.FetchAsync<MemberStatusEditList>();
        }

        public static async Task<MemberStatusEditList> GetMemberStatusEditList(List<MemberStatusEdit> childData)
        {
            return await DataPortal.FetchAsync<MemberStatusEditList>(childData);
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
            var dal = dalManager.GetProvider<IMemberStatusDal>();
            var childData = await dal.Fetch();

            await Fetch(childData);
        }

        [Fetch]
        private async Task Fetch(List<MemberStatus> childData)
        {
            using (LoadListMode)
            {
                foreach (var memberStatus in childData)
                {
                    var memberStatusToAdd = await MemberStatusEdit.GetMemberStatusEdit(memberStatus);
                    Add(memberStatusToAdd);
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