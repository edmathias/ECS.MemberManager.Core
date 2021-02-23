﻿


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
    public partial class CategoryOfPersonROCL : ReadOnlyListBase<CategoryOfPersonROCL,CategoryOfPersonROC>
    {
        #region Factory Methods

        internal static async Task<CategoryOfPersonROCL> NewCategoryOfPersonROCL()
        {
            return await DataPortal.CreateChildAsync<CategoryOfPersonROCL>();
        }

        internal static async Task<CategoryOfPersonROCL> GetCategoryOfPersonROCL(List<CategoryOfPerson> childData)
        {
            return await DataPortal.FetchChildAsync<CategoryOfPersonROCL>(childData);
        }

        #endregion

        #region Data Access
 
        [FetchChild]
        private async Task Fetch(List<CategoryOfPerson> childData)
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
