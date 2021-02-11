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
    public class OfficeERL : BusinessListBase<OfficeERL,OfficeEC>
    {
        #region Authorization Rules
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        #endregion
       
        #region Factory Methods
        
        public static async Task<OfficeERL> NewOfficeERL()
        {
            return await DataPortal.CreateAsync<OfficeERL>();
        }

        public static async Task<OfficeERL> GetOfficeERL()
        {
            return await DataPortal.FetchAsync<OfficeERL>();
        }
       
        #endregion
        
        #region Data Access
        
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOfficeDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var eventObj in childData)
                {
                    var eventToAdd = 
                        await OfficeEC.GetOfficeEC(eventObj);
                    Add(eventToAdd);
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