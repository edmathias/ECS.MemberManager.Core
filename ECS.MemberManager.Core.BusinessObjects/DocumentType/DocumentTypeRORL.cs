using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class DocumentTypeRORL : ReadOnlyListBase<DocumentTypeRORL,DocumentTypeROC>
    {
        #region Business Methods
        
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }
        
        #endregion
        
        #region Factory Methods

        public static async Task<DocumentTypeRORL> GetDocumentTypeRORL()
        {
            return await DataPortal.FetchAsync<DocumentTypeRORL>();
        }
        
        #endregion
        
        #region Data Access

        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IDocumentTypeDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var documentType in childData)
                {
                    var documentTypeToAdd = await DocumentTypeROC.GetDocumentTypeROC(documentType);
                    Add(documentTypeToAdd);
                }
            }
        }
        
        #endregion
    }
}