


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
    public class OrganizationROCL : ReadOnlyListBase<OrganizationROCL, OrganizationROC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<OrganizationROCL> GetOrganizationROCL(List<Organization> childData)
        {
            return await DataPortal.FetchAsync<OrganizationROCL>(childData);
        }

        [Fetch]
        private async Task Fetch(List<Organization> childData)
        {
            using (LoadListMode)
            {
                foreach (var objectToFetch in childData)
                {
                    var OrganizationToAdd = await OrganizationROC.GetOrganizationROC(objectToFetch);
                    Add(OrganizationToAdd);
                }
            }
        }
    }
}

