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
    public class EventDocumentInfo : ReadOnlyBase<EventDocumentInfo>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);

        public int Id
        {
            get => GetProperty(IdProperty);
            set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<int> EventIdProperty = RegisterProperty<int>(p => p.EventId);

        public int EventId
        {
            get => GetProperty(EventIdProperty);
            set => LoadProperty(EventIdProperty, value);
        }

        public static readonly PropertyInfo<string>
            DocumentNameProperty = RegisterProperty<string>(p => p.DocumentName);

        [Required(), MaxLength(50)]
        public string DocumentName
        {
            get => GetProperty(DocumentNameProperty);
            set => LoadProperty(DocumentNameProperty, value);
        }

        public static readonly PropertyInfo<int> DocumentTypeIdProperty = RegisterProperty<int>(p => p.DocumentTypeId);

        public int DocumentTypeId
        {
            get => GetProperty(DocumentTypeIdProperty);
            set => LoadProperty(DocumentTypeIdProperty, value);
        }

        public static readonly PropertyInfo<string> PathAndFileNameProperty =
            RegisterProperty<string>(p => p.PathAndFileName);

        [Required(), MaxLength(255)]
        public string PathAndFileName
        {
            get => GetProperty(PathAndFileNameProperty);
            set => LoadProperty(PathAndFileNameProperty, value);
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty =
            RegisterProperty<string>(p => p.LastUpdatedBy);

        [Required, MaxLength(255)]
        public string LastUpdatedBy
        {
            get => GetProperty(LastUpdatedByProperty);
            set => LoadProperty(LastUpdatedByProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty =
            RegisterProperty<SmartDate>(p => p.LastUpdatedDate);

        [Required]
        public SmartDate LastUpdatedDate
        {
            get => GetProperty(LastUpdatedDateProperty);
            set => LoadProperty(LastUpdatedDateProperty, value);
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(p => p.Notes);

        public string Notes
        {
            get => GetProperty(NotesProperty);
            set => LoadProperty(NotesProperty, value);
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

        public static async Task<EventDocumentInfo> GetEventDocumentInfo(EventDocument childData)
        {
            return await DataPortal.FetchChildAsync<EventDocumentInfo>(childData);
        }

        public static async Task<EventDocumentInfo> GetEventDocumentInfo(int id)
        {
            return await DataPortal.FetchAsync<EventDocumentInfo>(id);
        }

        #endregion

        #region Data Access Methods

        [Fetch]
        private async Task Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDocumentDal>();
            var data = await dal.Fetch(id);

            Fetch(data);
        }

        [FetchChild]
        private void Fetch(EventDocument childData)
        {
            Id = childData.Id;
            EventId = childData.EventId;
            DocumentName = childData.DocumentName;
            DocumentTypeId = childData.DocumentTypeId;
            PathAndFileName = childData.PathAndFileName;
            LastUpdatedBy = childData.LastUpdatedBy;
            LastUpdatedDate = childData.LastUpdatedDate;
            Notes = childData.Notes;
            RowVersion = childData.RowVersion;
        }

        #endregion
    }
}