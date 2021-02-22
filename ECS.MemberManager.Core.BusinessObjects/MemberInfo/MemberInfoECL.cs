


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
    public partial class MemberInfoECL : BusinessListBase<MemberInfoECL,MemberInfoEC>
    {
        #region Factory Methods

        internal static async Task<MemberInfoECL> NewMemberInfoECL()
        {
            return await DataPortal.CreateChildAsync<MemberInfoECL>();
        }

        internal static async Task<MemberInfoECL> GetMemberInfoECL(List<MemberInfo> childData)
        {
            return await DataPortal.FetchChildAsync<MemberInfoECL>(childData);
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
                    var objectToAdd = await MemberInfoEC.GetMemberInfoEC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion

     }
}
