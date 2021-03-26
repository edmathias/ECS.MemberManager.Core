

//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/25/2021 11:07:35
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
    public partial class DocumentTypeER : BusinessBase<DocumentTypeER>
    {
        #region Business Methods
 
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public virtual int Id 
        {
            get => GetProperty(IdProperty); 
            private set => LoadProperty(IdProperty, value);    
        }

        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(o => o.Description);
        public virtual string Description 
        {
            get => GetProperty(DescriptionProperty); 
            set => SetProperty(DescriptionProperty, value); 
   
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(o => o.LastUpdatedBy);
        public virtual string LastUpdatedBy 
        {
            get => GetProperty(LastUpdatedByProperty); 
            set => SetProperty(LastUpdatedByProperty, value); 
   
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty = RegisterProperty<SmartDate>(o => o.LastUpdatedDate);
        public virtual SmartDate LastUpdatedDate 
        {
            get => GetProperty(LastUpdatedDateProperty); 
            set => SetProperty(LastUpdatedDateProperty, value); 
   
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(o => o.Notes);
        public virtual string Notes 
        {
            get => GetProperty(NotesProperty); 
            set => SetProperty(NotesProperty, value); 
   
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); 
            set => SetProperty(RowVersionProperty, value); 
   
        }

        #endregion 

        #region Factory Methods
        public static async Task<DocumentTypeER> NewDocumentTypeER()
        {
            return await DataPortal.CreateAsync<DocumentTypeER>();
        }

        public static async Task<DocumentTypeER> GetDocumentTypeER(int id)
        {
            return await DataPortal.FetchAsync<DocumentTypeER>(id);
        }  

        public static async Task DeleteDocumentTypeER(int id)
        {
            await DataPortal.DeleteAsync<DocumentTypeER>(id);
        } 


        #endregion

        #region Data Access Methods

        [Fetch]
        private async Task Fetch(int id, [Inject] IDocumentTypeDal dal)
        {
            var data = await dal.Fetch(id);

            using(BypassPropertyChecks)
            {
            Id = data.Id;
            Description = data.Description;
            LastUpdatedBy = data.LastUpdatedBy;
            LastUpdatedDate = data.LastUpdatedDate;
            Notes = data.Notes;
            RowVersion = data.RowVersion;
            }            
        }
        [Insert]
        private async Task Insert([Inject] IDocumentTypeDal dal)
        {
            FieldManager.UpdateChildren();

            var data = new DocumentType()
            {

                Id = Id,
                Description = Description,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Insert(data);
            Id = insertedObj.Id;
            RowVersion = insertedObj.RowVersion;
        }

       [Update]
        private async Task Update([Inject] IDocumentTypeDal dal)
        {
            FieldManager.UpdateChildren();

            var data = new DocumentType()
            {

                Id = Id,
                Description = Description,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Update(data);
            RowVersion = insertedObj.RowVersion;
        }

        [DeleteSelf]
        private async Task DeleteSelf([Inject] IDocumentTypeDal dal)
        {
            await Delete(Id,dal);
        }
       
        [Delete]
        private async Task Delete(int id, [Inject] IDocumentTypeDal dal)
        {
            await dal.Delete(id);
        }

        #endregion
    }
}
