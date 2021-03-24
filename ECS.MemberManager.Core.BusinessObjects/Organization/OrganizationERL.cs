//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/23/2021 09:57:23
//******************************************************************************    

using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class OrganizationERL : BusinessListBase<OrganizationERL, OrganizationEC>
    {
        #region Factory Methods

        public static async Task<OrganizationERL> NewOrganizationERL()
        {
            return await DataPortal.CreateAsync<OrganizationERL>();
        }

        public static async Task<OrganizationERL> GetOrganizationERL()
        {
            return await DataPortal.FetchAsync<OrganizationERL>();
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task Fetch([Inject] IOrganizationDal dal)
        {
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

        #endregion
    }
}