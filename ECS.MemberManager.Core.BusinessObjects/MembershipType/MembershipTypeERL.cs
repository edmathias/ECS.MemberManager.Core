


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
    public partial class OfficeERL : BusinessListBase<OfficeERL,OfficeEC>
    {
        #region Factory Methods

        public static async Task<OfficeERL> NewOfficeERL()
        {
            return await DataPortal.CreateAsync<OfficeERL>();
        }

        public static async Task<OfficeERL> GetOfficeERL( )
        {
            return await DataPortal.FetchAsync<OfficeERL>();
        }

        #endregion

        #region Data Access
 
        [Fetch]
        private async Task Fetch([Inject] IOfficeDal dal)
        {
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await OfficeEC.GetOfficeEC(domainObjToAdd);
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
