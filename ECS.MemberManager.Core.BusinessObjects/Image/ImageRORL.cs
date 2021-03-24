using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class ImageRORL : ReadOnlyListBase<ImageRORL, ImageROC>
    {
        #region Factory Methods

        public static async Task<ImageRORL> GetImageRORL()
        {
            return await DataPortal.FetchAsync<ImageRORL>();
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task Fetch([Inject] IImageDal dal)
        {
            var childData = await dal.Fetch();

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