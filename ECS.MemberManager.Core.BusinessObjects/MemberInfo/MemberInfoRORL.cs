﻿


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
    public partial class MemberInfoRORL : ReadOnlyListBase<MemberInfoRORL,MemberInfoROC>
    {
        #region Factory Methods

        public static async Task<MemberInfoRORL> NewMemberInfoRORL()
        {
            return await DataPortal.CreateAsync<MemberInfoRORL>();
        }

        public static async Task<MemberInfoRORL> GetMemberInfoRORL( )
        {
            return await DataPortal.FetchAsync<MemberInfoRORL>();
        }

        #endregion

        #region Data Access
 
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMemberInfoDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await MemberInfoROC.GetMemberInfoROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion

     }
}