//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/23/2021 09:56:55
//******************************************************************************    

using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class EMailERL : BusinessListBase<EMailERL, EMailEC>
    {
        #region Factory Methods

        public static async Task<EMailERL> NewEMailERL()
        {
            return await DataPortal.CreateAsync<EMailERL>();
        }

        public static async Task<EMailERL> GetEMailERL()
        {
            return await DataPortal.FetchAsync<EMailERL>();
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task Fetch([Inject] IEMailDal dal)
        {
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await EMailEC.GetEMailEC(domainObjToAdd);
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