


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
    public class MemberInfoROCL : ReadOnlyListBase<MemberInfoROCL, MemberInfoROC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<MemberInfoROCL> GetMemberInfoROCL(List<MemberInfo> childData)
        {
            return await DataPortal.FetchAsync<MemberInfoROCL>(childData);
        }

        [Fetch]
        private async Task Fetch(List<MemberInfo> childData)
        {
            using (LoadListMode)
            {
                foreach (var objectToFetch in childData)
                {
                    var MemberInfoToAdd = await MemberInfoROC.GetMemberInfoROC(objectToFetch);
                    Add(MemberInfoToAdd);
                }
            }
        }
    }
}

