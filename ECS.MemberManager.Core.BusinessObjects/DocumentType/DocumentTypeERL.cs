﻿


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/08/2021 16:56:32
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
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IDocumentTypeDal>();
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
