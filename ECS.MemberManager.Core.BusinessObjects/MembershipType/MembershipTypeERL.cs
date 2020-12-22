using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class MembershipTypeERL : BusinessListBase<MembershipTypeERL, MembershipTypeEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<MembershipTypeERL> NewMembershipTypeList()
        {
            return await DataPortal.CreateAsync<MembershipTypeERL>();
        }

        public static async Task<MembershipTypeERL> GetMembershipTypeList(IList<MembershipType> listOfChildren)
        {
            return await DataPortal.FetchAsync<MembershipTypeERL>(listOfChildren);
        }

        [Fetch]
        private async void Fetch(IList<MembershipType> listOfChildren)
        {
            RaiseListChangedEvents = false;
            
            foreach (var membershipType in listOfChildren)
            {
                this.Add(await MembershipTypeEC.GetMembershipType(membershipType));
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