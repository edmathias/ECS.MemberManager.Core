using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class PersonECL : BusinessListBase<PersonECL, PersonEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<PersonECL> NewPersonECL()
        {
            return await DataPortal.CreateAsync<PersonECL>();
        }

        internal static async Task<PersonECL> GetPersonECL(List<Person> childData)
        {
            return await DataPortal.FetchAsync<PersonECL>(childData);
        }

        [Fetch]
        private async Task Fetch(List<Person> childData)
        {
            using (LoadListMode)
            {
                foreach (var Person in childData)
                {
                    var PersonToAdd = await PersonEC.GetPersonEC(Person);
                    Add(PersonToAdd);
                }
            }
        }

        [Update]
        private void Update()
        {
            Child_Update();
        }
    }
}