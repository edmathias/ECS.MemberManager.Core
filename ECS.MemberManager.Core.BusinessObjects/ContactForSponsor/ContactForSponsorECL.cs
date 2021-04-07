


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 04/07/2021 09:31:34
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
    public partial class ContactForSponsorECL : BusinessListBase<ContactForSponsorECL,ContactForSponsorEC>
    {
        #region Factory Methods

        internal static async Task<ContactForSponsorECL> NewContactForSponsorECL()
        {
            return await DataPortal.CreateChildAsync<ContactForSponsorECL>();
        }

        internal static async Task<ContactForSponsorECL> GetContactForSponsorECL(IList<ContactForSponsor> childData)
        {
            return await DataPortal.FetchChildAsync<ContactForSponsorECL>(childData);
        }

        #endregion

        #region Data Access
 
        [FetchChild]
        private async Task Fetch(IList<ContactForSponsor> childData)
        {

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await ContactForSponsorEC.GetContactForSponsorEC(domainObjToAdd);
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
