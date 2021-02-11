using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class OfficeROCL : ReadOnlyListBase<OfficeROCL,OfficeROC>
    {
        #region Authorization Rules
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        #endregion
       
        #region Factory Methods

        public static async Task<OfficeROCL> GetOfficeROCL(List<Office> childData)
        {
            return await DataPortal.FetchChildAsync<OfficeROCL>(childData);
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
                        await OfficeROC.GetOfficeROC(officeObj);
                    Add(officeToAdd);
                }
            }
        }

        #endregion
    }
}