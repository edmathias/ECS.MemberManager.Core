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
    public class PhoneEditList : BusinessListBase<PhoneEditList,PhoneEdit>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<PhoneEditList> NewPhoneEditList()
        {
            return await DataPortal.CreateAsync<PhoneEditList>();
        }

        public static async Task<PhoneEditList> GetPhoneEditList()
        {
            return await DataPortal.FetchAsync<PhoneEditList>();
        }

        public static async Task<PhoneEditList> GetPhoneEditList(List<PhoneEdit> childData)
        {
            return await DataPortal.FetchAsync<PhoneEditList>(childData);
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
                    var phoneToAdd = await PhoneEdit.GetPhoneEdit(phone);
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