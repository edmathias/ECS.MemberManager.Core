﻿


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 04/07/2021 09:32:07
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
    public partial class MemberInfoROCL : ReadOnlyListBase<MemberInfoROCL,MemberInfoROC>
    {
        #region Factory Methods


        internal static async Task<MemberInfoROCL> GetMemberInfoROCL(IList<MemberInfo> childData)
        {
            return await DataPortal.FetchChildAsync<MemberInfoROCL>(childData);
        }

        #endregion

        #region Data Access
 
        [FetchChild]
        private async Task Fetch(IList<MemberInfo> childData)
        {

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await MemberInfoROC.GetMemberInfoROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion

     }
}
