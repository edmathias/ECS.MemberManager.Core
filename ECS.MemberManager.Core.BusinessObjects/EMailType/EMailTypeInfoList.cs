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

        public static async Task<EMailTypeInfoList> GetEMailTypeInfoList()
        {
            return await DataPortal.FetchAsync<EMailTypeInfoList>();
        }

        public static async Task<EMailTypeInfoList> GetEMailTypeInfoList(IList<EMailType> listOfChildren)
        {
            return await DataPortal.FetchChildAsync<EMailTypeInfoList>(listOfChildren);
        }

        [RunLocal]
        [Create]
        private void Create()
        {
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
                    Add(await EMailTypeInfo.GetEMailTypeInfo(eMailType));             
                }
            }
        }
        
    }
}