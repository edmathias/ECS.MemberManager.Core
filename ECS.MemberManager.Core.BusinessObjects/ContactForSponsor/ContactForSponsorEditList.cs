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
    public class ContactForSponsorEditList : BusinessListBase<ContactForSponsorEditList,ContactForSponsorEdit>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<ContactForSponsorEditList> NewContactForSponsorEditList()
        {
            return await DataPortal.CreateAsync<ContactForSponsorEditList>();
        }

        public static async Task<ContactForSponsorEditList> GetContactForSponsorEditList()
        {
            return await DataPortal.FetchAsync<ContactForSponsorEditList>();
        }

        public static async Task<ContactForSponsorEditList> GetContactForSponsorEditList(List<ContactForSponsor> childData)
        {
            return await DataPortal.FetchAsync<ContactForSponsorEditList>(childData);
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
            var dal = dalManager.GetProvider<IContactForSponsorDal>();
            var childData = await dal.Fetch();

            await Fetch(childData);

        }

        [Fetch]
        private async Task Fetch(List<ContactForSponsor> childData)
        {
            using (LoadListMode)
            {
                foreach (var categoryOfPerson in childData)
                {
                    var categoryOfPersonToAdd = 
                        await ContactForSponsorEdit.GetContactForSponsorEdit(categoryOfPerson);
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