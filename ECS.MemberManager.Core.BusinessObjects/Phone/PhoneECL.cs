


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
    public partial class PhoneECL : BusinessListBase<PhoneECL,PhoneEC>
    {
        #region Factory Methods

        internal static async Task<PhoneECL> NewPhoneECL()
        {
            return await DataPortal.CreateChildAsync<PhoneECL>();
        }

        internal static async Task<PhoneECL> GetPhoneECL(List<Phone> childData)
        {
            return await DataPortal.FetchChildAsync<PhoneECL>(childData);
        }

        #endregion

        #region Data Access
 
        [FetchChild]
        private async Task Fetch(List<Phone> childData)
        {

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await PhoneEC.GetPhoneEC(domainObjToAdd);
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
