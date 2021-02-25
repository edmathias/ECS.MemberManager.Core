


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
    public partial class PersonECL : BusinessListBase<PersonECL,PersonEC>
    {
        #region Factory Methods

        internal static async Task<PersonECL> NewPersonECL()
        {
            return await DataPortal.CreateChildAsync<PersonECL>();
        }

        internal static async Task<PersonECL> GetPersonECL(List<Person> childData)
        {
            return await DataPortal.FetchChildAsync<PersonECL>(childData);
        }

        #endregion

        #region Data Access
 
        [FetchChild]
        private async Task Fetch(List<Person> childData)
        {

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await PersonEC.GetPersonEC(domainObjToAdd);
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
