

//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/18/2021 16:28:40
//******************************************************************************    

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
        internal static async Task<TaskForEventEC> NewTaskForEventEC()
        {
            return await DataPortal.CreateChildAsync<TaskForEventEC>();
        }

        internal static async Task<TaskForEventEC> GetTaskForEventEC(TaskForEvent childData)
        {
            return await DataPortal.FetchChildAsync<TaskForEventEC>(childData);
        }  


        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(TaskForEvent data)
        {
            using(BypassPropertyChecks)
            {
            Id = data.Id;
            Event = (data.Event != null ? await EventEC.GetEventEC(data.Event) : null);
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
        [InsertChild]
        private async Task Insert([Inject] ITaskForEventDal dal)
        {
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
        private async Task Update([Inject] ITaskForEventDal dal)
        {
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

            var insertedObj = await dal.Update(data);
            RowVersion = insertedObj.RowVersion;
        }

       
        [DeleteSelfChild]
        private async Task DeleteSelf([Inject] ITaskForEventDal dal)
        {
            await Delete(Id,dal);
        }
       
        [Delete]
        private async Task Delete(int id, [Inject] ITaskForEventDal dal)
        {
            await dal.Delete(id);
        }

        #endregion
    }
}
