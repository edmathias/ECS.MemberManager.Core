


using System; 
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class TitleRORL : ReadOnlyListBase<TitleRORL,TitleROC>
    {
        #region Factory Methods

        public static async Task<TitleRORL> NewTitleRORL()
        {
            return await DataPortal.CreateAsync<TitleRORL>();
        }

        public static async Task<TitleRORL> GetTitleRORL( )
        {
            return await DataPortal.FetchAsync<TitleRORL>();
        }

        #endregion

        #region Data Access
 
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITitleDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await TitleROC.GetTitleROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion

     }
}
