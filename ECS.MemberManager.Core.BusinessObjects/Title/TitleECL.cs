using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class TitleECL : BusinessListBase<TitleECL, TitleEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<TitleECL> NewTitleECL()
        {
            return await DataPortal.CreateAsync<TitleECL>();
        }

        internal static async Task<TitleECL> GetTitleECL(List<Title> childData)
        {
            return await DataPortal.FetchAsync<TitleECL>(childData);
        }

        [Fetch]
        private async Task Fetch(List<Title> childData)
        {
            using (LoadListMode)
            {
                foreach (var title in childData)
                {
                    var titleToAdd = await TitleEC.GetTitleEC(title);
                    Add(titleToAdd);
                }
            }
        }

        [Update]
        private void Update()
        {
            Child_Update();
        }
    }
}