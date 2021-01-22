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
    public class OrganizationTypeEditList : BusinessListBase<OrganizationTypeEditList,OrganizationTypeEdit>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<OrganizationTypeEditList> NewOrganizationTypeEditList()
        {
            return await DataPortal.CreateAsync<OrganizationTypeEditList>();
        }

        public static async Task<OrganizationTypeEditList> GetOrganizationTypeEditList()
        {
            return await DataPortal.FetchAsync<OrganizationTypeEditList>();
        }

        public static async Task<OrganizationTypeEditList> GetOrganizationTypeEditList(List<OrganizationType> childData)
        {
            return await DataPortal.FetchAsync<OrganizationTypeEditList>(childData);
        }
        
        [RunLocal]
        [Create]
        private void Create()
        {
            base.DataPortal_Create();
        }
        
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOrganizationTypeDal>();
            var childData = await dal.Fetch();

            await FetchChild(childData);
        }

        [FetchChild]
        private async Task FetchChild(List<OrganizationType> childData)
        {
            using (LoadListMode)
            {
                foreach (var organizationType in childData)
                {
                    var organizationTypeToAdd = await OrganizationTypeEdit.GetOrganizationTypeEdit(organizationType);
                    Add(organizationTypeToAdd);
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