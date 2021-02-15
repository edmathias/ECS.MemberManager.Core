


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
    public class OrganizationTypeROCL : ReadOnlyListBase<OrganizationTypeROCL, OrganizationTypeROC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<OrganizationTypeROCL> GetOrganizationTypeROCL(List<OrganizationType> childData)
        {
            return await DataPortal.FetchAsync<OrganizationTypeROCL>(childData);
        }

        [Fetch]
        private async Task Fetch(List<OrganizationType> childData)
        {
            using (LoadListMode)
            {
                foreach (var objectToFetch in childData)
                {
                    var OrganizationTypeToAdd = await OrganizationTypeROC.GetOrganizationTypeROC(objectToFetch);
                    Add(OrganizationTypeToAdd);
                }
            }
        }
    }
}

