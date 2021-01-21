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
    public class OfficeInfoList : ReadOnlyListBase<OfficeInfoList,OfficeInfo>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<OfficeInfoList> GetOfficeInfoList()
        {
            return await DataPortal.FetchAsync<OfficeInfoList>();
        }

        public static async Task<OfficeInfoList> GetOfficeInfoList(List<OfficeInfo> childData)
        {
            return await DataPortal.FetchAsync<OfficeInfoList>(childData);
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
                    var officeToAdd = await OfficeInfo.GetOfficeInfo(office);
                    Add(officeToAdd);
                }
            }
        }
    }
}