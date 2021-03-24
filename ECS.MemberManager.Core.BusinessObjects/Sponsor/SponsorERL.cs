//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/23/2021 09:57:45
//******************************************************************************    

using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class SponsorERL : BusinessListBase<SponsorERL, SponsorEC>
    {
        #region Factory Methods

        public static async Task<SponsorERL> NewSponsorERL()
        {
            return await DataPortal.CreateAsync<SponsorERL>();
        }

        public static async Task<SponsorERL> GetSponsorERL()
        {
            return await DataPortal.FetchAsync<SponsorERL>();
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task Fetch([Inject] ISponsorDal dal)
        {
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await SponsorEC.GetSponsorEC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        [Update]
        private void Update()
        {
            Child_Update();
        }

        #endregion
    }
}