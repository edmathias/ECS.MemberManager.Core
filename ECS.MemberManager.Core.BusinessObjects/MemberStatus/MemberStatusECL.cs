using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class MemberStatusECL : BusinessListBase<MemberStatusECL, MemberStatusEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<MemberStatusECL> NewMemberStatusECL()
        {
            return await DataPortal.CreateAsync<MemberStatusECL>();
        }

        internal static async Task<MemberStatusECL> GetMemberStatusECL(List<MemberStatus> childData)
        {
            return await DataPortal.FetchAsync<MemberStatusECL>(childData);
        }

        [Fetch]
        private async Task Fetch(List<MemberStatus> childData)
        {
            using (LoadListMode)
            {
                foreach (var memberStatus in childData)
                {
                    var memberStatusToAdd = await MemberStatusEC.GetMemberStatusEC(memberStatus);
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