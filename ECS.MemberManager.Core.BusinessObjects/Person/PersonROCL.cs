using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class PersonROCL : ReadOnlyListBase<PersonROCL, PersonROC>
    {
        #region Factory Methods

        internal static async Task<PersonROCL> NewPersonROCL()
        {
            return await DataPortal.CreateChildAsync<PersonROCL>();
        }

        internal static async Task<PersonROCL> GetPersonROCL(List<Person> childData)
        {
            return await DataPortal.FetchChildAsync<PersonROCL>(childData);
        }

        #endregion

        #region Data Access

        [FetchChild]
        private async Task Fetch(List<Person> childData)
        {
            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await PersonROC.GetPersonROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion
    }
}