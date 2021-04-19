using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class DocumentTypeDal : IDal<DocumentType> 
    {
        public Task<DocumentType> Fetch(int id)
        {
            return Task.FromResult(MockDb.DocumentTypes.FirstOrDefault(dt => dt.Id == id));
        }

        public Task<List<DocumentType>> Fetch()
        {
            return Task.FromResult(MockDb.DocumentTypes.ToList());
        }

        public Task<DocumentType> Insert(DocumentType documentType)
        {
            var lastDocumentType = MockDb.DocumentTypes.ToList().OrderByDescending(dt => dt.Id).First();
            documentType.Id = 1 + lastDocumentType.Id;
            documentType.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);

            MockDb.DocumentTypes.Add(documentType);

            return Task.FromResult(documentType);
        }

        public Task<DocumentType> Update(DocumentType documentType)
        {
            var documentTypeToUpdate =
                MockDb.DocumentTypes.FirstOrDefault(em => em.Id == documentType.Id &&
                                                          em.RowVersion.SequenceEqual(documentType.RowVersion));

            if (documentTypeToUpdate == null)
                throw new Csla.DataPortalException(null);

            documentTypeToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return Task.FromResult(documentTypeToUpdate);
        }

        public Task Delete(int id)
        {
            var documentTypesToDelete = MockDb.DocumentTypes.FirstOrDefault(dt => dt.Id == id);
            var listIndex = MockDb.DocumentTypes.IndexOf(documentTypesToDelete);
            if (listIndex > -1)
                MockDb.DocumentTypes.RemoveAt(listIndex);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}