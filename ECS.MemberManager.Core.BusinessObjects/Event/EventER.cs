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
    public class EventER : BusinessBase<EventER>
    {
        #region Business Methods
        
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get => GetProperty(IdProperty);
            set => SetProperty(IdProperty, value);
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

        public static async Task<EventER> NewEventER()
        {
            return await DataPortal.CreateAsync<EventER>();
        }

        public static async Task<EventER> GetEventER(int id)
        {
            return await DataPortal.FetchAsync<EventER>(id);
        }

        public static async Task DeleteEventER(int id)
        {
            await DataPortal.DeleteAsync<EventER>(id);
        }

        #endregion

        #region Data Access Methods
 
        [Fetch]
        private async Task Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDal>();
            var data = await dal.Fetch(id);

            using (BypassPropertyChecks)
            {
                Id = data.Id;
                EventName = data.EventName;
                Description = data.Description;
                IsOneTime = data.IsOneTime;
                NextDate = data.NextDate;
                LastUpdatedBy = data.LastUpdatedBy;
                LastUpdatedDate = data.LastUpdatedDate;
                Notes = data.Notes;
                RowVersion = data.RowVersion;
            }
        }

        [Insert]
        private async Task Insert()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDal>();
            var data = new Event()
            {
                EventName = EventName,
                Description = Description,
                IsOneTime = IsOneTime,
                NextDate = NextDate,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes
            };

            var insertedEvent = await dal.Insert(data);
            Id = insertedEvent.Id;
            RowVersion = insertedEvent.RowVersion;
        }

        [Update]
        private async Task Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDal>();

            var eventTypeToUpdate = new Event()
            {
                Id = Id,
                EventName = EventName,
                Description = Description,
                IsOneTime = IsOneTime,
                NextDate = NextDate,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
                RowVersion = RowVersion
            };

            var updatedEmail = await dal.Update(eventTypeToUpdate);
            RowVersion = updatedEmail.RowVersion;
        }

        [DeleteSelf]
        private async Task DeleteSelf()
        {
            await Delete(Id);
        }
        
        [Delete]
        private async Task Delete(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDal>();
           
            await dal.Delete(id);
        }

        #endregion
    }
}