using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class DocumentTypeECL : BusinessListBase<DocumentTypeECL,DocumentTypeEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<DocumentTypeECL> NewDocumentTypeECL()
        {
            return await DataPortal.CreateAsync<DocumentTypeECL>();
        }

        public static async Task<DocumentTypeECL> GetDocumentTypeECL(List<DocumentType> childData)
        {
            return await DataPortal.FetchAsync<DocumentTypeECL>(childData);
        }
        
        [Fetch]
        private async Task Fetch(List<DocumentType> childData)
        {
           using (LoadListMode)
            {
                foreach (var documentType in childData)
                {
                    var documentTypeToAdd = await DocumentTypeEC.GetDocumentTypeEC(documentType);
                    Add(documentTypeToAdd);
                }
            }
        }
        
        [Update]
        private void Update()
        {
            Child_Update();
        }
    }
}