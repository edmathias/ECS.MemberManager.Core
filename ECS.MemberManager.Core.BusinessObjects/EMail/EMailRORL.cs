//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/23/2021 09:56:57
//******************************************************************************    

using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class EMailRORL : ReadOnlyListBase<EMailRORL, EMailROC>
    {
        #region Factory Methods

        public static async Task<EMailRORL> GetEMailRORL()
        {
            return await DataPortal.FetchAsync<EMailRORL>();
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
                    var objectToAdd = await EMailROC.GetEMailROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion
    }
}