using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class EventDocumentEC : BusinessBase<EventDocumentEC>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);

        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<EventEC> EventProperty = RegisterProperty<EventEC>(p => p.Event);
        public EventEC Event
        {
            get => GetProperty(EventProperty);
            set => SetProperty(EventProperty, value);
        }


        public static readonly PropertyInfo<string> DocumentNameProperty = RegisterProperty<string>(p => p.DocumentName);
        [Required(),MaxLength(50)]
        public string DocumentName
        {
            get => GetProperty(DocumentNameProperty);
            set => SetProperty(DocumentNameProperty, value);
        }

        public static readonly PropertyInfo<DocumentTypeEC> DocumentTypeProperty = RegisterProperty<DocumentTypeEC>(p => p.DocumentType);
        public DocumentTypeEC DocumentType
        {
            get => GetProperty(DocumentTypeProperty);
            set => SetProperty(DocumentTypeProperty, value);
        }

        public static readonly PropertyInfo<string> PathAndFileNameProperty = RegisterProperty<string>(p => p.PathAndFileName);
        [Required(), MaxLength(255)]
        public string PathAndFileName
        {
            get => GetProperty(PathAndFileNameProperty);
            set => SetProperty(PathAndFileNameProperty, value);
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(p => p.LastUpdatedBy);
        [Required,MaxLength(255)]
        public string LastUpdatedBy
        {
            get => GetProperty(LastUpdatedByProperty);
            set => SetProperty(LastUpdatedByProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty = RegisterProperty<SmartDate>(p => p.LastUpdatedDate);
        [Required] 
        public SmartDate LastUpdatedDate
        {
            get => GetProperty(LastUpdatedDateProperty);
            set => SetProperty(LastUpdatedDateProperty, value);
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(p => p.Notes);
        public string Notes
        {
            get => GetProperty(NotesProperty);
            set => SetProperty(NotesProperty, value);
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(p => p.RowVersion);

        public byte[] RowVersion
        {
            get => GetProperty(RowVersionProperty);
            private set => LoadProperty(RowVersionProperty, value);
        }

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();

            // TODO: add business rules
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        #endregion

        #region Factory Methods

        public static async Task<EventDocumentEC> NewEventDocumentEC()
        {
            return await DataPortal.CreateAsync<EventDocumentEC>();
        }

        public static async Task<EventDocumentEC> GetEventDocumentEC(EventDocument childData)
        {
            return await DataPortal.FetchChildAsync<EventDocumentEC>(childData);
        }

        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(EventDocument childData)
        {
            using (BypassPropertyChecks)
            {
                Id = childData.Id;
                Event = childData.Event != null ? await EventEC.GetEventEC(childData.Event) : null;
                DocumentName = childData.DocumentName;
                DocumentType = childData.DocumentType != null
                    ? await DocumentTypeEC.GetDocumentTypeEC(childData.DocumentType)
                    : null;
                PathAndFileName = childData.PathAndFileName;
                LastUpdatedBy = childData.LastUpdatedBy;
                LastUpdatedDate = childData.LastUpdatedDate;
                Notes = childData.Notes;
                RowVersion = childData.RowVersion;
            }
        }

        [InsertChild]
        private async Task InsertChild()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDocumentDal>();
            var data = new EventDocument()
            {
                Event = new Event() { Id = Event.Id },
                DocumentName = DocumentName,
                DocumentType = DocumentType != null ? new DocumentType() { Id = DocumentType.Id } : null,
                PathAndFileName = PathAndFileName,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
                RowVersion = RowVersion
            };

            var insertedEventDocument = await dal.Insert(data);
            Id = insertedEventDocument.Id;
            RowVersion = insertedEventDocument.RowVersion;
        }

        [UpdateChild]
        private async Task ChildUpdate()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDocumentDal>();

            var emailTypeToUpdate = new EventDocument()
            {
                Id = Id,
                Event = Event != null ? new Event() { Id = Event.Id } : null,
                DocumentName = DocumentName,
                DocumentType = DocumentType != null ? new DocumentType() { Id = DocumentType.Id } : null,
                PathAndFileName = PathAndFileName,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
                RowVersion = RowVersion
            };

            var updatedEmail = await dal.Update(emailTypeToUpdate);
            RowVersion = updatedEmail.RowVersion;
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