using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class DocumentTypeERL : BusinessListBase<DocumentTypeERL, DocumentTypeEC>
    {
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<DocumentTypeERL> NewDocumentTypeList()
        {
            return await DataPortal.CreateAsync<DocumentTypeERL>();
        }

        public static async Task<DocumentTypeERL> GetDocumentTypeList(IList<DocumentType> listOfChildren)
        {
            return await DataPortal.FetchAsync<DocumentTypeERL>(listOfChildren);
        }

        [Fetch]
        private async void Fetch(IList<DocumentType> listOfChildren)
        {
            RaiseListChangedEvents = false;
            
            foreach (var documentTypeData in listOfChildren)
            {
                this.Add(await DocumentTypeEC.GetDocumentType(documentTypeData));
            }
            
            RaiseListChangedEvents = true;
        }

        [Update]
        private void Update()
        {
            base.Child_Update();
        }
            
    }
}