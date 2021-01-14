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
    public class ContactForSponsorInfoList : ReadOnlyListBase<ContactForSponsorInfoList, ContactForSponsorInfo>
    {
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<ContactForSponsorInfoList> GetContactForSponsorInfoList(IList<ContactForSponsor> listOfChildren)
        {
            return await DataPortal.FetchAsync<ContactForSponsorInfoList>(listOfChildren);
        }
        
        public static async Task<ContactForSponsorInfoList> GetContactForSponsorInfoList()
        {
            return await DataPortal.FetchAsync<ContactForSponsorInfoList>();
        }        
        
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IContactForSponsorDal>();
            var childData = await dal.Fetch();

            await Fetch(childData);
        }

        [Fetch]
        private async Task Fetch(IList<ContactForSponsor> listOfChildren)
        {
            using (LoadListMode)
            {
                foreach (var categoryOfPersonData in listOfChildren)
                {
                    var category = await
                        ContactForSponsorInfo.GetContactForSponsorInfo(categoryOfPersonData); 
                    this.Add(category);
                }
            }
        }
    }
}