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
    public class EMailTypeInfoList : ReadOnlyListBase<EMailTypeInfoList,EMailTypeInfo>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<EMailTypeInfoList> NewEMailTypeList()
        {
            return await DataPortal.CreateChildAsync<EMailTypeInfoList>();
        }

        internal static async Task<EMailTypeInfoList> GetEMailTypeList(IList<EMailType> listOfChildren)
        {
            return await DataPortal.FetchChildAsync<EMailTypeInfoList>(listOfChildren);
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
                    Add(await EMailTypeInfo.GetEMailType(eMailType));             
                }
            }
        }
  
    }
}