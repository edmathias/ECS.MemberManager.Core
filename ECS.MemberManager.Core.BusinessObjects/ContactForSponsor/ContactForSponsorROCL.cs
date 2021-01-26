using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class ContactForSponsorROCL : ReadOnlyListBase<ContactForSponsorROCL, ContactForSponsorROC>
    {
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<ContactForSponsorROCL> GetContactForSponsorROCL(IList<ContactForSponsor> listOfChildren)
        {
            return await DataPortal.FetchAsync<ContactForSponsorROCL>(listOfChildren);
        }
        
        public static async Task<ContactForSponsorROCL> GetContactForSponsorROCL()
        {
            return await DataPortal.FetchAsync<ContactForSponsorROCL>();
        }        
        
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IContactForSponsorDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var categoryOfPersonData in childData)
                {
                    var category = await
                        ContactForSponsorROC.GetContactForSponsorROC(categoryOfPersonData); 
                    this.Add(category);
                }
            }
        }
    }
}