using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class EMailTypeECL : BusinessListBase<EMailTypeECL,EMailTypeEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<EMailTypeECL> NewEMailTypeECL()
        {
            return await DataPortal.CreateAsync<EMailTypeECL>();
        }

        internal static async Task<EMailTypeECL> GetEMailTypeECL(List<EMailType> childData)
        {
            return await DataPortal.FetchAsync<EMailTypeECL>(childData);
        }
        
        [Fetch]
        private async Task Fetch(List<EMailType> childData)
        {
           using (LoadListMode)
            {
                foreach (var eMailType in childData)
                {
                    var eMailTypeToAdd = await EMailTypeEC.GetEMailTypeEC(eMailType);
                    Add(eMailTypeToAdd);
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