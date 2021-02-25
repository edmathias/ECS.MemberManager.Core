


using System; 
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class PersonRORL : ReadOnlyListBase<PersonRORL,PersonROC>
    {
        #region Factory Methods

        public static async Task<PersonRORL> NewPersonRORL()
        {
            return await DataPortal.CreateAsync<PersonRORL>();
        }

        public static async Task<PersonRORL> GetPersonRORL( )
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
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await PersonROC.GetPersonROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion

     }
}
