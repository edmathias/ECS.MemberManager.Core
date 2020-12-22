using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class ContactForSponsorECL : BusinessListBase<ContactForSponsorECL,ContactForSponsorEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<ContactForSponsorECL> NewContactForSponsorList()
        {
            return await DataPortal.CreateChildAsync<ContactForSponsorECL>();
        }

        internal static async Task<ContactForSponsorECL> GetContactForSponsorList(IList<ContactForSponsor> listOfChildren)
        {
            return await DataPortal.FetchChildAsync<ContactForSponsorECL>(listOfChildren);
        }

        [FetchChild]
        private async void Fetch(IList<ContactForSponsor> listOfChildren)
        {
            RaiseListChangedEvents = false;
            
            foreach (var childData in listOfChildren)
            {
                this.Add( await ContactForSponsorEC.GetContactForSponsor(childData)  );
            }

            RaiseListChangedEvents = true;
        }
  
    }
}