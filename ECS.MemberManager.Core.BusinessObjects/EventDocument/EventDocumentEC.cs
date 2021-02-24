

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
    public partial class EventDocumentEC : BusinessBase<EventDocumentEC>
    {
        #region Business Methods
 
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public virtual int Id 
        {
            get => GetProperty(IdProperty); //1-2
            private set => LoadProperty(IdProperty, value); //2-3   
        }


        public static readonly PropertyInfo<EventEC> EventProperty = RegisterProperty<EventEC>(o => o.Event);
        public EventEC Event  
        {
            get => GetProperty(EventProperty); //1-1
            set => SetProperty(EventProperty, value); //2-2
        }    
 
        public static readonly PropertyInfo<string> DocumentNameProperty = RegisterProperty<string>(o => o.DocumentName);
        public virtual string DocumentName 
        {
            get => GetProperty(DocumentNameProperty); //1-2
            set => SetProperty(DocumentNameProperty, value); //2-4
   
        }


        public static readonly PropertyInfo<DocumentTypeEC> DocumentTypeProperty = RegisterProperty<DocumentTypeEC>(o => o.DocumentType);
        public DocumentTypeEC DocumentType  
        {
            get => GetProperty(DocumentTypeProperty); //1-1
            set => SetProperty(DocumentTypeProperty, value); //2-2
        }    
 
        public static readonly PropertyInfo<string> PathAndFileNameProperty = RegisterProperty<string>(o => o.PathAndFileName);
        public virtual string PathAndFileName 
        {
            get => GetProperty(PathAndFileNameProperty); //1-2
            set => SetProperty(PathAndFileNameProperty, value); //2-4
   
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(o => o.LastUpdatedBy);
        public virtual string LastUpdatedBy 
        {
            get => GetProperty(LastUpdatedByProperty); //1-2
            set => SetProperty(LastUpdatedByProperty, value); //2-4
   
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty = RegisterProperty<SmartDate>(o => o.LastUpdatedDate);
        public virtual SmartDate LastUpdatedDate 
        {
            get => GetProperty(LastUpdatedDateProperty); //1-2
            set => SetProperty(LastUpdatedDateProperty, value); //2-4
   
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(o => o.Notes);
        public virtual string Notes 
        {
            get => GetProperty(NotesProperty); //1-2
            set => SetProperty(NotesProperty, value); //2-4
   
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); //1-2
            set => SetProperty(RowVersionProperty, value); //2-4
   
        }

        #endregion 

        #region Factory Methods
        internal static async Task<EventDocumentEC> NewEventDocumentEC()
        {
            return await DataPortal.CreateChildAsync<EventDocumentEC>();
        }

        internal static async Task<EventDocumentEC> GetEventDocumentEC(EventDocument childData)
        {
            return await DataPortal.FetchChildAsync<EventDocumentEC>(childData);
        }  


        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(EventDocument data)
        {
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
        [InsertChild]
        private async Task Insert()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDocumentDal>();
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

       [UpdateChild]
        private async Task Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDocumentDal>();
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
            Id = insertedObj.Id;
            RowVersion = insertedObj.RowVersion;
        }

       
        [DeleteSelfChild]
        private async Task DeleteSelf()
        {
            await Delete(Id);
        }
       
        [Delete]
        private async Task Delete(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDocumentDal>();
           
            await dal.Delete(id);
        }

        #endregion
    }
}
