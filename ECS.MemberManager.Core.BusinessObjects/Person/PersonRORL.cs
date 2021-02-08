using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class PersonRORL : ReadOnlyListBase<PersonRORL,PersonROC>
    {
        #region Business Methods
        
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }
        
        #endregion
        
        #region Factory Methods

        public static async Task<PersonRORL> GetPersonRORL()
        {
            return await DataPortal.FetchAsync<PersonRORL>();
        }
        
        #endregion
        
        #region Data Access

        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPersonDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var person in childData)
                {
                    var personToAdd = await PersonROC.GetPersonROC(person);
                    Add(personToAdd);
                }
            }
        }
        
        #endregion
    }
}