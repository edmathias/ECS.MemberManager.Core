using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class TitleInfoList : ReadOnlyListBase<TitleInfoList,TitleInfo>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<TitleInfoList> GetTitleInfoList()
        {
            return await DataPortal.FetchAsync<TitleInfoList>();
        }

        public static async Task<TitleInfoList> GetTitleInfoList(IList<Title> listOfChildren)
        {
            return await DataPortal.FetchChildAsync<TitleInfoList>(listOfChildren);
        }

        [RunLocal]
        [Create]
        private void Create()
        {
        }
        
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITitleDal>();
            var childData = await dal.Fetch();

            await Fetch(childData);
        }
 
        [Fetch]
        private async Task Fetch(List<Title> childData)
        {
            using (LoadListMode)
            {
                foreach (var eMailType in childData)
                {
                    Add(await TitleInfo.GetTitleInfo(eMailType));             
                }
            }
        }
    }
}