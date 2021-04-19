


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 04/14/2021 08:41:12
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
    public partial class PersonalNoteERL : BusinessListBase<PersonalNoteERL,PersonalNoteEC>
    {
        #region Factory Methods

        public static async Task<PersonalNoteERL> NewPersonalNoteERL()
        {
            return await DataPortal.CreateAsync<PersonalNoteERL>();
        }

        public static async Task<PersonalNoteERL> GetPersonalNoteERL( )
        {
            return await DataPortal.FetchAsync<PersonalNoteERL>();
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
                    var objectToAdd = await PersonalNoteEC.GetPersonalNoteEC(domainObjToAdd);
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
