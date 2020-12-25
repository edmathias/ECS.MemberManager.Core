﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class OrganizationECL : BusinessListBase<OrganizationECL,OrganizationEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<OrganizationECL> NewOrganizationList()
        {
            return await DataPortal.CreateChildAsync<OrganizationECL>();
        }

        internal static async Task<OrganizationECL> GetOrganizationList(IList<Organization> listOfChildren)
        {
            return await DataPortal.FetchChildAsync<OrganizationECL>(listOfChildren);
        }

        [FetchChild]
        private async void Fetch(IList<Organization> listOfChildren)
        {
            RaiseListChangedEvents = false;
            
            foreach (var childData in listOfChildren)
            {
                this.Add( await OrganizationEC.GetOrganization(childData)  );
            }

            RaiseListChangedEvents = true;
        }
        
    }
}