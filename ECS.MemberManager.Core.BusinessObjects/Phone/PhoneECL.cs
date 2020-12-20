using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class PhoneECL : BusinessListBase<PhoneECL, PhoneEC>
    {
        
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<PhoneECL> NewPhoneList()
        {
            return await DataPortal.CreateChildAsync<PhoneECL>();
        }

        internal static async Task<PhoneECL> GetPhoneList(IList<Phone> listOfChildren)
        {
            return await DataPortal.FetchChildAsync<PhoneECL>(listOfChildren);
        }

        [FetchChild]
        private async void FetchChild(IList<Phone> listOfChildren)
        {
            RaiseListChangedEvents = false;
            
            foreach (var phoneData in listOfChildren)
            {
                this.Add(await PhoneEC.GetPhone(phoneData));
            }

            RaiseListChangedEvents = true;
        }
            
    }
}