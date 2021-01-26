using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class PhoneEditChildList : BusinessListBase<PhoneEditChildList,PhoneEditChild>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<PhoneEditChildList> NewPhoneEditChildList()
        {
            return await DataPortal.CreateAsync<PhoneEditChildList>();
        }

        public static async Task<PhoneEditChildList> GetPhoneEditChildList()
        {
            return await DataPortal.FetchAsync<PhoneEditChildList>();
        }

        public static async Task<PhoneEditChildList> GetPhoneEditChildList(List<PhoneEdit> childData)
        {
            return await DataPortal.FetchAsync<PhoneEditChildList>(childData);
        }
        
        [RunLocal]
        [Create]
        private void Create()
        {
            base.DataPortal_Create();
        }
        
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPhoneDal>();
            var childData = await dal.Fetch();

            await FetchChild(childData);
        }

        [FetchChild]
        private async Task FetchChild(List<Phone> childData)
        {
            using (LoadListMode)
            {
                foreach (var phone in childData)
                {
                    var phoneToAdd = await PhoneEditChild.GetPhoneEditChild(phone);
                    Add(phoneToAdd);
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