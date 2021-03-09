


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/09/2021 14:33:57
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
    public partial class CategoryOfPersonECL : BusinessListBase<CategoryOfPersonECL,CategoryOfPersonEC>
    {
        #region Factory Methods

        internal static async Task<CategoryOfPersonECL> NewCategoryOfPersonECL()
        {
            return await DataPortal.CreateChildAsync<CategoryOfPersonECL>();
        }

        internal static async Task<CategoryOfPersonECL> GetCategoryOfPersonECL(List<CategoryOfPerson> childData)
        {
            return await DataPortal.FetchChildAsync<CategoryOfPersonECL>(childData);
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
                    var objectToAdd = await CategoryOfPersonEC.GetCategoryOfPersonEC(domainObjToAdd);
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
