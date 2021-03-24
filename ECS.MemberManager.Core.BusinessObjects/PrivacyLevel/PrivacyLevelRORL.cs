﻿//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/23/2021 09:57:44
//******************************************************************************    

using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class PrivacyLevelRORL : ReadOnlyListBase<PrivacyLevelRORL, PrivacyLevelROC>
    {
        #region Factory Methods

        public static async Task<PrivacyLevelRORL> GetPrivacyLevelRORL()
        {
            return await DataPortal.FetchAsync<PrivacyLevelRORL>();
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task Fetch([Inject] IPrivacyLevelDal dal)
        {
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await PrivacyLevelROC.GetPrivacyLevelROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion
    }
}