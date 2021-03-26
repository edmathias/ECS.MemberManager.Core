


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
    public partial class TermInOfficeECL : BusinessListBase<TermInOfficeECL,TermInOfficeEC>
    {
        #region Factory Methods

        internal static async Task<TermInOfficeECL> NewTermInOfficeECL()
        {
            return await DataPortal.CreateChildAsync<TermInOfficeECL>();
        }

        internal static async Task<TermInOfficeECL> GetTermInOfficeECL(IList<TermInOffice> childData)
        {
            return await DataPortal.FetchChildAsync<TermInOfficeECL>(childData);
        }

        #endregion

        #region Data Access
 
        [FetchChild]
        private async Task Fetch(IList<TermInOffice> childData)
        {

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await TermInOfficeEC.GetTermInOfficeEC(domainObjToAdd);
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
