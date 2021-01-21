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
    public class OrganizationInfoList : ReadOnlyListBase<OrganizationInfoList,OrganizationInfo>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<OrganizationInfoList> GetOrganizationInfoList()
        {
            return await DataPortal.FetchAsync<OrganizationInfoList>();
        }

        public static async Task<OrganizationInfoList> GetOrganizationInfoList(List<OrganizationInfo> childData)
        {
            return await DataPortal.FetchAsync<OrganizationInfoList>(childData);
        }
        
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOrganizationDal>();
            var childData = await dal.Fetch();

            await Fetch(childData);

        }

        [Fetch]
        private async Task Fetch(List<Organization> childData)
        {
            using (LoadListMode)
            {
                foreach (var eMailType in childData)
                {
                    var eMailTypeToAdd = await OrganizationInfo.GetOrganizationInfo(eMailType);
                    Add(eMailTypeToAdd);
                }
            }
        }

    }
}