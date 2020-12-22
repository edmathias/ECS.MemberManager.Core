using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class EMailTypeECL : BusinessListBase<EMailTypeECL,EMailTypeEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<EMailTypeECL> NewEMailTypeList()
        {
            return await DataPortal.CreateChildAsync<EMailTypeECL>();
        }

        internal static async Task<EMailTypeECL> GetEMailTypeList(IList<EMailType> listOfChildren)
        {
            return await DataPortal.FetchChildAsync<EMailTypeECL>(listOfChildren);
        }

        [FetchChild]
        private async void Fetch(IList<EMailType> listOfChildren)
        {
            RaiseListChangedEvents = false;
            
            foreach (var childData in listOfChildren)
            {
                this.Add( await EMailTypeEC.GetEMailType(childData)  );
            }

            RaiseListChangedEvents = true;
        }
  
    }
}