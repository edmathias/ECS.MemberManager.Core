using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class TitleDal : ITitleDal
    {
        public async Task<Title> Fetch(int id)
        {
            return MockDb.Titles.FirstOrDefault(dt => dt.Id == id);
        }

        public async Task<List<Title>> Fetch()
        {
            return MockDb.Titles.ToList();
        }

        public async Task<Title> Insert( Title title)
        {
            var lastTitle = MockDb.Titles.ToList().OrderByDescending(dt => dt.Id).First();
            title.Id = 1+lastTitle.Id;
            MockDb.Titles.Add(title);
            
            return title;
        }

        public async Task<Title> Update(Title title)
        {
            // mockdb in memory list reference already updated 
            return title;
        }

        public async Task Delete(int id)
        {
            var titleToDelete = MockDb.Titles.FirstOrDefault(dt => dt.Id == id);
            var listIndex = MockDb.Titles.IndexOf(titleToDelete);
            if(listIndex > -1)
                MockDb.Titles.RemoveAt(listIndex);
        }

        public void Dispose()
        {
        }
    }
}