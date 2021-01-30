using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class TitleRORL : ReadOnlyListBase<TitleRORL,TitleROC>
    {
        #region Business Methods
        
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }
        
        #endregion
        
        #region Factory Methods

        public static async Task<TitleRORL> GetTitleRORL()
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
                foreach (var title in childData)
                {
                    var titleToAdd = await TitleROC.GetTitleROC(title);
                    Add(titleToAdd);
                }
            }
        }
        
        #endregion
    }
}