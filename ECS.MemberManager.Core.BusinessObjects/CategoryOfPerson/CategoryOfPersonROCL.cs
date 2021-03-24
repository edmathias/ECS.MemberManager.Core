//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/23/2021 09:56:47
//******************************************************************************    

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class CategoryOfPersonROCL : ReadOnlyListBase<CategoryOfPersonROCL, CategoryOfPersonROC>
    {
        #region Factory Methods

        internal static async Task<CategoryOfPersonROCL> GetCategoryOfPersonROCL(IList<CategoryOfPerson> childData)
        {
            return await DataPortal.FetchChildAsync<CategoryOfPersonROCL>(childData);
        }

        #endregion

        #region Data Access

        [FetchChild]
        private async Task Fetch(IList<CategoryOfPerson> childData)
        {
            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await CategoryOfPersonROC.GetCategoryOfPersonROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion
    }
}