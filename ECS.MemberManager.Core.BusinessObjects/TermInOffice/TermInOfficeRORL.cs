


using System; 
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class TermInOfficeRORL : ReadOnlyListBase<TermInOfficeRORL,TermInOfficeROC>
    {
        #region Factory Methods


        public static async Task<TermInOfficeRORL> GetTermInOfficeRORL( )
        {
            return await DataPortal.FetchAsync<TermInOfficeRORL>();
        }

        #endregion

        #region Data Access
 
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITermInOfficeDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await TermInOfficeROC.GetTermInOfficeROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion

     }
}
