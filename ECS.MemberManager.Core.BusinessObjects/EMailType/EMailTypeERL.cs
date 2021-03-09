


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/09/2021 14:34:08
//******************************************************************************    

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
    public partial class EMailTypeERL : BusinessListBase<EMailTypeERL,EMailTypeEC>
    {
        #region Factory Methods

        public static async Task<EMailTypeERL> NewEMailTypeERL()
        {
            return await DataPortal.CreateAsync<EMailTypeERL>();
        }

        public static async Task<EMailTypeERL> GetEMailTypeERL( )
        {
            return await DataPortal.FetchAsync<EMailTypeERL>();
        }

        #endregion

        #region Data Access
 
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailTypeDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await EMailTypeEC.GetEMailTypeEC(domainObjToAdd);
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
