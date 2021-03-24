//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/23/2021 09:57:45
//******************************************************************************    

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class SponsorECL : BusinessListBase<SponsorECL, SponsorEC>
    {
        #region Factory Methods

        internal static async Task<SponsorECL> NewSponsorECL()
        {
            return await DataPortal.CreateChildAsync<SponsorECL>();
        }

        internal static async Task<SponsorECL> GetSponsorECL(IList<Sponsor> childData)
        {
            return await DataPortal.FetchChildAsync<SponsorECL>(childData);
        }

        #endregion

        #region Data Access

        [FetchChild]
        private async Task Fetch(IList<Sponsor> childData)
        {
            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await SponsorEC.GetSponsorEC(domainObjToAdd);
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