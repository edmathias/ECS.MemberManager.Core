


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 04/14/2021 08:41:27
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
    public partial class PersonalNoteROCL : ReadOnlyListBase<PersonalNoteROCL,PersonalNoteROC>
    {
        #region Factory Methods


        internal static async Task<PersonalNoteROCL> GetPersonalNoteROCL(IList<PersonalNote> childData)
        {
            return await DataPortal.FetchChildAsync<PersonalNoteROCL>(childData);
        }

        #endregion

        #region Data Access
 
        [FetchChild]
        private async Task Fetch(IList<PersonalNote> childData)
        {

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await PersonalNoteROC.GetPersonalNoteROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion

     }
}
