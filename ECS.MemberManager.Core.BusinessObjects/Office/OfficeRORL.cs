using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class OfficeRORL : ReadOnlyListBase<OfficeRORL,OfficeROC>
    {
        #region Authorization Rules
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        #endregion
       
        #region Factory Methods

        public static async Task<OfficeRORL> GetOfficeRORL()
        {
            return await DataPortal.FetchAsync<OfficeRORL>();
        }
       
        #endregion
        
        #region Data Access
        
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOfficeDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var officeObj in childData)
                {
                    var officeToAdd = 
                        await OfficeROC.GetOfficeROC(officeObj);
                    Add(officeToAdd);
                }
            }
        }
        
        #endregion
    }
}