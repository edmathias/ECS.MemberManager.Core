


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
    public partial class EMailROCL : ReadOnlyListBase<EMailROCL,EMailROC>
    {
        #region Factory Methods

        internal static async Task<EMailROCL> NewEMailROCL()
        {
            return await DataPortal.CreateChildAsync<EMailROCL>();
        }

        internal static async Task<EMailROCL> GetEMailROCL(List<EMail> childData)
        {
            return await DataPortal.FetchChildAsync<EMailROCL>(childData);
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
                    var objectToAdd = await EMailROC.GetEMailROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion

     }
}
