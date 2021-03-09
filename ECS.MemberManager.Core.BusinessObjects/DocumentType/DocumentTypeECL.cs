


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/09/2021 14:34:26
//******************************************************************************    

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
    public partial class DocumentTypeECL : BusinessListBase<DocumentTypeECL,DocumentTypeEC>
    {
        #region Factory Methods

        internal static async Task<DocumentTypeECL> NewDocumentTypeECL()
        {
            return await DataPortal.CreateChildAsync<DocumentTypeECL>();
        }

        internal static async Task<DocumentTypeECL> GetDocumentTypeECL(List<DocumentType> childData)
        {
            return await DataPortal.FetchChildAsync<DocumentTypeECL>(childData);
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
                    var objectToAdd = await DocumentTypeEC.GetDocumentTypeEC(domainObjToAdd);
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
