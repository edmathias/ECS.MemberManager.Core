


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
    public class OrganizationRORL : ReadOnlyListBase<OrganizationRORL, OrganizationROC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<OrganizationRORL> GetOrganizationRORL()
        {
            return await DataPortal.FetchAsync<OrganizationRORL>();
        }

        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOrganizationDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var objectToFetch in childData)
                {
                    var objectToAdd = await OrganizationROC.GetOrganizationROC(objectToFetch);
                    Add(objectToAdd);
                }
            }
        }
    }
}

