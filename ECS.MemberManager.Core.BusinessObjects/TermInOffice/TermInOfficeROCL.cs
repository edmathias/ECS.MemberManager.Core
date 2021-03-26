


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
    public partial class TermInOfficeROCL : ReadOnlyListBase<TermInOfficeROCL,TermInOfficeROC>
    {
        #region Factory Methods


        internal static async Task<TermInOfficeROCL> GetTermInOfficeROCL(IList<TermInOffice> childData)
        {
            return await DataPortal.FetchChildAsync<TermInOfficeROCL>(childData);
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
                    var objectToAdd = await TermInOfficeROC.GetTermInOfficeROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion

     }
}
