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
    public partial class ContactForSponsorRORL : ReadOnlyListBase<ContactForSponsorRORL,ContactForSponsorROC>
    {
        #region Factory Methods

        public static async Task<ContactForSponsorRORL> NewContactForSponsorRORL()
        {
            return await DataPortal.CreateAsync<ContactForSponsorRORL>();
        }

        public static async Task<ContactForSponsorRORL> GetContactForSponsorRORL( )
        {
            return await DataPortal.FetchAsync<ContactForSponsorRORL>();
        }

        #endregion

        #region Data Access
 
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IContactForSponsorDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await ContactForSponsorROC.GetContactForSponsorROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion

     }
}
