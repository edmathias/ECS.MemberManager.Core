


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
    public partial class TermInOfficeERL : BusinessListBase<TermInOfficeERL,TermInOfficeEC>
    {
        #region Factory Methods

        public static async Task<TermInOfficeERL> NewTermInOfficeERL()
        {
            return await DataPortal.CreateAsync<TermInOfficeERL>();
        }

        public static async Task<TermInOfficeERL> GetTermInOfficeERL( )
        {
            return await DataPortal.FetchAsync<TermInOfficeERL>();
        }

        #endregion

        #region Data Access
 
        [Fetch]
        private async Task Fetch([Inject] IDal<TermInOffice> dal)
        {
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await TermInOfficeEC.GetTermInOfficeEC(domainObjToAdd);
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
