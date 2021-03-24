//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/23/2021 09:56:58
//******************************************************************************    

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class EMailTypeECL : BusinessListBase<EMailTypeECL, EMailTypeEC>
    {
        #region Factory Methods

        internal static async Task<EMailTypeECL> NewEMailTypeECL()
        {
            return await DataPortal.CreateChildAsync<EMailTypeECL>();
        }

        internal static async Task<EMailTypeECL> GetEMailTypeECL(IList<EMailType> childData)
        {
            return await DataPortal.FetchChildAsync<EMailTypeECL>(childData);
        }

        #endregion

        #region Data Access

        [FetchChild]
        private async Task Fetch(IList<EMailType> childData)
        {
            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await EMailTypeEC.GetEMailTypeEC(domainObjToAdd);
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