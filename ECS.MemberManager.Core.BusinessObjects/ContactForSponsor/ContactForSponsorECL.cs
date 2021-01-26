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
    public class ContactForSponsorECL : BusinessListBase<ContactForSponsorECL,ContactForSponsorEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<ContactForSponsorECL> NewContactForSponsorECL()
        {
            return await DataPortal.CreateAsync<ContactForSponsorECL>();
        }

        public static async Task<ContactForSponsorECL> GetContactForSponsorECL(List<ContactForSponsor> childData)
        {
            return await DataPortal.FetchAsync<ContactForSponsorECL>(childData);
        }
        
        [RunLocal]
        [Create]
        private void Create()
        {
            base.DataPortal_Create();
        }
        
        [FetchChild]
        private async Task FetchChild()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IContactForSponsorDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var categoryOfPerson in childData)
                {
                    var categoryOfPersonToAdd = 
                        await ContactForSponsorEC.GetContactForSponsorEditChild(categoryOfPerson);
                    Add(categoryOfPersonToAdd);
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