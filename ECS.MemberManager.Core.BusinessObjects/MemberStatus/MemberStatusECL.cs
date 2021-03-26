


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
    public partial class MemberStatusECL : BusinessListBase<MemberStatusECL,MemberStatusEC>
    {
        #region Factory Methods

        internal static async Task<MemberStatusECL> NewMemberStatusECL()
        {
            return await DataPortal.CreateChildAsync<MemberStatusECL>();
        }

        internal static async Task<MemberStatusECL> GetMemberStatusECL(IList<MemberStatus> childData)
        {
            return await DataPortal.FetchChildAsync<MemberStatusECL>(childData);
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
                    var objectToAdd = await MemberStatusEC.GetMemberStatusEC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }
       
        [Update]
        private void Update()
        {
            Child_Update();
        }

        #endregion

     }
}
