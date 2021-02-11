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
    public class EventDocumentROC : ReadOnlyBase<EventDocumentROC>
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

        public static async Task<EventDocumentROC> NewEventDocumentROC()
        {
            return await DataPortal.CreateAsync<EventDocumentROC>();
        }

        public static async Task<EventDocumentROC> GetEventDocumentROC(EventDocument childData)
        {
            return await DataPortal.FetchChildAsync<EventDocumentROC>(childData);
        }

        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(EventDocument childData)
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

        #endregion
    }
}