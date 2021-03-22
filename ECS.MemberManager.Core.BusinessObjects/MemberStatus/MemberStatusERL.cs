


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
    public partial class MemberStatusERL : BusinessListBase<MemberStatusERL,MemberStatusEC>
    {
        #region Factory Methods

        public static async Task<MemberStatusERL> NewMemberStatusERL()
        {
            return await DataPortal.CreateAsync<MemberStatusERL>();
        }

        public static async Task<MemberStatusERL> GetMemberStatusERL( )
        {
            return await DataPortal.FetchAsync<MemberStatusERL>();
        }

        #endregion

        #region Data Access
 
        [Fetch]
        private async Task Fetch([Inject] IMemberStatusDal dal)
        {
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await MemberStatusEC.GetMemberStatusEC(domainObjToAdd);
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
