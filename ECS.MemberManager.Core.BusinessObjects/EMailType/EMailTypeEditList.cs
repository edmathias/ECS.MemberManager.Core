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

        public static async Task<EMailTypeEditList> GetEMailTypeEditList(List<EMailTypeEdit> childData)
        {
            return await DataPortal.FetchAsync<EMailTypeEditList>(childData);
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
            var dal = dalManager.GetProvider<IEMailTypeDal>();
            var childData = await dal.Fetch();

            await Fetch(childData);

        }

        [Fetch]
        private async Task Fetch(List<EMailType> childData)
        {
            using (LoadListMode)
            {
                foreach (var eMailType in childData)
                {
                    var eMailTypeToAdd = await EMailTypeEdit.GetEMailTypeEdit(eMailType);
                    Add(eMailTypeToAdd);
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