


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/09/2021 14:34:52
//******************************************************************************    

using System; 
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class SponsorRORL : ReadOnlyListBase<SponsorRORL,SponsorROC>
    {
        #region Factory Methods


        public static async Task<SponsorRORL> GetSponsorRORL( )
        {
            return await DataPortal.FetchAsync<SponsorRORL>();
        }

        #endregion

        #region Data Access
 
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ISponsorDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await SponsorROC.GetSponsorROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion

     }
}
