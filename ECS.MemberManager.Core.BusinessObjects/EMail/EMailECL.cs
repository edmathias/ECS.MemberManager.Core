//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/23/2021 09:56:54
//******************************************************************************    

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class EMailECL : BusinessListBase<EMailECL, EMailEC>
    {
        #region Factory Methods

        internal static async Task<EMailECL> NewEMailECL()
        {
            return await DataPortal.CreateChildAsync<EMailECL>();
        }

        internal static async Task<EMailECL> GetEMailECL(IList<EMail> childData)
        {
            return await DataPortal.FetchChildAsync<EMailECL>(childData);
        }

        #endregion

        #region Data Access

        [FetchChild]
        private async Task Fetch(IList<EMail> childData)
        {
            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await EMailEC.GetEMailEC(domainObjToAdd);
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