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
    public class EventDocumentROR : BusinessBase<EventDocumentROR>
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
            private set => LoadProperty(EventProperty, value);
        }


        public static readonly PropertyInfo<string>
            DocumentNameProperty = RegisterProperty<string>(p => p.DocumentName);

        public string DocumentName
        {
            get => GetProperty(DocumentNameProperty);
            private set => LoadProperty(DocumentNameProperty, value);
        }

        public static readonly PropertyInfo<DocumentTypeEC> DocumentTypeProperty =
            RegisterProperty<DocumentTypeEC>(p => p.DocumentType);

        public DocumentTypeEC DocumentType
        {
            get => GetProperty(DocumentTypeProperty);
            private set => LoadProperty(DocumentTypeProperty, value);
        }

        public static readonly PropertyInfo<string> PathAndFileNameProperty =
            RegisterProperty<string>(p => p.PathAndFileName);
        public string PathAndFileName
        {
            get => GetProperty(PathAndFileNameProperty);
            private set => LoadProperty(PathAndFileNameProperty, value);
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty =
            RegisterProperty<string>(p => p.LastUpdatedBy);
        public string LastUpdatedBy
        {
            get => GetProperty(LastUpdatedByProperty);
            private set => LoadProperty(LastUpdatedByProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty =
            RegisterProperty<SmartDate>(p => p.LastUpdatedDate);
        public SmartDate LastUpdatedDate
        {
            get => GetProperty(LastUpdatedDateProperty);
            private set => LoadProperty(LastUpdatedDateProperty, value);
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(p => p.Notes);
        public string Notes
        {
            get => GetProperty(NotesProperty);
            private set => LoadProperty(NotesProperty, value);
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

        public static async Task<EventDocumentROR> NewEventDocumentROR()
        {
            return await DataPortal.CreateAsync<EventDocumentROR>();
        }

        public static async Task<EventDocumentROR> GetEventDocumentROR(EventDocument childData)
        {
            return await DataPortal.FetchChildAsync<EventDocumentROR>(childData);
        }

        public static async Task<EventDocumentROR> GetEventDocumentROR(int id)
        {
            return await DataPortal.FetchAsync<EventDocumentROR>(id);
        }

        public static async Task DeleteEventDocumentROR(int id)
        {
            await DataPortal.DeleteAsync<EventDocumentROR>(id);
        }

        #endregion

        #region Data Access Methods

        [Fetch]
        private async Task Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDocumentDal>();
            var data = await dal.Fetch(id);

            await Fetch(data);
        }

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

        [Insert]
        private async Task Insert()
        {
            await InsertChild();
        }

        [InsertChild]
        private async Task InsertChild()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDocumentDal>();
            var data = new EventDocument()
            {
                Event = Event != null ? new Event() {Id = Event.Id} : null,
                DocumentName = DocumentName,
                DocumentType = DocumentType != null ? new DocumentType() {Id = DocumentType.Id} : null,
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

        [Update]
        private async Task Update()
        {
            await ChildUpdate();
        }

        [UpdateChild]
        private async Task ChildUpdate()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDocumentDal>();

            var emailTypeToUpdate = new EventDocument()
            {
                Id = Id,
                Event = Event != null ? new Event() {Id = Event.Id} : null,
                DocumentName = DocumentName,
                DocumentType = DocumentType != null ? new DocumentType() {Id = DocumentType.Id} : null,
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