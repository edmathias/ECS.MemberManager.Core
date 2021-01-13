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
    public class DocumentTypeEditList : BusinessListBase<DocumentTypeEditList,DocumentTypeEdit>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<DocumentTypeEditList> NewDocumentTypeEditList()
        {
            return await DataPortal.CreateAsync<DocumentTypeEditList>();
        }

        public static async Task<DocumentTypeEditList> GetDocumentTypeEditList()
        {
            return await DataPortal.FetchAsync<DocumentTypeEditList>();
        }

        public static async Task<DocumentTypeEditList> GetDocumentTypeEditList(List<DocumentType> childData)
        {
            return await DataPortal.FetchAsync<DocumentTypeEditList>(childData);
        }
        
        
        [RunLocal]
        [Create]
        private void Create()
        {
            base.DataPortal_Create();
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
                    var eMailTypeToAdd = await DocumentTypeEdit.GetDocumentTypeEdit(eMailType);
                    Add(eMailTypeToAdd);
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