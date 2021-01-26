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
    public class AddressERL : BusinessListBase<AddressERL,AddressEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<AddressERL> NewAddressERL()
        {
            return await DataPortal.CreateAsync<AddressERL>();
        }

        public static async Task<AddressERL> GetAddressERL()
        {
            return await DataPortal.FetchAsync<AddressERL>();
        }
        
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IAddressDal>();
            var childData = await dal.Fetch();
            
            using (LoadListMode)
            {
                foreach (var address in childData)
                {
                    var addressToAdd = await AddressEC.GetAddressEC(address);
                    Add(addressToAdd);
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