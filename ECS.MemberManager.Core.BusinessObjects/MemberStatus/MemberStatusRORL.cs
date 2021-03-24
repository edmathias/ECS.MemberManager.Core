using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class MemberStatusRORL : ReadOnlyListBase<MemberStatusRORL, MemberStatusROC>
    {
        #region Factory Methods

        public static async Task<MemberStatusRORL> GetMemberStatusRORL()
        {
            return await DataPortal.FetchAsync<MemberStatusRORL>();
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task Fetch([Inject] IMemberStatusDal dal)
        {
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await MemberStatusROC.GetMemberStatusROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion
    }
}