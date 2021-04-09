using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class TitleDal : IDal<Title>
    {
        public Task<Title> Fetch(int id)
        {
            return Task.FromResult(MockDb.Titles.FirstOrDefault(dt => dt.Id == id));
        }

        public Task<List<Title>> Fetch()
        {
            return Task.FromResult(MockDb.Titles.ToList());
        }

        public Task<Title> Insert(Title title)
        {
            var lastTitle = MockDb.Titles.ToList().OrderByDescending(dt => dt.Id).First();
            title.Id = 1 + lastTitle.Id;
            title.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);

            MockDb.Titles.Add(title);

            return Task.FromResult(title);
        }

        public Task<Title> Update(Title title)
        {
            var titleToUpdate =
                MockDb.Titles.FirstOrDefault(em => em.Id == title.Id &&
                                                   em.RowVersion.SequenceEqual(title.RowVersion));

            if (titleToUpdate == null)
                throw new Csla.DataPortalException(null);

            titleToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return Task.FromResult(titleToUpdate);
        }

        public Task Delete(int id)
        {
            var titleToDelete = MockDb.Titles.FirstOrDefault(dt => dt.Id == id);
            var listIndex = MockDb.Titles.IndexOf(titleToDelete);
            if (listIndex > -1)
                MockDb.Titles.RemoveAt(listIndex);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}