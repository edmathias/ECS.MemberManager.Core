


using System; 
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class MemberStatusROCL : ReadOnlyListBase<MemberStatusROCL,MemberStatusROC>
    {
        #region Factory Methods


        internal static async Task<MemberStatusROCL> GetMemberStatusROCL(IList<MemberStatus> childData)
        {
            return await DataPortal.FetchChildAsync<MemberStatusROCL>(childData);
        }

        #endregion

        #region Data Access
 
        [FetchChild]
        private async Task Fetch(IList<MemberStatus> childData)
        {

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
