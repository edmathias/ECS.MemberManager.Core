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
    public class OfficeECL : BusinessListBase<OfficeECL,OfficeEC>
    {
        #region Authorization Rules
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        #endregion
       
        #region Factory Methods
        
        public static async Task<OfficeECL> NewOfficeECL()
        {
            return await DataPortal.CreateChildAsync<OfficeECL>();
        }

        public static async Task<OfficeECL> GetOfficeECL(List<Office> childData)
        {
            return await DataPortal.FetchChildAsync<OfficeECL>(childData);
        }
       
        #endregion
        
        #region Data Access
        
        [FetchChild]
        private async Task Fetch(List<Office> childData)
        {
            using (LoadListMode)
            {
                foreach (var officeObj in childData)
                {
                    var officeToAdd = 
                        await OfficeEC.GetOfficeEC(officeObj);
                    Add(officeToAdd);
                }
            }
        }
        
        [UpdateChild]
        private void Update()
        {
            Child_Update();
        }
        
        #endregion
    }
}