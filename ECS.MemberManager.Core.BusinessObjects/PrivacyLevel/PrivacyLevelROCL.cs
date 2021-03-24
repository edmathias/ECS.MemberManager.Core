//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/23/2021 09:57:43
//******************************************************************************    

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class PrivacyLevelROCL : ReadOnlyListBase<PrivacyLevelROCL, PrivacyLevelROC>
    {
        #region Factory Methods

        internal static async Task<PrivacyLevelROCL> GetPrivacyLevelROCL(IList<PrivacyLevel> childData)
        {
            return await DataPortal.FetchChildAsync<PrivacyLevelROCL>(childData);
        }

        #endregion

        #region Data Access

        [FetchChild]
        private async Task Fetch(IList<PrivacyLevel> childData)
        {
            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await PrivacyLevelROC.GetPrivacyLevelROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion
    }
}