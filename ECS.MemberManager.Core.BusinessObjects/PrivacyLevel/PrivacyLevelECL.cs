﻿


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
    public partial class PrivacyLevelECL : BusinessListBase<PrivacyLevelECL,PrivacyLevelEC>
    {
        #region Factory Methods

        internal static async Task<PrivacyLevelECL> NewPrivacyLevelECL()
        {
            return await DataPortal.CreateChildAsync<PrivacyLevelECL>();
        }

        internal static async Task<PrivacyLevelECL> GetPrivacyLevelECL(List<PrivacyLevel> childData)
        {
            return await DataPortal.FetchChildAsync<PrivacyLevelECL>(childData);
        }

        #endregion

        #region Data Access
 
        [FetchChild]
        private async Task Fetch(List<PrivacyLevel> childData)
        {

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
