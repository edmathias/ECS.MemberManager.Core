using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class ImageECL : BusinessListBase<ImageECL, ImageEC>
    {
        #region Factory Methods

        internal static async Task<ImageECL> NewImageECL()
        {
            return await DataPortal.CreateChildAsync<ImageECL>();
        }

        internal static async Task<ImageECL> GetImageECL(IList<Image> childData)
        {
            return await DataPortal.FetchChildAsync<ImageECL>(childData);
        }

        #endregion

        #region Data Access

        [FetchChild]
        private async Task Fetch(IList<Image> childData)
        {
            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await ImageEC.GetImageEC(domainObjToAdd);
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