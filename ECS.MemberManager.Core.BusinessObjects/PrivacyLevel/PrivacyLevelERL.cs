﻿using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class PrivacyLevelERL : BusinessListBase<PrivacyLevelERL,PrivacyLevelEC>
    {
        #region Authorization Rules
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        #endregion
       
        #region Factory Methods
        
        public static async Task<PrivacyLevelERL> NewPrivacyLevelERL()
        {
            return await DataPortal.CreateAsync<PrivacyLevelERL>();
        }

        public static async Task<PrivacyLevelERL> GetPrivacyLevelERL()
        {
            return await DataPortal.FetchAsync<PrivacyLevelERL>();
        }
       
        #endregion
        
        #region Data Access
        
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPrivacyLevelDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var PrivacyLevel in childData)
                {
                    var PrivacyLevelToAdd = 
                        await PrivacyLevelEC.GetPrivacyLevelEC(PrivacyLevel);
                    Add(PrivacyLevelToAdd);
                }
            }
        }
        
        [Update]
        private void Update()
        {
            Child_Update();
        }
        
        #endregion
    }
}