using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Csla;
using Csla.Serialization.Mobile;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class EMailTypeERL : BusinessListBase<EMailTypeERL,EMailTypeEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<EMailTypeERL> NewEMailTypeList()
        {
            return await DataPortal.CreateChildAsync<EMailTypeERL>();
        }

        public static async Task<EMailTypeERL> GetEMailTypeList()
        {
            return await DataPortal.FetchAsync<EMailTypeERL>();
        }

        [Fetch]
        private async void Fetch()
        {
            RaiseListChangedEvents = false;
           
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailTypeDal>();
            var data = dal.Fetch();

            foreach (var eMailType in data)
            {
                var childData = new EMailType()
                {
                    Id = eMailType.Id,
                    Description = eMailType.Description,
                    Notes = eMailType.Notes
                };
                
                Add(await EMailTypeEC.GetEMailType(childData));             
            }
            
            RaiseListChangedEvents = true;
        }
  
    }
}