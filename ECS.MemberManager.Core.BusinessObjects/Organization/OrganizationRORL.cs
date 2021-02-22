


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
    public partial class OrganizationRORL : ReadOnlyListBase<OrganizationRORL,OrganizationROC>
    {
        #region Factory Methods

        public static async Task<OrganizationRORL> NewOrganizationRORL()
        {
            return await DataPortal.CreateAsync<OrganizationRORL>();
        }

        public static async Task<OrganizationRORL> GetOrganizationRORL( )
        {
            return await DataPortal.FetchAsync<OrganizationRORL>();
        }

        #endregion

        #region Data Access
 
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
                    var objectToAdd = await OrganizationROC.GetOrganizationROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion

     }
}
