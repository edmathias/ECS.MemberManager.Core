using System.Collections.Generic;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class PrivacyLevelDal : IPrivacyLevelDal
    {
        public void Dispose()
        {
        }

        public PrivacyLevel Fetch(int id)
        {
            return MockDb.PrivacyLevels.FirstOrDefault(pl => pl.Id == id);
        }

        public List<PrivacyLevel> Fetch()
        {
            return MockDb.PrivacyLevels.ToList();
        }

        public int Insert(PrivacyLevel privacyLevel)
        {
            var lastPrivacyLevel = MockDb.PrivacyLevels.ToList().OrderByDescending(dt => dt.Id).First();
            privacyLevel.Id = ++lastPrivacyLevel.Id;
            MockDb.PrivacyLevels.Add(privacyLevel);
            
            return privacyLevel.Id;
        }

        public void Update(PrivacyLevel privacyLevel)
        {
           // Mock privacy level reference updated 
        }

        public void Delete(int id)
        {
            var privacyLevelToDelete = MockDb.PrivacyLevels.FirstOrDefault(dt => dt.Id == id);
            var listIndex = MockDb.PrivacyLevels.IndexOf(privacyLevelToDelete);
            if(listIndex > -1)
                MockDb.PrivacyLevels.RemoveAt(listIndex);
        }
    }
}