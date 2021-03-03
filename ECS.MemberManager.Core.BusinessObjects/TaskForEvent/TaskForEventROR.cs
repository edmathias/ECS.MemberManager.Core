


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/02/2021 21:50:49
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
    public partial class TaskForEventROR : BusinessBase<TaskForEventROR>
    {
        #region Business Methods 
         public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public virtual int Id 
        {
            get => GetProperty(IdProperty); 
            private set => LoadProperty(IdProperty, value);    
        }


        public static readonly PropertyInfo<EventROC> EventProperty = RegisterProperty<EventROC>(o => o.Event);
        public EventROC Event  
        {
            get => GetProperty(EventProperty); 
        
            private set => LoadProperty(EventProperty, value); 
        }    
 
        public static readonly PropertyInfo<string> TaskNameProperty = RegisterProperty<string>(o => o.TaskName);
        public virtual string TaskName 
        {
            get => GetProperty(TaskNameProperty); 
            private set => LoadProperty(TaskNameProperty, value);    
        }

        public static readonly PropertyInfo<SmartDate> PlannedDateProperty = RegisterProperty<SmartDate>(o => o.PlannedDate);
        public virtual SmartDate PlannedDate 
        {
            get => GetProperty(PlannedDateProperty); 
            private set => LoadProperty(PlannedDateProperty, value);    
        }

        public static readonly PropertyInfo<SmartDate> ActualDateProperty = RegisterProperty<SmartDate>(o => o.ActualDate);
        public virtual SmartDate ActualDate 
        {
            get => GetProperty(ActualDateProperty); 
            private set => LoadProperty(ActualDateProperty, value);    
        }

        public static readonly PropertyInfo<string> InformationProperty = RegisterProperty<string>(o => o.Information);
        public virtual string Information 
        {
            get => GetProperty(InformationProperty); 
            private set => LoadProperty(InformationProperty, value);    
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(o => o.LastUpdatedBy);
        public virtual string LastUpdatedBy 
        {
            get => GetProperty(LastUpdatedByProperty); 
            private set => LoadProperty(LastUpdatedByProperty, value);    
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty = RegisterProperty<SmartDate>(o => o.LastUpdatedDate);
        public virtual SmartDate LastUpdatedDate 
        {
            get => GetProperty(LastUpdatedDateProperty); 
            private set => LoadProperty(LastUpdatedDateProperty, value);    
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(o => o.Notes);
        public virtual string Notes 
        {
            get => GetProperty(NotesProperty); 
            private set => LoadProperty(NotesProperty, value);    
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); 
            private set => LoadProperty(RowVersionProperty, value);    
        }

        #endregion 

        #region Factory Methods
        public static async Task<TaskForEventROR> GetTaskForEventROR(int id)
        {
            return await DataPortal.FetchAsync<TaskForEventROR>(id);
        }  


        #endregion

        #region Data Access Methods

        [Fetch]
        private async Task Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITaskForEventDal>();
            var data = await dal.Fetch(id);
                Id = data.Id;
                Event = (data.Event != null ? await EventROC.GetEventROC(data.Event) : null);
                TaskName = data.TaskName;
                PlannedDate = data.PlannedDate;
                ActualDate = data.ActualDate;
                Information = data.Information;
                LastUpdatedBy = data.LastUpdatedBy;
                LastUpdatedDate = data.LastUpdatedDate;
                Notes = data.Notes;
                RowVersion = data.RowVersion;
        }

        #endregion
    }
}
