//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/23/2021 09:57:13
//******************************************************************************    

using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class MemberInfoERL : BusinessListBase<MemberInfoERL, MemberInfoEC>
    {
        #region Factory Methods

        public static async Task<MemberInfoERL> NewMemberInfoERL()
        {
            return await DataPortal.CreateAsync<MemberInfoERL>();
        }

        public static async Task<MemberInfoERL> GetMemberInfoERL()
        {
            return await DataPortal.FetchAsync<MemberInfoERL>();
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task Fetch([Inject] IMemberInfoDal dal)
        {
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await MemberInfoEC.GetMemberInfoEC(domainObjToAdd);
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