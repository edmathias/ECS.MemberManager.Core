


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
    public class OrganizationERL : BusinessListBase<OrganizationERL, OrganizationEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }
        
        public static async Task<OrganizationERL> NewOrganizationERL()
        {
            return await DataPortal.CreateAsync<OrganizationERL>();
        }

        internal static async Task<OrganizationERL> GetOrganizationERL()
        {
            return await DataPortal.FetchAsync<OrganizationERL>();
        }

        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOrganizationDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await OrganizationEC.GetOrganizationEC(domainObjToAdd);
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

