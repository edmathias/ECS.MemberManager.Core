using System;
using System.Collections.Generic;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class TitleDal : ITitleDal
    {
        public Title Fetch(int id)
        {
            return MockDb.Titles.FirstOrDefault(dt => dt.Id == id);
        }

        public List<Title> Fetch()
        {
            return MockDb.Titles.ToList();
        }

        public int Insert( Title documentType)
        {
            var lastTitle = MockDb.Titles.ToList().OrderByDescending(dt => dt.Id).First();
            documentType.Id = ++lastTitle.Id;
            MockDb.Titles.Add(documentType);
            
            return documentType.Id;
        }

        public void Update(Title documentType)
        {
            // mockdb in memory list reference already updated 
        }

        public void Delete(int id)
        {
            var documentTypesToDelete = MockDb.Titles.FirstOrDefault(dt => dt.Id == id);
            var listIndex = MockDb.Titles.IndexOf(documentTypesToDelete);
            if(listIndex > -1)
                MockDb.Titles.RemoveAt(listIndex);
        }

        public void Dispose()
        {
        }
    }
}