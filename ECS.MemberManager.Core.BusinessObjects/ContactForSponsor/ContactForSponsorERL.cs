


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 04/01/2021 14:00:36
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
    public partial class ContactForSponsorERL : BusinessListBase<ContactForSponsorERL,ContactForSponsorEC>
    {
        #region Factory Methods

        public static async Task<ContactForSponsorERL> NewContactForSponsorERL()
        {
            return await DataPortal.CreateAsync<ContactForSponsorERL>();
        }

        public static async Task<ContactForSponsorERL> GetContactForSponsorERL( )
        {
            return await DataPortal.FetchAsync<ContactForSponsorERL>();
        }

        #endregion

        #region Data Access
 
        [Fetch]
        private async Task Fetch([Inject] IDal<ContactForSponsor> dal)
        {
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await ContactForSponsorEC.GetContactForSponsorEC(domainObjToAdd);
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
