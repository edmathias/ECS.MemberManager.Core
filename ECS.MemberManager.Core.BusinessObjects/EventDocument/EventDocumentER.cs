﻿

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
    public partial class EventDocumentER : BusinessBase<EventDocumentER>
    {
        #region Business Methods
 
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public virtual int Id 
        {
            get => GetProperty(IdProperty); 
            private set => LoadProperty(IdProperty, value);    
        }


        public static readonly PropertyInfo<EventEC> EventProperty = RegisterProperty<EventEC>(o => o.Event);
        public EventEC Event  
        {
            get => GetProperty(EventProperty); 
            set => SetProperty(EventProperty, value); 
        }    
 
        public static readonly PropertyInfo<string> DocumentNameProperty = RegisterProperty<string>(o => o.DocumentName);
        public virtual string DocumentName 
        {
            get => GetProperty(DocumentNameProperty); 
            set => SetProperty(DocumentNameProperty, value); 
   
        }


        public static readonly PropertyInfo<DocumentTypeEC> DocumentTypeProperty = RegisterProperty<DocumentTypeEC>(o => o.DocumentType);
        public DocumentTypeEC DocumentType  
        {
            get => GetProperty(DocumentTypeProperty); 
            set => SetProperty(DocumentTypeProperty, value); 
        }    
 
        public static readonly PropertyInfo<string> PathAndFileNameProperty = RegisterProperty<string>(o => o.PathAndFileName);
        public virtual string PathAndFileName 
        {
            get => GetProperty(PathAndFileNameProperty); 
            set => SetProperty(PathAndFileNameProperty, value); 
   
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
        public static async Task<EventDocumentER> NewEventDocumentER()
        {
            return await DataPortal.CreateAsync<EventDocumentER>();
        }

        public static async Task<EventDocumentER> GetEventDocumentER(int id)
        {
            return await DataPortal.FetchAsync<EventDocumentER>(id);
        }  

        public static async Task DeleteEventDocumentER(int id)
        {
            await DataPortal.DeleteAsync<EventDocumentER>(id);
        } 


        #endregion

        #region Data Access Methods

        [Fetch]
        private async Task Fetch(int id, [Inject] IDal<EventDocument> dal)
        {
            var data = await dal.Fetch(id);

            using(BypassPropertyChecks)
            {
            Id = data.Id;
            Event = (data.Event != null ? await EventEC.GetEventEC(data.Event) : null);
            DocumentName = data.DocumentName;
            DocumentType = (data.DocumentType != null ? await DocumentTypeEC.GetDocumentTypeEC(data.DocumentType) : null);
            PathAndFileName = data.PathAndFileName;
            LastUpdatedBy = data.LastUpdatedBy;
            LastUpdatedDate = data.LastUpdatedDate;
            Notes = data.Notes;
            RowVersion = data.RowVersion;
            }            
        }
        [Insert]
        private async Task Insert([Inject] IDal<EventDocument> dal)
        {
            FieldManager.UpdateChildren();

            var data = new EventDocument()
            {

                Id = Id,
                Event = (Event != null ? new Event() { Id = Event.Id } : null),
                DocumentName = DocumentName,
                DocumentType = (DocumentType != null ? new DocumentType() { Id = DocumentType.Id } : null),
                PathAndFileName = PathAndFileName,
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
        private async Task Update([Inject] IDal<EventDocument> dal)
        {
            FieldManager.UpdateChildren();

            var data = new EventDocument()
            {

                Id = Id,
                Event = (Event != null ? new Event() { Id = Event.Id } : null),
                DocumentName = DocumentName,
                DocumentType = (DocumentType != null ? new DocumentType() { Id = DocumentType.Id } : null),
                PathAndFileName = PathAndFileName,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Update(data);
            RowVersion = insertedObj.RowVersion;
        }

        [DeleteSelf]
        private async Task DeleteSelf([Inject] IDal<EventDocument> dal)
        {
            await Delete(Id,dal);
        }
       
        [Delete]
        private async Task Delete(int id, [Inject] IDal<EventDocument> dal)
        {
            await dal.Delete(id);
        }

        #endregion
    }
}
