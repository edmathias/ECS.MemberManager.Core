


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
    public partial class OfficeROR : BusinessBase<OfficeROR>
    {
        #region Business Methods 
         public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public virtual int Id 
        {
            get => GetProperty(IdProperty); 
            private set => LoadProperty(IdProperty, value);    
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(o => o.Name);
        public virtual string Name 
        {
            get => GetProperty(NameProperty); 
            private set => LoadProperty(NameProperty, value);    
        }

        public static readonly PropertyInfo<int> TermProperty = RegisterProperty<int>(o => o.Term);
        public virtual int Term 
        {
            get => GetProperty(TermProperty); 
            private set => LoadProperty(TermProperty, value);    
        }

        public static readonly PropertyInfo<string> CalendarPeriodProperty = RegisterProperty<string>(o => o.CalendarPeriod);
        public virtual string CalendarPeriod 
        {
            get => GetProperty(CalendarPeriodProperty); 
            private set => LoadProperty(CalendarPeriodProperty, value);    
        }

        public static readonly PropertyInfo<int> ChosenHowProperty = RegisterProperty<int>(o => o.ChosenHow);
        public virtual int ChosenHow 
        {
            get => GetProperty(ChosenHowProperty); 
            private set => LoadProperty(ChosenHowProperty, value);    
        }

        public static readonly PropertyInfo<string> AppointerProperty = RegisterProperty<string>(o => o.Appointer);
        public virtual string Appointer 
        {
            get => GetProperty(AppointerProperty); 
            private set => LoadProperty(AppointerProperty, value);    
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
        public static async Task<OfficeROR> GetOfficeROR(int id)
        {
            return await DataPortal.FetchAsync<OfficeROR>(id);
        }  


        #endregion

        #region Data Access Methods

        [Fetch]
        private async Task Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOfficeDal>();
            var data = await dal.Fetch(id);

            Id = data.Id;
            Name = data.Name;
            Term = data.Term;
            CalendarPeriod = data.CalendarPeriod;
            ChosenHow = data.ChosenHow;
            Appointer = data.Appointer;
            LastUpdatedBy = data.LastUpdatedBy;
            LastUpdatedDate = data.LastUpdatedDate;
            Notes = data.Notes;
            RowVersion = data.RowVersion;
        }

        #endregion
    }
}
