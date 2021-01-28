using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class PrivacyLevelECL : BusinessListBase<PrivacyLevelECL, PrivacyLevelEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<PrivacyLevelECL> NewPrivacyLevelECL()
        {
            return await DataPortal.CreateAsync<PrivacyLevelECL>();
        }

        internal static async Task<PrivacyLevelECL> GetPrivacyLevelECL(List<PrivacyLevel> childData)
        {
            return await DataPortal.FetchAsync<PrivacyLevelECL>(childData);
        }

        [Fetch]
        private async Task Fetch(List<PrivacyLevel> childData)
        {
            using (LoadListMode)
            {
                foreach (var PrivacyLevel in childData)
                {
                    var PrivacyLevelToAdd = await PrivacyLevelEC.GetPrivacyLevelEC(PrivacyLevel);
                    Add(PrivacyLevelToAdd);
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