﻿


using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class PersonROCL : ReadOnlyListBase<PersonROCL, PersonROC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<PersonROCL> GetPersonROCL(List<Person> childData)
        {
            return await DataPortal.FetchAsync<PersonROCL>(childData);
        }

        [Fetch]
        private async Task Fetch(List<Person> childData)
        {
            using (LoadListMode)
            {
                foreach (var objectToFetch in childData)
                {
                    var PersonToAdd = await PersonROC.GetPersonROC(objectToFetch);
                    Add(PersonToAdd);
                }
            }
        }
    }
}

