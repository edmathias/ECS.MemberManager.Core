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
    public class PersonERL : BusinessListBase<PersonERL, PersonEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }
        
        public static async Task<PersonERL> NewPersonERL()
        {
            return await DataPortal.CreateAsync<PersonERL>();
        }

        internal static async Task<PersonERL> GetPersonERL()
        {
            return await DataPortal.FetchAsync<PersonERL>();
        }

        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPersonDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await PersonEC.GetPersonEC(domainObjToAdd);
                    Add(objectToAdd);
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