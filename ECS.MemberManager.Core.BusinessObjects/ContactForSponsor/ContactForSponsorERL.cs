using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class ContactForSponsorERL : BusinessListBase<ContactForSponsorERL, ContactForSponsorEC>
    {
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<ContactForSponsorERL> NewContactForSponsorList()
        {
            return await DataPortal.CreateAsync<ContactForSponsorERL>();
        }

        public static async Task<ContactForSponsorERL> GetContactForSponsorList(IList<ContactForSponsor> listOfChildren)
        {
            return await DataPortal.FetchAsync<ContactForSponsorERL>(listOfChildren);
        }

        [Fetch]
        private async void Fetch(IList<ContactForSponsor> listOfChildren)
        {
            RaiseListChangedEvents = false;
            
            foreach (var addressData in listOfChildren)
            {
                this.Add(await ContactForSponsorEC.GetContactForSponsor(addressData));
            }
            
            RaiseListChangedEvents = true;
        }

        [Update]
        private void Update()
        {
            base.Child_Update();
        }
            
    }
}