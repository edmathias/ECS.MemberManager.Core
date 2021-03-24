using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class TitleECL : BusinessListBase<TitleECL, TitleEC>
    {
        #region Factory Methods

        internal static async Task<TitleECL> NewTitleECL()
        {
            return await DataPortal.CreateChildAsync<TitleECL>();
        }

        internal static async Task<TitleECL> GetTitleECL(IList<Title> childData)
        {
            return await DataPortal.FetchChildAsync<TitleECL>(childData);
        }

        #endregion

        #region Data Access

        [FetchChild]
        private async Task Fetch(IList<Title> childData)
        {
            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await TitleEC.GetTitleEC(domainObjToAdd);
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