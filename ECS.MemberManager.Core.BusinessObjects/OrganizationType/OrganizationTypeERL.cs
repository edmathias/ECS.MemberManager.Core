


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
    public class OrganizationTypeERL : BusinessListBase<OrganizationTypeERL, OrganizationTypeEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }
        
        public static async Task<OrganizationTypeERL> NewOrganizationTypeERL()
        {
            return await DataPortal.CreateAsync<OrganizationTypeERL>();
        }

        internal static async Task<OrganizationTypeERL> GetOrganizationTypeERL()
        {
            return await DataPortal.FetchAsync<OrganizationTypeERL>();
        }

        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOrganizationTypeDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await OrganizationTypeEC.GetOrganizationTypeEC(domainObjToAdd);
                    Add(objectToAdd);
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

