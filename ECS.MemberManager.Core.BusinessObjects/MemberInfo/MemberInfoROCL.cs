


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
    public partial class MemberInfoROCL : ReadOnlyListBase<MemberInfoROCL,MemberInfoROC>
    {
        #region Factory Methods

        internal static async Task<MemberInfoROCL> NewMemberInfoROCL()
        {
            return await DataPortal.CreateChildAsync<MemberInfoROCL>();
        }

        internal static async Task<MemberInfoROCL> GetMemberInfoROCL(List<MemberInfo> childData)
        {
            return await DataPortal.FetchChildAsync<MemberInfoROCL>(childData);
        }

        #endregion

        #region Data Access
 
        [FetchChild]
        private async Task Fetch(List<MemberInfo> childData)
        {

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await MemberInfoROC.GetMemberInfoROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion

     }
}
