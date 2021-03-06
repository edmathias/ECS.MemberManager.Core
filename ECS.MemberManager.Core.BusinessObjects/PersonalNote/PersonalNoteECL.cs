﻿


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 04/14/2021 08:40:04
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
    public partial class PersonalNoteECL : BusinessListBase<PersonalNoteECL,PersonalNoteEC>
    {
        #region Factory Methods

        internal static async Task<PersonalNoteECL> NewPersonalNoteECL()
        {
            return await DataPortal.CreateChildAsync<PersonalNoteECL>();
        }

        internal static async Task<PersonalNoteECL> GetPersonalNoteECL(IList<PersonalNote> childData)
        {
            return await DataPortal.FetchChildAsync<PersonalNoteECL>(childData);
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
