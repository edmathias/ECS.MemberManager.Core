


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
    public partial class MembershipTypeERL : BusinessListBase<MembershipTypeERL,MembershipTypeEC>
    {
        #region Factory Methods

        public static async Task<MembershipTypeERL> NewMembershipTypeERL()
        {
            return await DataPortal.CreateAsync<MembershipTypeERL>();
        }

        public static async Task<MembershipTypeERL> GetMembershipTypeERL( )
        {
            return await DataPortal.FetchAsync<MembershipTypeERL>();
        }

        #endregion

        #region Data Access
 
        [Fetch]
        private async Task Fetch([Inject] IDal<MembershipType> dal)
        {
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await MembershipTypeEC.GetMembershipTypeEC(domainObjToAdd);
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
