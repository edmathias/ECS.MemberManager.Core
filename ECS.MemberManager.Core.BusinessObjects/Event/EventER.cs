

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
    public partial class EventER : BusinessBase<EventER>
    {
        #region Business Methods
 
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public virtual int Id 
        {
            get => GetProperty(IdProperty); //1-2
            private set => LoadProperty(IdProperty, value); //2-3   
        }

        public static readonly PropertyInfo<string> EventNameProperty = RegisterProperty<string>(o => o.EventName);
        public virtual string EventName 
        {
            get => GetProperty(EventNameProperty); //1-2
            set => SetProperty(EventNameProperty, value); //2-4
   
        }

        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(o => o.Description);
        public virtual string Description 
        {
            get => GetProperty(DescriptionProperty); //1-2
            set => SetProperty(DescriptionProperty, value); //2-4
   
        }

        public static readonly PropertyInfo<bool> IsOneTimeProperty = RegisterProperty<bool>(o => o.IsOneTime);
        public virtual bool IsOneTime 
        {
            get => GetProperty(IsOneTimeProperty); //1-2
            set => SetProperty(IsOneTimeProperty, value); //2-4
   
        }

        public static readonly PropertyInfo<SmartDate> NextDateProperty = RegisterProperty<SmartDate>(o => o.NextDate);
        public virtual SmartDate NextDate 
        {
            get => GetProperty(NextDateProperty); //1-2
            set => SetProperty(NextDateProperty, value); //2-4
   
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(o => o.LastUpdatedBy);
        public virtual string LastUpdatedBy 
        {
            get => GetProperty(LastUpdatedByProperty); //1-2
            set => SetProperty(LastUpdatedByProperty, value); //2-4
   
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty = RegisterProperty<SmartDate>(o => o.LastUpdatedDate);
        public virtual SmartDate LastUpdatedDate 
        {
            get => GetProperty(LastUpdatedDateProperty); //1-2
            set => SetProperty(LastUpdatedDateProperty, value); //2-4
   
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(o => o.Notes);
        public virtual string Notes 
        {
            get => GetProperty(NotesProperty); //1-2
            set => SetProperty(NotesProperty, value); //2-4
   
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); //1-2
            set => SetProperty(RowVersionProperty, value); //2-4
   
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
            using(BypassPropertyChecks)
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

                Id = Id,
                EventName = EventName,
                Description = Description,
                IsOneTime = IsOneTime,
                NextDate = NextDate,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Insert(data);
            Id = insertedObj.Id;
            RowVersion = insertedObj.RowVersion;
        }

       [Update]
        private async Task Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDal>();
            var data = new Event()
            {

                Id = Id,
                EventName = EventName,
                Description = Description,
                IsOneTime = IsOneTime,
                NextDate = NextDate,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Update(data);
            Id = insertedObj.Id;
            RowVersion = insertedObj.RowVersion;
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
            var dal = dalManager.GetProvider<IEventDal>();
           
            await dal.Delete(id);
        }

        #endregion
    }
}
