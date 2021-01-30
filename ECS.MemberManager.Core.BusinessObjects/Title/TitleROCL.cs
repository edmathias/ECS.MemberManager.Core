using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class TitleROCL : ReadOnlyListBase<TitleROCL,TitleROC>
    {
        #region Business Rules
        
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        #endregion
        
        #region Factory Methods
        
        internal static async Task<TitleROCL> GetTitleROCL(IList<Title> childData)
        {
            return await DataPortal.FetchChildAsync<TitleROCL>(childData);
        }

        #endregion 
       
        #region Data Access
        
        [FetchChild]
        private async Task FetchChild(List<Title> childData)
        {
            using (LoadListMode)
            {
                foreach (var title in childData)
                {
                    var docTypeToAdd = await TitleROC.GetTitleROC(title);
                    Add(docTypeToAdd);             
                }
            }
        }
       
        #endregion
    }
}