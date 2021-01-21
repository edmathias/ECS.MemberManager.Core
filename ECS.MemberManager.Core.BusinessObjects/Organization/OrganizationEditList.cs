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
    public class OrganizationEditList : BusinessListBase<OrganizationEditList,OrganizationEdit>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<OrganizationEditList> NewOrganizationEditList()
        {
            return await DataPortal.CreateAsync<OrganizationEditList>();
        }

        public static async Task<OrganizationEditList> GetOrganizationEditList()
        {
            return await DataPortal.FetchAsync<OrganizationEditList>();
        }

        public static async Task<OrganizationEditList> GetOrganizationEditList(List<OrganizationEdit> childData)
        {
            return await DataPortal.FetchAsync<OrganizationEditList>(childData);
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
                    var eMailTypeToAdd = await OrganizationEdit.GetOrganizationEdit(eMailType);
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