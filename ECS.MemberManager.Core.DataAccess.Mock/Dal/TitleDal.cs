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

        public async Task<Title> Insert(Title title)
        {
            var lastTitle = MockDb.Titles.ToList().OrderByDescending(dt => dt.Id).First();
            title.Id = 1 + lastTitle.Id;
            title.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);

            MockDb.Titles.Add(title);

            return title;
        }

        public async Task<Title> Update(Title title)
        {
            var titleToUpdate =
                MockDb.Titles.FirstOrDefault(em => em.Id == title.Id &&
                                                   em.RowVersion.SequenceEqual(title.RowVersion));

            if (titleToUpdate == null)
                throw new Csla.DataPortalException(null);

            titleToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return titleToUpdate;
        }

        public async Task Delete(int id)
        {
            var titleToDelete = MockDb.Titles.FirstOrDefault(dt => dt.Id == id);
            var listIndex = MockDb.Titles.IndexOf(titleToDelete);
            if (listIndex > -1)
                MockDb.Titles.RemoveAt(listIndex);
        }

        public void Dispose()
        {
        }
    }
}