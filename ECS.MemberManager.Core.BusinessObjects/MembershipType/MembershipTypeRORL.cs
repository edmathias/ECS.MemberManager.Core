using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class OfficeRORL : ReadOnlyListBase<OfficeRORL, OfficeROC>
    {
        #region Factory Methods

        public static async Task<OfficeRORL> GetOfficeRORL()
        {
            return await DataPortal.FetchAsync<OfficeRORL>();
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task Fetch([Inject] IOfficeDal dal)
        {
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await OfficeROC.GetOfficeROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion
    }
}