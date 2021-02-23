


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

        public static async Task<DocumentTypeRORL> NewDocumentTypeRORL()
        {
            return await DataPortal.CreateAsync<DocumentTypeRORL>();
        }

        public static async Task<DocumentTypeRORL> GetDocumentTypeRORL( )
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
