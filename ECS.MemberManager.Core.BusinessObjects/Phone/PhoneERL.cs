using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class PhoneERL : BusinessListBase<PhoneERL, PhoneEC>
    {
        #region Factory Methods

        public static async Task<PhoneERL> NewPhoneERL()
        {
            return await DataPortal.CreateAsync<PhoneERL>();
        }

        public static async Task<PhoneERL> GetPhoneERL()
        {
            return await DataPortal.FetchAsync<PhoneERL>();
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task Fetch([Inject] IPhoneDal dal)
        {
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await PhoneEC.GetPhoneEC(domainObjToAdd);
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