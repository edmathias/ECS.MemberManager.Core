using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class EMailTypeEditList : BusinessListBase<EMailTypeEditList,EMailTypeEdit>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<EMailTypeEditList> NewEMailTypeEditList()
        {
            return await DataPortal.CreateAsync<EMailTypeEditList>();
        }

        public static async Task<EMailTypeEditList> GetEMailTypeEditList()
        {
            return await DataPortal.FetchAsync<EMailTypeEditList>();
        }

        [RunLocal]
        [Create]
        private void Create()
        {
            base.DataPortal_Create();
        }
        
        [Fetch]
        private async void Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailTypeDal>();
            var data = dal.Fetch();

            using (LoadListMode)
            {
                foreach (var eMailType in data)
                {
                    Add(await EMailTypeEdit.GetEMailType(eMailType));             
                }
            }
        }

        [Update]
        private async void Update()
        {
            Child_Update();       
        }
    }
}