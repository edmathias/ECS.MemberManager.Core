﻿using System;
using System.Collections.Generic;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class DocumentTypeDal : IDocumentTypeDal
    {
        public DocumentType Fetch(int id)
        {
            return MockDb.DocumentTypes.FirstOrDefault(dt => dt.Id == id);
        }

        public List<DocumentType> Fetch()
        {
            return MockDb.DocumentTypes.ToList();
        }

        public int Insert( DocumentType documentType)
        {
            var lastDocumentType = MockDb.DocumentTypes.ToList().OrderByDescending(dt => dt.Id).First();
            documentType.Id = 1+lastDocumentType.Id;
            MockDb.DocumentTypes.Add(documentType);
            
            return documentType.Id;
        }

        public void Update(DocumentType documentType)
        {
            // mockdb in memory list reference already updated 
        }

        public void Delete(int id)
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