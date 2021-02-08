using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class PersonROCL : ReadOnlyListBase<PersonROCL,PersonROC>
    {
        #region Business Rules
        
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        #endregion
        
        #region Factory Methods
        
        internal static async Task<PersonROCL> GetPersonROCL(IList<Person> childData)
        {
            return await DataPortal.FetchChildAsync<PersonROCL>(childData);
        }

        #endregion 
       
        #region Data Access
        
        [FetchChild]
        private async Task FetchChild(List<Person> childData)
        {
            using (LoadListMode)
            {
                foreach (var person in childData)
                {
                    var personToAdd = await PersonROC.GetPersonROC(person);
                    Add(personToAdd);             
                }
            }
        }
       
        #endregion
    }
}