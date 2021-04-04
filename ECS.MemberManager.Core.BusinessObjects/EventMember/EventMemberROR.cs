


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
    public partial class EventMemberROR : BusinessBase<EventMemberROR>
    {
        #region Business Methods 
         public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public virtual int Id 
        {
            get => GetProperty(IdProperty); 
            private set => LoadProperty(IdProperty, value);    
        }


        public static readonly PropertyInfo<MemberInfoROC> MemberInfoProperty = RegisterProperty<MemberInfoROC>(o => o.MemberInfo);
        public MemberInfoROC MemberInfo  
        {
            get => GetProperty(MemberInfoProperty); 
        
            private set => LoadProperty(MemberInfoProperty, value); 
        }    
 

        public static readonly PropertyInfo<EventROC> EventProperty = RegisterProperty<EventROC>(o => o.Event);
        public EventROC Event  
        {
            get => GetProperty(EventProperty); 
        
            private set => LoadProperty(EventProperty, value); 
        }    
 
        public static readonly PropertyInfo<string> RoleProperty = RegisterProperty<string>(o => o.Role);
        public virtual string Role 
        {
            get => GetProperty(RoleProperty); 
            private set => LoadProperty(RoleProperty, value);    
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
        public static async Task<EventMemberROR> GetEventMemberROR(int id)
        {
            return await DataPortal.FetchAsync<EventMemberROR>(id);
        }  


        #endregion

        #region Data Access Methods

        [Fetch]
        private async Task Fetch(int id, [Inject] IDal<EventMember> dal)
        {
            var data = await dal.Fetch(id);

            Id = data.Id;
            MemberInfo = (data.MemberInfo != null ? await MemberInfoROC.GetMemberInfoROC(data.MemberInfo) : null);
            Event = (data.Event != null ? await EventROC.GetEventROC(data.Event) : null);
            Role = data.Role;
            LastUpdatedBy = data.LastUpdatedBy;
            LastUpdatedDate = data.LastUpdatedDate;
            Notes = data.Notes;
            RowVersion = data.RowVersion;
        }

        #endregion
    }
}
