﻿


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
    public partial class OrganizationECL : BusinessListBase<OrganizationECL, OrganizationEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<OrganizationECL> NewOrganizationECL()
        {
            return await DataPortal.CreateAsync<OrganizationECL>();
        }

        internal static async Task<OrganizationECL> GetOrganizationECL(List<Organization> childData)
        {
            return await DataPortal.FetchAsync<OrganizationECL>(childData);
        }

        [Fetch]
        private async Task Fetch(List<Organization> childData)
        {
            using (LoadListMode)
            {
                foreach (var Organization in childData)
                {
                    var OrganizationToAdd = await OrganizationEC.GetOrganizationEC(Organization);
                    Add(OrganizationToAdd);
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

