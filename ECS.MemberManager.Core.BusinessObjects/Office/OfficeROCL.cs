


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
    public partial class MembershipTypeROCL : ReadOnlyListBase<MembershipTypeROCL,MembershipTypeROC>
    {
        #region Factory Methods


        internal static async Task<MembershipTypeROCL> GetMembershipTypeROCL(List<MembershipType> childData)
        {
            return await DataPortal.FetchChildAsync<MembershipTypeROCL>(childData);
        }

        #endregion

        #region Data Access
 
        [FetchChild]
        private async Task Fetch(List<MembershipType> childData)
        {

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await MembershipTypeROC.GetMembershipTypeROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion

     }
}
