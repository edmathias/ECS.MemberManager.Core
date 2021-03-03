


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/03/2021 13:27:05
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
    public partial class ContactForSponsorROCL : ReadOnlyListBase<ContactForSponsorROCL,ContactForSponsorROC>
    {
        #region Factory Methods


        internal static async Task<ContactForSponsorROCL> GetContactForSponsorROCL(List<ContactForSponsor> childData)
        {
            return await DataPortal.FetchChildAsync<ContactForSponsorROCL>(childData);
        }

        #endregion

        #region Data Access
 
        [FetchChild]
        private async Task Fetch(List<ContactForSponsor> childData)
        {

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await ContactForSponsorROC.GetContactForSponsorROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion

     }
}
