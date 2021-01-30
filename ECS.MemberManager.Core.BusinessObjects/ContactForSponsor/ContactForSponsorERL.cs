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
    public class ContactForSponsorERL : BusinessListBase<ContactForSponsorERL,ContactForSponsorEC>
    {
        #region Authorization Rules
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        #endregion
       
        #region Factory Methods
        
        public static async Task<ContactForSponsorERL> NewContactForSponsorERL()
        {
            return await DataPortal.CreateAsync<ContactForSponsorERL>();
        }

        public static async Task<ContactForSponsorERL> GetContactForSponsorERL()
        {
            return await DataPortal.FetchAsync<ContactForSponsorERL>();
        }
       
        #endregion
        
        #region Data Access
        
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IContactForSponsorDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var contactForSponsor in childData)
                {
                    var contactForSponsorToAdd = 
                        await ContactForSponsorEC.GetContactForSponsorEC(contactForSponsor);
                    Add(contactForSponsorToAdd);
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