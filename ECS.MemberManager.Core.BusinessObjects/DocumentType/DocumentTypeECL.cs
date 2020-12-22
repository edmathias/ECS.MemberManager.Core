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

        internal static async Task<DocumentTypeECL> NewDocumentTypeList()
        {
            return await DataPortal.CreateChildAsync<DocumentTypeECL>();
        }

        internal static async Task<DocumentTypeECL> GetDocumentTypeList(IList<DocumentType> listOfChildren)
        {
            return await DataPortal.FetchChildAsync<DocumentTypeECL>(listOfChildren);
        }

        [FetchChild]
        private async void Fetch(IList<DocumentType> listOfChildren)
        {
            RaiseListChangedEvents = false;
            
            foreach (var childData in listOfChildren)
            {
                this.Add( await DocumentTypeEC.GetDocumentType(childData)  );
            }

            RaiseListChangedEvents = true;
        }
  
    }
}