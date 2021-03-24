using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class OrganizationTypeERL : BusinessListBase<OrganizationTypeERL, OrganizationTypeEC>
    {
        #region Factory Methods

        public static async Task<OrganizationTypeERL> NewOrganizationTypeERL()
        {
            return await DataPortal.CreateAsync<OrganizationTypeERL>();
        }

        public static async Task<OrganizationTypeERL> GetOrganizationTypeERL()
        {
            return await DataPortal.FetchAsync<OrganizationTypeERL>();
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task Fetch([Inject] IOrganizationTypeDal dal)
        {
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

        #endregion
    }
}