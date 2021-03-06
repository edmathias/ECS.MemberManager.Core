﻿

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
    public partial class EventMemberEC : BusinessBase<EventMemberEC>
    {
        #region Business Methods
 
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public virtual int Id 
        {
            get => GetProperty(IdProperty); 
            private set => LoadProperty(IdProperty, value);    
        }


        public static readonly PropertyInfo<MemberInfoEC> MemberInfoProperty = RegisterProperty<MemberInfoEC>(o => o.MemberInfo);
        public MemberInfoEC MemberInfo  
        {
            get => GetProperty(MemberInfoProperty); 
            set => SetProperty(MemberInfoProperty, value); 
        }    
 

        public static readonly PropertyInfo<EventEC> EventProperty = RegisterProperty<EventEC>(o => o.Event);
        public EventEC Event  
        {
            get => GetProperty(EventProperty); 
            set => SetProperty(EventProperty, value); 
        }    
 
        public static readonly PropertyInfo<string> RoleProperty = RegisterProperty<string>(o => o.Role);
        public virtual string Role 
        {
            get => GetProperty(RoleProperty); 
            set => SetProperty(RoleProperty, value); 
   
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
        internal static async Task<EventMemberEC> NewEventMemberEC()
        {
            return await DataPortal.CreateChildAsync<EventMemberEC>();
        }

        internal static async Task<EventMemberEC> GetEventMemberEC(EventMember childData)
        {
            return await DataPortal.FetchChildAsync<EventMemberEC>(childData);
        }  


        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(EventMember data)
        {
            using(BypassPropertyChecks)
            {
            Id = data.Id;
            MemberInfo = (data.MemberInfo != null ? await MemberInfoEC.GetMemberInfoEC(data.MemberInfo) : null);
            Event = (data.Event != null ? await EventEC.GetEventEC(data.Event) : null);
            Role = data.Role;
            LastUpdatedBy = data.LastUpdatedBy;
            LastUpdatedDate = data.LastUpdatedDate;
            Notes = data.Notes;
            RowVersion = data.RowVersion;
            }            
        }
        [InsertChild]
        private async Task Insert([Inject] IDal<EventMember> dal)
        {
            FieldManager.UpdateChildren();

            var data = new EventMember()
            {

                Id = Id,
                MemberInfo = (MemberInfo != null ? new MemberInfo() { Id = MemberInfo.Id } : null),
                Event = (Event != null ? new Event() { Id = Event.Id } : null),
                Role = Role,
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
        private async Task Update([Inject] IDal<EventMember> dal)
        {
            FieldManager.UpdateChildren();

            var data = new EventMember()
            {

                Id = Id,
                MemberInfo = (MemberInfo != null ? new MemberInfo() { Id = MemberInfo.Id } : null),
                Event = (Event != null ? new Event() { Id = Event.Id } : null),
                Role = Role,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Update(data);
            RowVersion = insertedObj.RowVersion;
        }

       
        [DeleteSelfChild]
        private async Task DeleteSelf([Inject] IDal<EventMember> dal)
        {
            await Delete(Id,dal);
        }
       
        [Delete]
        private async Task Delete(int id, [Inject] IDal<EventMember> dal)
        {
            await dal.Delete(id);
        }

        #endregion
    }
}
