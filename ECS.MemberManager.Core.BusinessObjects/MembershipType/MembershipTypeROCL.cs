


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
    public partial class OfficeROCL : ReadOnlyListBase<OfficeROCL,OfficeROC>
    {
        #region Factory Methods

        internal static async Task<OfficeROCL> NewOfficeROCL()
        {
            return await DataPortal.CreateChildAsync<OfficeROCL>();
        }

        internal static async Task<OfficeROCL> GetOfficeROCL(List<Office> childData)
        {
            return await DataPortal.FetchChildAsync<OfficeROCL>(childData);
        }

        #endregion

        #region Data Access
 
        [FetchChild]
        private async Task Fetch(List<Office> childData)
        {

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
