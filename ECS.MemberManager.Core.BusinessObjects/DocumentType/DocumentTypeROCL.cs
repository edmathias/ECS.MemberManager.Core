


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
    public partial class DocumentTypeROCL : ReadOnlyListBase<DocumentTypeROCL,DocumentTypeROC>
    {
        #region Factory Methods

        internal static async Task<DocumentTypeROCL> NewDocumentTypeROCL()
        {
            return await DataPortal.CreateChildAsync<DocumentTypeROCL>();
        }

        internal static async Task<DocumentTypeROCL> GetDocumentTypeROCL(List<DocumentType> childData)
        {
            return await DataPortal.FetchChildAsync<DocumentTypeROCL>(childData);
        }

        #endregion

        #region Data Access
 
        [FetchChild]
        private async Task Fetch(List<DocumentType> childData)
        {

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await DocumentTypeROC.GetDocumentTypeROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion

     }
}
