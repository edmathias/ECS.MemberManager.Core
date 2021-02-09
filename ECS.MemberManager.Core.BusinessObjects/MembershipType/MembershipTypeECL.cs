using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class MembershipTypeECL : BusinessListBase<MembershipTypeECL, MembershipTypeEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<MembershipTypeECL> NewMembershipTypeECL()
        {
            return await DataPortal.CreateAsync<MembershipTypeECL>();
        }

        internal static async Task<MembershipTypeECL> GetMembershipTypeECL(List<MembershipType> childData)
        {
            return await DataPortal.FetchAsync<MembershipTypeECL>(childData);
        }

        [Fetch]
        private async Task Fetch(List<MembershipType> childData)
        {
            using (LoadListMode)
            {
                foreach (var eventObj in childData)
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