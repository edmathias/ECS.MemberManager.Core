


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 04/07/2021 09:31:30
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
    public partial class CategoryOfPersonERL : BusinessListBase<CategoryOfPersonERL,CategoryOfPersonEC>
    {
        #region Factory Methods

        public static async Task<CategoryOfPersonERL> NewCategoryOfPersonERL()
        {
            return await DataPortal.CreateAsync<CategoryOfPersonERL>();
        }

        public static async Task<CategoryOfPersonERL> GetCategoryOfPersonERL( )
        {
            return await DataPortal.FetchAsync<CategoryOfPersonERL>();
        }

        #endregion

        #region Data Access
 
        [Fetch]
        private async Task Fetch([Inject] IDal<CategoryOfPerson> dal)
        {
            var childData = await dal.Fetch();

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
