


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 04/14/2021 08:41:49
//******************************************************************************    

using System; 
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class PersonalNoteRORL : ReadOnlyListBase<PersonalNoteRORL,PersonalNoteROC>
    {
        #region Factory Methods


        public static async Task<PersonalNoteRORL> GetPersonalNoteRORL( )
        {
            return await DataPortal.FetchAsync<PersonalNoteRORL>();
        }

        #endregion

        #region Data Access
 
        [Fetch]
        private async Task Fetch([Inject] IDal<PersonalNote> dal)
        {
            var childData = await dal.Fetch();

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
