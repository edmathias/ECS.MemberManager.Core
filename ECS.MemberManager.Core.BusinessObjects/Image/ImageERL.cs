


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
    public partial class ImageERL : BusinessListBase<ImageERL,ImageEC>
    {
        #region Factory Methods

        public static async Task<ImageERL> NewImageERL()
        {
            return await DataPortal.CreateAsync<ImageERL>();
        }

        public static async Task<ImageERL> GetImageERL( )
        {
            return await DataPortal.FetchAsync<ImageERL>();
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
