


using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class TaskForEventEC : BusinessBase<TaskForEventEC>
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

        public static readonly PropertyInfo<string> TaskNameProperty = RegisterProperty<string>(o => o.TaskName);
        public virtual string TaskName 
        {
            get => GetProperty(TaskNameProperty); 
            set => SetProperty(TaskNameProperty, value); 
   
        }        
        public static readonly PropertyInfo<SmartDate> PlannedDateProperty = RegisterProperty<SmartDate>(o => o.PlannedDate);
        public virtual SmartDate PlannedDate 
        {
            get => GetProperty(PlannedDateProperty); 
            set => SetProperty(PlannedDateProperty, value); 
   
        }        
        public static readonly PropertyInfo<SmartDate> ActualDateProperty = RegisterProperty<SmartDate>(o => o.ActualDate);
        public virtual SmartDate ActualDate 
        {
            get => GetProperty(ActualDateProperty); 
            set => SetProperty(ActualDateProperty, value); 
   
        }        
        public static readonly PropertyInfo<string> InformationProperty = RegisterProperty<string>(o => o.Information);
        public virtual string Information 
        {
            get => GetProperty(InformationProperty); 
            set => SetProperty(InformationProperty, value); 
   
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



        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(TaskForEvent childData)
        {
            using (BypassPropertyChecks)
            {
                Id = childData.Id;
                if(childData.Event != null )
                {
                    Event = await EventEC.GetEventEC(childData.Event);
                }
                TaskName = childData.TaskName;
                PlannedDate = childData.PlannedDate;
                ActualDate = childData.ActualDate;
                Information = childData.Information;
                LastUpdatedBy = childData.LastUpdatedBy;
                LastUpdatedDate = childData.LastUpdatedDate;
                Notes = childData.Notes;
                RowVersion = childData.RowVersion;
            }
        }

        [InsertChild]
        private async Task Insert()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITaskForEventDal>();
            var data = new TaskForEvent()
            {
                Id = Id,
                Event = (Event != null ? new Event() { Id = Event.Id } : null),
                TaskName = TaskName,
                PlannedDate = PlannedDate,
                ActualDate = ActualDate,
                Information = Information,
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
            var dal = dalManager.GetProvider<ITaskForEventDal>();

            var objToUpdate = new TaskForEvent()
            {
                Id = Id,
                Event = (Event != null ? new Event() { Id = Event.Id } : null),
                TaskName = TaskName,
                PlannedDate = PlannedDate,
                ActualDate = ActualDate,
                Information = Information,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
                RowVersion = RowVersion,
        
            };

            var updatedObj = await dal.Update(objToUpdate);
            RowVersion = updatedObj.RowVersion;
        }

        [DeleteSelfChild]
        private async Task DeleteSelf()
        {
            await Delete(Id);
        }
        
        private async Task Delete(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITaskForEventDal>();
           
            await dal.Delete(id);
        }


        #endregion
    }
}
