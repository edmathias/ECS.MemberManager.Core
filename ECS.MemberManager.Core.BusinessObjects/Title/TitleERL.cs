using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class TitleERL : BusinessListBase<TitleERL,TitleEC>
    {
        #region Authorization Rules
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        #endregion
       
        #region Factory Methods
        
        public static async Task<TitleERL> NewTitleERL()
        {
            return await DataPortal.CreateAsync<TitleERL>();
        }

        public static async Task<TitleERL> GetTitleERL()
        {
            return await DataPortal.FetchAsync<TitleERL>();
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
                    var titleToAdd = 
                        await TitleEC.GetTitleEC(title);
                    Add(titleToAdd);
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