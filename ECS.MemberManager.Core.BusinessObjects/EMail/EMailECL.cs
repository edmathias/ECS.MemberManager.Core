


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
    public partial class EMailECL : BusinessListBase<EMailECL,EMailEC>
    {
        #region Factory Methods

        internal static async Task<EMailECL> NewEMailECL()
        {
            return await DataPortal.CreateChildAsync<EMailECL>();
        }

        internal static async Task<EMailECL> GetEMailECL(List<EMail> childData)
        {
            return await DataPortal.FetchChildAsync<EMailECL>(childData);
        }

        #endregion

        #region Data Access
 
        [FetchChild]
        private async Task Fetch(List<EMail> childData)
        {

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await EMailEC.GetEMailEC(domainObjToAdd);
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
