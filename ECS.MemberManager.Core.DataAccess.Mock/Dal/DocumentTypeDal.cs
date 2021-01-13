using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class DocumentTypeDal : IDocumentTypeDal
    {
        public async Task<DocumentType> Fetch(int id)
        {
            return MockDb.DocumentTypes.FirstOrDefault(dt => dt.Id == id);
        }

        public async Task<List<DocumentType>> Fetch()
        {
            return MockDb.DocumentTypes.ToList();
        }

        public async Task<DocumentType> Insert( DocumentType documentType)
        {
            var lastDocumentType = MockDb.DocumentTypes.ToList().OrderByDescending(dt => dt.Id).First();
            documentType.Id = 1+lastDocumentType.Id;
            MockDb.DocumentTypes.Add(documentType);
            
            return documentType;
        }

        public async Task<DocumentType> Update(DocumentType documentType)
        {
            // mockdb in memory list reference already updated 
            return documentType;
        }

        public async Task Delete(int id)
        {
            var documentTypesToDelete = MockDb.DocumentTypes.FirstOrDefault(dt => dt.Id == id);
            var listIndex = MockDb.DocumentTypes.IndexOf(documentTypesToDelete);
            if(listIndex > -1)
                MockDb.DocumentTypes.RemoveAt(listIndex);
        }

        public void Dispose()
        {
        }
    }
}