using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class EMailECL : BusinessListBase<EMailECL,EMailEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<EMailECL> NewEMailList()
        {
            return await DataPortal.CreateChildAsync<EMailECL>();
        }

        internal static async Task<EMailECL> GetEMailList(IList<EMail> listOfChildren)
        {
            return await DataPortal.FetchChildAsync<EMailECL>(listOfChildren);
        }

        [FetchChild]
        private async void Fetch(IList<EMail> listOfChildren)
        {
            RaiseListChangedEvents = false;
            
            foreach (var childData in listOfChildren)
            {
                this.Add( await EMailEC.GetEMail(childData)  );
            }

            RaiseListChangedEvents = true;
        }
  
    }
}