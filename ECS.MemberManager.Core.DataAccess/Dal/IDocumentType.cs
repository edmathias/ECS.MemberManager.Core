using System;
using System.Collections.Generic;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IDocumentTypeDal : IDisposable
    {
        DocumentType Fetch(int id);
        List<DocumentType> Fetch();
        int Insert(DocumentType documentType);
        void Update(DocumentType documentType );
        void Delete(int id);
    }
}