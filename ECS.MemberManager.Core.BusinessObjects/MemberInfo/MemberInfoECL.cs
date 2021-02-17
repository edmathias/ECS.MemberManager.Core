


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
    public partial class MemberInfoECL : BusinessListBase<MemberInfoECL, MemberInfoEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<MemberInfoECL> NewMemberInfoECL()
        {
            return await DataPortal.CreateAsync<MemberInfoECL>();
        }

        internal static async Task<MemberInfoECL> GetMemberInfoECL(List<MemberInfo> childData)
        {
            return await DataPortal.FetchAsync<MemberInfoECL>(childData);
        }

        [Fetch]
        private async Task Fetch(List<MemberInfo> childData)
        {
            using (LoadListMode)
            {
                foreach (var MemberInfo in childData)
                {
                    var MemberInfoToAdd = await MemberInfoEC.GetMemberInfoEC(MemberInfo);
                    Add(MemberInfoToAdd);
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

