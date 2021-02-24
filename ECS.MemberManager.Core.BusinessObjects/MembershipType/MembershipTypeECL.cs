


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
    public partial class OfficeECL : BusinessListBase<OfficeECL,OfficeEC>
    {
        #region Factory Methods

        internal static async Task<OfficeECL> NewOfficeECL()
        {
            return await DataPortal.CreateChildAsync<OfficeECL>();
        }

        internal static async Task<OfficeECL> GetOfficeECL(List<Office> childData)
        {
            return await DataPortal.FetchChildAsync<OfficeECL>(childData);
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
                    var objectToAdd = await OfficeEC.GetOfficeEC(domainObjToAdd);
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
