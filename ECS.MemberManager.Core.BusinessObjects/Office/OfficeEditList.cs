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
    public class OfficeEditList : BusinessListBase<OfficeEditList,OfficeEdit>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<OfficeEditList> NewOfficeEditList()
        {
            return await DataPortal.CreateAsync<OfficeEditList>();
        }

        public static async Task<OfficeEditList> GetOfficeEditList()
        {
            return await DataPortal.FetchAsync<OfficeEditList>();
        }

        public static async Task<OfficeEditList> GetOfficeEditList(List<OfficeEdit> childData)
        {
            return await DataPortal.FetchAsync<OfficeEditList>(childData);
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
            var dal = dalManager.GetProvider<IOfficeDal>();
            var childData = await dal.Fetch();

            await Fetch(childData);
        }

        [Fetch]
        private async Task Fetch(List<Office> childData)
        {
            using (LoadListMode)
            {
                foreach (var office in childData)
                {
                    var officeToAdd = await OfficeEdit.GetOfficeEdit(office);
                    Add(officeToAdd);
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