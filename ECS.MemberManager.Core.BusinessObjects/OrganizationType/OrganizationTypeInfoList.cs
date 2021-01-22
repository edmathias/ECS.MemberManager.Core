using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class OrganizationTypeInfoList : ReadOnlyListBase<OrganizationTypeInfoList,OrganizationTypeInfo>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<OrganizationTypeInfoList> NewOrganizationTypeInfoList()
        {
            return await DataPortal.CreateAsync<OrganizationTypeInfoList>();
        }

        public static async Task<OrganizationTypeInfoList> GetOrganizationTypeInfoList()
        {
            return await DataPortal.FetchAsync<OrganizationTypeInfoList>();
        }

        public static async Task<OrganizationTypeInfoList> GetOrganizationTypeInfoList(List<OrganizationType> childData)
        {
            return await DataPortal.FetchAsync<OrganizationTypeInfoList>(childData);
        }

        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOrganizationTypeDal>();
            var childData = await dal.Fetch();

            await FetchChild(childData);
        }

        [Fetch]
        private async Task FetchChild(List<OrganizationType> childData)
        {
            using (LoadListMode)
            {
                foreach (var organizationType in childData)
                {
                    var organizationTypeToAdd = await OrganizationTypeInfo.GetOrganizationTypeInfo(organizationType);
                    Add(organizationTypeToAdd);
                }
            }
        }
    }
}