


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
    public partial class OrganizationTypeECL : BusinessListBase<OrganizationTypeECL, OrganizationTypeEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<OrganizationTypeECL> NewOrganizationTypeECL()
        {
            return await DataPortal.CreateAsync<OrganizationTypeECL>();
        }

        internal static async Task<OrganizationTypeECL> GetOrganizationTypeECL(List<OrganizationType> childData)
        {
            return await DataPortal.FetchAsync<OrganizationTypeECL>(childData);
        }

        [Fetch]
        private async Task Fetch(List<OrganizationType> childData)
        {
            using (LoadListMode)
            {
                foreach (var OrganizationType in childData)
                {
                    var OrganizationTypeToAdd = await OrganizationTypeEC.GetOrganizationTypeEC(OrganizationType);
                    Add(OrganizationTypeToAdd);
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

