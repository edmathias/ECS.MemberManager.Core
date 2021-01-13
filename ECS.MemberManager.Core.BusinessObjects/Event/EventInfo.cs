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
    public class EventInfo : ReadOnlyBase<EventInfo>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);

        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<string> EventNameProperty = RegisterProperty<string>(p => p.EventName);

        [Required, MaxLength(255)]
        public string EventName
        {
            get => GetProperty(EventNameProperty);
            private set => LoadProperty(EventNameProperty, value);
        }

        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(p => p.Description);

        public string Description
        {
            get => GetProperty(DescriptionProperty);
            private set => LoadProperty(DescriptionProperty, value);
        }

        public static readonly PropertyInfo<bool> IsOneTimeProperty = RegisterProperty<bool>(p => p.IsOneTime);

        public bool IsOneTime
        {
            get => GetProperty(IsOneTimeProperty);
            private set => LoadProperty(IsOneTimeProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> NextDateProperty = RegisterProperty<SmartDate>(p => p.NextDate);

        public SmartDate NextDate
        {
            get => GetProperty(NextDateProperty);
            private set => LoadProperty(NextDateProperty, value);
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty =
            RegisterProperty<string>(p => p.LastUpdatedBy);

        [Required, MaxLength(255)]
        public string LastUpdatedBy
        {
            get => GetProperty(LastUpdatedByProperty);
            private set => LoadProperty(LastUpdatedByProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty =
            RegisterProperty<SmartDate>(p => p.LastUpdatedDate);

        [Required]
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

        public static async Task<EventInfo> GetEventInfo(Event childData)
        {
            return await DataPortal.FetchChildAsync<EventInfo>(childData);
        }

        public static async Task<EventInfo> GetEventInfo(int id)
        {
            return await DataPortal.FetchAsync<EventInfo>(id);
        }

        #endregion

        #region Data Access Methods

        [FetchChild]
        private void Fetch(Event childData)
        {
            Id = childData.Id;
            EventName = childData.EventName;
            Description = childData.Description;
            IsOneTime = childData.IsOneTime;
            NextDate = childData.NextDate;
            LastUpdatedBy = childData.LastUpdatedBy;
            LastUpdatedDate = childData.LastUpdatedDate;
            Notes = childData.Notes;
            RowVersion = childData.RowVersion;
        }

        [Fetch]
        private async Task Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDal>();
            var data = await dal.Fetch(id);

            Fetch(data);
        }

        #endregion
    }
}