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
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<DocumentTypeROCL> GetDocumentTypeROCL(IList<DocumentType> listOfChildren)
        {
            return await DataPortal.FetchChildAsync<DocumentTypeROCL>(listOfChildren);
        }


        [Fetch]
        private async Task Fetch(List<DocumentType> childData)
        {
            using (LoadListMode)
            {
                foreach (var eMailType in childData)
                {
                    Add(await DocumentTypeROC.GetDocumentTypeROC(eMailType));             
                }
            }
        }
        
    }
}