


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/25/2021 11:07:37
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
    public partial class DocumentTypeRORL : ReadOnlyListBase<DocumentTypeRORL,DocumentTypeROC>
    {
        #region Factory Methods


        public static async Task<DocumentTypeRORL> GetDocumentTypeRORL( )
        {
            return await DataPortal.FetchAsync<DocumentTypeRORL>();
        }

        #endregion

        #region Data Access
 
        [Fetch]
        private async Task Fetch([Inject] IDocumentTypeDal dal)
        {
            var childData = await dal.Fetch();

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
