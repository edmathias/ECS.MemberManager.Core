﻿


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/18/2021 16:28:10
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
    public partial class DocumentTypeERL : BusinessListBase<DocumentTypeERL,DocumentTypeEC>
    {
        #region Factory Methods

        public static async Task<DocumentTypeERL> NewDocumentTypeERL()
        {
            return await DataPortal.CreateAsync<DocumentTypeERL>();
        }

        public static async Task<DocumentTypeERL> GetDocumentTypeERL( )
        {
            return await DataPortal.FetchAsync<DocumentTypeERL>();
        }

        #endregion

        #region Data Access
 
        [Fetch]
        private async Task Fetch([Inject] IDocumentTypeDal dal)
        {
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await DocumentTypeEC.GetDocumentTypeEC(domainObjToAdd);
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
