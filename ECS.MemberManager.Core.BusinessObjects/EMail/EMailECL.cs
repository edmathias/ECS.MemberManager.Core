using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class EMailECL : BusinessListBase<EMailECL, EMailEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<EMailECL> NewEMailECL()
        {
            return await DataPortal.CreateAsync<EMailECL>();
        }

        internal static async Task<EMailECL> GetEMailECL(List<EMail> childData)
        {
            return await DataPortal.FetchAsync<EMailECL>(childData);
        }

        [Fetch]
        private async Task Fetch(List<EMail> childData)
        {
            using (LoadListMode)
            {
                foreach (var EMail in childData)
                {
                    var EMailToAdd = await EMailEC.GetEMailEC(EMail);
                    Add(EMailToAdd);
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