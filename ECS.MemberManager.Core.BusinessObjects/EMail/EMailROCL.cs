//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/23/2021 09:56:56
//******************************************************************************    

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class EMailROCL : ReadOnlyListBase<EMailROCL, EMailROC>
    {
        #region Factory Methods

        internal static async Task<EMailROCL> GetEMailROCL(IList<EMail> childData)
        {
            return await DataPortal.FetchChildAsync<EMailROCL>(childData);
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
                    var objectToAdd = await EMailROC.GetEMailROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion
    }
}