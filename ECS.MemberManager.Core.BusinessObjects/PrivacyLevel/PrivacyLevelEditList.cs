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
    public class PrivacyLevelEditList : BusinessListBase<PrivacyLevelEditList,PrivacyLevelEdit>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<PrivacyLevelEditList> NewPrivacyLevelEditList()
        {
            return await DataPortal.CreateAsync<PrivacyLevelEditList>();
        }

        public static async Task<PrivacyLevelEditList> GetPrivacyLevelEditList()
        {
            return await DataPortal.FetchAsync<PrivacyLevelEditList>();
        }

        public static async Task<PrivacyLevelEditList> GetPrivacyLevelEditList(List<PrivacyLevelEdit> childData)
        {
            return await DataPortal.FetchAsync<PrivacyLevelEditList>(childData);
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
            var dal = dalManager.GetProvider<IPrivacyLevelDal>();
            var childData = await dal.Fetch();

            await Fetch(childData);

        }

        [Fetch]
        private async Task Fetch(List<PrivacyLevel> childData)
        {
            using (LoadListMode)
            {
                foreach (var privacyLevel in childData)
                {
                    var privacyLevelToAdd = await PrivacyLevelEdit.GetPrivacyLevelEdit(privacyLevel);
                    Add(privacyLevelToAdd);
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