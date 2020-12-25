using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class PersonECL : BusinessListBase<PersonECL,PersonEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<PersonECL> NewPersonList()
        {
            return await DataPortal.CreateChildAsync<PersonECL>();
        }

        internal static async Task<PersonECL> GetPersonList(IList<Person> listOfChildren)
        {
            return await DataPortal.FetchChildAsync<PersonECL>(listOfChildren);
        }

        [FetchChild]
        private async void Fetch(IList<Person> listOfChildren)
        {
            RaiseListChangedEvents = false;
            
            foreach (var childData in listOfChildren)
            {
                this.Add( await PersonEC.GetPerson(childData)  );
            }

            RaiseListChangedEvents = true;
        }
  
    }
}