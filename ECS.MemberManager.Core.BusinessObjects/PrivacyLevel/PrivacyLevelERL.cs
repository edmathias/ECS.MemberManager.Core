﻿


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 04/07/2021 09:32:42
//******************************************************************************    

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
    public partial class PrivacyLevelERL : BusinessListBase<PrivacyLevelERL,PrivacyLevelEC>
    {
        #region Factory Methods

        public static async Task<PrivacyLevelERL> NewPrivacyLevelERL()
        {
            return await DataPortal.CreateAsync<PrivacyLevelERL>();
        }

        public static async Task<PrivacyLevelERL> GetPrivacyLevelERL( )
        {
            return await DataPortal.FetchAsync<PrivacyLevelERL>();
        }

        #endregion

        #region Data Access
 
        [Fetch]
        private async Task Fetch([Inject] IDal<PrivacyLevel> dal)
        {
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await PrivacyLevelEC.GetPrivacyLevelEC(domainObjToAdd);
                    Add(objectToAdd);
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
