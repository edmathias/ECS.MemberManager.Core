using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class ImageROCL : ReadOnlyListBase<ImageROCL, ImageROC>
    {
        #region Factory Methods

        internal static async Task<ImageROCL> GetImageROCL(IList<Image> childData)
        {
            return await DataPortal.FetchChildAsync<ImageROCL>(childData);
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
                    var objectToAdd = await ImageROC.GetImageROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion
    }
}