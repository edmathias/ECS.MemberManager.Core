using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IDocumentTypeDal : IDisposable
    {
        Task<DocumentType> Fetch(int id);
        Task<List<DocumentType>> Fetch();
        Task<DocumentType> Insert(DocumentType documentType);
        Task<DocumentType> Update(DocumentType documentType );
        Task Delete(int id);
    }
}