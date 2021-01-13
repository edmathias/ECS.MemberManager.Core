using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class TitleEditList : BusinessListBase<TitleEditList,TitleEdit>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<TitleEditList> NewTitleEditList()
        {
            return await DataPortal.CreateAsync<TitleEditList>();
        }

        public static async Task<TitleEditList> GetTitleEditList()
        {
            return await DataPortal.FetchAsync<TitleEditList>();
        }

        public static async Task<TitleEditList> GetTitleEditList(List<TitleEdit> childData)
        {
            return await DataPortal.FetchAsync<TitleEditList>(childData);
        }
        
        
        [RunLocal]
        [Create]
        private void Create()
        {
            base.DataPortal_Create();
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
                    var eMailTypeToAdd = await TitleEdit.GetTitleEdit(eMailType);
                    Add(eMailTypeToAdd);
                }
            }
        }
        
        [Update]
        private void Update()
        {
            Child_Update();
        }
    }
}