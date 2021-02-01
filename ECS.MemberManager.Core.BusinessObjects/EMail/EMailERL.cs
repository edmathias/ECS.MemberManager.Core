using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class EMailERL : BusinessListBase<EMailERL,EMailEC>
    {
        #region Authorization Rules
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        #endregion
       
        #region Factory Methods
        
        public static async Task<EMailERL> NewEMailERL()
        {
            return await DataPortal.CreateAsync<EMailERL>();
        }

        public static async Task<EMailERL> GetEMailERL()
        {
            return await DataPortal.FetchAsync<EMailERL>();
        }
       
        #endregion
        
        #region Data Access
        
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var EMail in childData)
                {
                    var EMailToAdd = 
                        await EMailEC.GetEMailEC(EMail);
                    Add(EMailToAdd);
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