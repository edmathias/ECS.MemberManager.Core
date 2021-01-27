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
    public class DocumentTypeROCL : ReadOnlyListBase<DocumentTypeROCL,DocumentTypeROC>
    {
        #region Business Rules
        
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        #endregion
        
        #region Factory Methods
        
        internal static async Task<DocumentTypeROCL> GetDocumentTypeROCL(IList<DocumentType> childData)
        {
            return await DataPortal.FetchChildAsync<DocumentTypeROCL>(childData);
        }

        #endregion 
       
        #region Data Access
        
        [FetchChild]
        private async Task FetchChild(List<DocumentType> childData)
        {
            using (LoadListMode)
            {
                foreach (var documentType in childData)
                {
                    var docTypeToAdd = await DocumentTypeROC.GetDocumentTypeROC(documentType);
                    Add(docTypeToAdd);             
                }
            }
        }
       
        #endregion
    }
}