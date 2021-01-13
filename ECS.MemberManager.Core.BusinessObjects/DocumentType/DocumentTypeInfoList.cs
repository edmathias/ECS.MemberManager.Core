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
    public class DocumentTypeInfoList : ReadOnlyListBase<DocumentTypeInfoList,DocumentTypeInfo>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<DocumentTypeInfoList> GetDocumentTypeInfoList()
        {
            return await DataPortal.FetchAsync<DocumentTypeInfoList>();
        }

        public static async Task<DocumentTypeInfoList> GetDocumentTypeInfoList(IList<DocumentType> listOfChildren)
        {
            return await DataPortal.FetchChildAsync<DocumentTypeInfoList>(listOfChildren);
        }

        [RunLocal]
        [Create]
        private void Create()
        {
        }
        
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IDocumentTypeDal>();
            var childData = await dal.Fetch();

            await Fetch(childData);
        }
 
        [Fetch]
        private async Task Fetch(List<DocumentType> childData)
        {
            using (LoadListMode)
            {
                foreach (var eMailType in childData)
                {
                    Add(await DocumentTypeInfo.GetDocumentTypeInfo(eMailType));             
                }
            }
        }
        
    }
}