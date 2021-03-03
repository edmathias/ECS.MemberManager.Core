


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/02/2021 21:50:14
//******************************************************************************    

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
    public partial class MembershipTypeECL : BusinessListBase<MembershipTypeECL,MembershipTypeEC>
    {
        #region Factory Methods

        internal static async Task<MembershipTypeECL> NewMembershipTypeECL()
        {
            return await DataPortal.CreateChildAsync<MembershipTypeECL>();
        }

        internal static async Task<MembershipTypeECL> GetMembershipTypeECL(List<MembershipType> childData)
        {
            return await DataPortal.FetchChildAsync<MembershipTypeECL>(childData);
        }

        #endregion

        #region Data Access
 
        [FetchChild]
        private async Task Fetch(List<MembershipType> childData)
        {

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await MembershipTypeEC.GetMembershipTypeEC(domainObjToAdd);
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
