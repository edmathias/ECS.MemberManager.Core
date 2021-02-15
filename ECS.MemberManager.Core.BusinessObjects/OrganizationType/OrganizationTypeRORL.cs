


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
    public class OrganizationTypeRORL : ReadOnlyListBase<OrganizationTypeRORL, OrganizationTypeROC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<OrganizationTypeRORL> GetOrganizationTypeRORL()
        {
            return await DataPortal.FetchAsync<OrganizationTypeRORL>();
        }

        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOrganizationTypeDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var objectToFetch in childData)
                {
                    var objectToAdd = await OrganizationTypeROC.GetOrganizationTypeROC(objectToFetch);
                    Add(objectToAdd);
                }
            }
        }
    }
}

