using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class PrivacyLevelDal : IDal<PrivacyLevel>
    {
        public void Dispose()
        {
        }

        public Task<PrivacyLevel> Fetch(int id)
        {
            return Task.FromResult(MockDb.PrivacyLevels.FirstOrDefault(pl => pl.Id == id));
        }

        public Task<List<PrivacyLevel>> Fetch()
        {
            return Task.FromResult(MockDb.PrivacyLevels.ToList());
        }

        public Task<PrivacyLevel> Insert(PrivacyLevel privacyLevel)
        {
            var lastPrivacyLevel = MockDb.PrivacyLevels.ToList().OrderByDescending(dt => dt.Id).First();
            privacyLevel.Id = 1 + lastPrivacyLevel.Id;
            privacyLevel.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);

            MockDb.PrivacyLevels.Add(privacyLevel);

            return Task.FromResult(privacyLevel);
        }

        public Task<PrivacyLevel> Update(PrivacyLevel privacyLevel)
        {
            var privacyLevelToUpdate =
                MockDb.PrivacyLevels.FirstOrDefault(em => em.Id == privacyLevel.Id &&
                                                          em.RowVersion.SequenceEqual(privacyLevel.RowVersion));

            if (privacyLevelToUpdate == null)
                throw new Csla.DataPortalException(null);

            privacyLevelToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return Task.FromResult(privacyLevelToUpdate);
        }

        public Task Delete(int id)
        {
            var privacyLevelToDelete = MockDb.PrivacyLevels.FirstOrDefault(dt => dt.Id == id);
            var listIndex = MockDb.PrivacyLevels.IndexOf(privacyLevelToDelete);
            if (listIndex > -1)
                MockDb.PrivacyLevels.RemoveAt(listIndex);

            return Task.CompletedTask;
        }
    }
}