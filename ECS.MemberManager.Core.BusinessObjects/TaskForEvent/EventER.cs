using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class TaskForEventER : BusinessBase<TaskForEventER>
    {
        #region Business Methods
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get => GetProperty(IdProperty); 
            private set => LoadProperty(IdProperty, value); 
        }        
    
        public static readonly PropertyInfo<int> EventIdProperty = RegisterProperty<int>(p => p.EventId);
        public int EventId
        {
            get => GetProperty(EventIdProperty); 
            set => SetProperty(EventIdProperty, value); 
        }        
    
        public static readonly PropertyInfo<string> TaskNameProperty = RegisterProperty<string>(p => p.TaskName);
        public string TaskName
        {
            get => GetProperty(TaskNameProperty); 
            set => SetProperty(TaskNameProperty, value); 
        }        
    
        public static readonly PropertyInfo<DateTime> PlannedDateProperty = RegisterProperty<DateTime>(p => p.PlannedDate);
        public DateTime PlannedDate
        {
            get => GetProperty(PlannedDateProperty); 
            set => SetProperty(PlannedDateProperty, value); 
        }        
    
        public static readonly PropertyInfo<DateTime> ActualDateProperty = RegisterProperty<DateTime>(p => p.ActualDate);
        public DateTime ActualDate
        {
            get => GetProperty(ActualDateProperty); 
            set => SetProperty(ActualDateProperty, value); 
        }        
    
        public static readonly PropertyInfo<string> InformationProperty = RegisterProperty<string>(p => p.Information);
        public string Information
        {
            get => GetProperty(InformationProperty); 
            set => SetProperty(InformationProperty, value); 
        }        
    
        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(p => p.LastUpdatedBy);
        public string LastUpdatedBy
        {
            get => GetProperty(LastUpdatedByProperty); 
            set => SetProperty(LastUpdatedByProperty, value); 
        }        
    
        public static readonly PropertyInfo<DateTime> LastUpdatedDateProperty = RegisterProperty<DateTime>(p => p.LastUpdatedDate);
        public DateTime LastUpdatedDate
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
            set => SetProperty(RowVersionProperty, value); 
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

        public static async Task<TaskForEventER> NewTaskForEventER()
        {
            return await DataPortal.CreateAsync<TaskForEventER>();
        }

        public static async Task<TaskForEventER> GetTaskForEventER(int id)
        {
            return await DataPortal.FetchAsync<TaskForEventER>(id);
        }

        public static async Task DeleteTaskForEventER(int id)
        {
            await DataPortal.DeleteAsync<TaskForEventER>(id);
        }

        #endregion

        #region Data Access Methods
 
        [Fetch]
        private async Task Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITaskForEventDal>();
            var data = await dal.Fetch(id);

            using (BypassPropertyChecks)
            {
                Id = data.Id;
                TaskName = data.TaskName;
                PlannedDate = data.PlannedDate;
                ActualDate = data.ActualDate;
                Information = data.Information;
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
            var dal = dalManager.GetProvider<ITaskForEventDal>();
            var data = new TaskForEvent()
            {
                TaskName = this.TaskName,
                PlannedDate = this.PlannedDate,
                ActualDate = this.ActualDate,
                Information = this.Information,
                LastUpdatedBy = this.LastUpdatedBy,
                LastUpdatedDate = this.LastUpdatedDate,
                Notes = this.Notes,
                RowVersion = this.RowVersion,

            };

            var insertedTaskForEvent = await dal.Insert(data);
            Id = insertedTaskForEvent.Id;
            RowVersion = insertedTaskForEvent.RowVersion;
        }

        [Update]
        private async Task Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITaskForEventDal>();

            var eventTypeToUpdate = new TaskForEvent()
            {
                Id = Id,
                TaskName = this.TaskName,
                PlannedDate = this.PlannedDate,
                ActualDate = this.ActualDate,
                Information = this.Information,
                LastUpdatedBy = this.LastUpdatedBy,
                LastUpdatedDate = this.LastUpdatedDate,
                Notes = this.Notes,
                RowVersion = this.RowVersion,            
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
            var dal = dalManager.GetProvider<ITaskForEventDal>();
           
            await dal.Delete(id);
        }

        #endregion
    }
}