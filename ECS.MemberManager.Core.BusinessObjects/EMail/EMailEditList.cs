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
    public class EMailEditList : BusinessListBase<EMailEditList,EMailEdit>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<EMailEditList> NewEMailEditList()
        {
            return await DataPortal.CreateAsync<EMailEditList>();
        }

        public static async Task<EMailEditList> GetEMailEditList()
        {
            return await DataPortal.FetchAsync<EMailEditList>();
        }

        public static async Task<EMailEditList> GetEMailEditList(List<EMailEdit> childData)
        {
            return await DataPortal.FetchAsync<EMailEditList>(childData);
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
            var dal = dalManager.GetProvider<IEMailDal>();
            var childData = await dal.Fetch();

            await Fetch(childData);

        }

        [Fetch]
        private async Task Fetch(List<EMail> childData)
        {
            using (LoadListMode)
            {
                foreach (var eMailType in childData)
                {
                    var eMailTypeToAdd = await EMailEdit.GetEMailEdit(eMailType);
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