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
    public class MembershipTypeERL : BusinessListBase<MembershipTypeERL, MembershipTypeEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<MembershipTypeERL> NewMembershipTypeERL()
        {
            return await DataPortal.CreateAsync<MembershipTypeERL>();
        }

        internal static async Task<MembershipTypeERL> GetMembershipTypeERL()
        {
            return await DataPortal.FetchAsync<MembershipTypeERL>();
        }

        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMembershipTypeDal>();
            var data = await dal.Fetch();            
            
            using (LoadListMode)
            {
                foreach (var eventObj in data)
                {
                    var eventToAdd = await MembershipTypeEC.GetMembershipTypeEC(eventObj);
                    Add(eventToAdd);
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