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
    public class EventEC : BusinessBase<EventEC>
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
            set => SetProperty(EventNameProperty, value);
        }

        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(p => p.Description);

        public string Description
        {
            get => GetProperty(DescriptionProperty);
            set => SetProperty(DescriptionProperty, value);
        }

        public static readonly PropertyInfo<bool> IsOneTimeProperty = RegisterProperty<bool>(p => p.IsOneTime);

        public bool IsOneTime
        {
            get => GetProperty(IsOneTimeProperty);
            set => SetProperty(IsOneTimeProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> NextDateProperty = RegisterProperty<SmartDate>(p => p.NextDate);

        public SmartDate NextDate
        {
            get => GetProperty(NextDateProperty);
            set => SetProperty(NextDateProperty, value);
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty =
            RegisterProperty<string>(p => p.LastUpdatedBy);

        public string LastUpdatedBy
        {
            get => GetProperty(LastUpdatedByProperty);
            set => SetProperty(LastUpdatedByProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty =
            RegisterProperty<SmartDate>(p => p.LastUpdatedDate);

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

        public static async Task<EventEC> NewEvent()
        {
            return await DataPortal.CreateChildAsync<EventEC>();
        }

        public static async Task<EventEC> GetEvent(Event childData)
        {
            return await DataPortal.FetchChildAsync<EventEC>(childData);
        }

        public static async Task DeleteEvent(int id)
        {
            await DataPortal.DeleteAsync<EventEC>(id);
        }

        #endregion

        #region Data Access

        [FetchChild]
        private void Fetch(Event eventData)
        {
            Id = eventData.Id;
            EventName = eventData.EventName;
            Description = eventData.Description;
            NextDate = eventData.NextDate;
            IsOneTime = eventData.IsOneTime;
            LastUpdatedDate = eventData.LastUpdatedDate;
            LastUpdatedBy = eventData.LastUpdatedBy;
            Notes = eventData.Notes;
        }

        [InsertChild]
        private void Insert()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDal>();
            using (BypassPropertyChecks)
            {
                var eventToInsert = new Event()
                {
                    EventName = EventName,
                    Description = Description,
                    IsOneTime = IsOneTime,
                    NextDate = NextDate,
                    LastUpdatedBy = LastUpdatedBy,
                    LastUpdatedDate = LastUpdatedDate,
                    Notes = Notes
                };

                Id = dal.Insert(eventToInsert);
            }
        }

        [UpdateChild]
        private void Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDal>();

            var eventToUpdate = dal.Fetch(Id);
            eventToUpdate.Id = Id;
            eventToUpdate.EventName = EventName;
            eventToUpdate.Description = Description;
            eventToUpdate.IsOneTime = IsOneTime;
            eventToUpdate.NextDate = NextDate;
            eventToUpdate.LastUpdatedBy = LastUpdatedBy;
            eventToUpdate.LastUpdatedDate = LastUpdatedDate;
            eventToUpdate.Notes = Notes;

            dal.Update(eventToUpdate);
        }

        [DeleteSelfChild]
        private void DeleteSelf()
        {
            Delete(Id);
        }

        [Delete]
        private void Delete(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDal>();

            dal.Delete(id);
        }

        #endregion
    }
}