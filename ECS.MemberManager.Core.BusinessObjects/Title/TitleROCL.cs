


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
    public partial class TitleROCL : ReadOnlyListBase<TitleROCL,TitleROC>
    {
        #region Factory Methods

        internal static async Task<TitleROCL> NewTitleROCL()
        {
            return await DataPortal.CreateChildAsync<TitleROCL>();
        }

        internal static async Task<TitleROCL> GetTitleROCL(List<Title> childData)
        {
            return await DataPortal.FetchChildAsync<TitleROCL>(childData);
        }

        #endregion

        #region Data Access
 
        [FetchChild]
        private async Task Fetch(List<Title> childData)
        {

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await TitleROC.GetTitleROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion

     }
}
