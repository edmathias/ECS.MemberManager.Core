

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
    public partial class OfficeROC : ReadOnlyBase<OfficeROC>
    {
        #region Business Methods
 
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public virtual int Id 
        {
            get => GetProperty(IdProperty); //1-2
            private set => LoadProperty(IdProperty, value); //2-3   
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(o => o.Name);
        public virtual string Name 
        {
            get => GetProperty(NameProperty); //1-2
            private set => LoadProperty(NameProperty, value); //2-3   
        }

        public static readonly PropertyInfo<int> TermProperty = RegisterProperty<int>(o => o.Term);
        public virtual int Term 
        {
            get => GetProperty(TermProperty); //1-2
            private set => LoadProperty(TermProperty, value); //2-3   
        }

        public static readonly PropertyInfo<string> CalendarPeriodProperty = RegisterProperty<string>(o => o.CalendarPeriod);
        public virtual string CalendarPeriod 
        {
            get => GetProperty(CalendarPeriodProperty); //1-2
            private set => LoadProperty(CalendarPeriodProperty, value); //2-3   
        }

        public static readonly PropertyInfo<int> ChosenHowProperty = RegisterProperty<int>(o => o.ChosenHow);
        public virtual int ChosenHow 
        {
            get => GetProperty(ChosenHowProperty); //1-2
            private set => LoadProperty(ChosenHowProperty, value); //2-3   
        }

        public static readonly PropertyInfo<string> AppointerProperty = RegisterProperty<string>(o => o.Appointer);
        public virtual string Appointer 
        {
            get => GetProperty(AppointerProperty); //1-2
            private set => LoadProperty(AppointerProperty, value); //2-3   
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(o => o.LastUpdatedBy);
        public virtual string LastUpdatedBy 
        {
            get => GetProperty(LastUpdatedByProperty); //1-2
            private set => LoadProperty(LastUpdatedByProperty, value); //2-3   
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty = RegisterProperty<SmartDate>(o => o.LastUpdatedDate);
        public virtual SmartDate LastUpdatedDate 
        {
            get => GetProperty(LastUpdatedDateProperty); //1-2
            private set => LoadProperty(LastUpdatedDateProperty, value); //2-3   
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(o => o.Notes);
        public virtual string Notes 
        {
            get => GetProperty(NotesProperty); //1-2
            private set => LoadProperty(NotesProperty, value); //2-3   
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); //1-2
            private set => LoadProperty(RowVersionProperty, value); //2-3   
        }

        #endregion 

        #region Factory Methods
        internal static async Task<OfficeROC> GetOfficeROC(Office childData)
        {
            return await DataPortal.FetchChildAsync<OfficeROC>(childData);
        }  


        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(Office data)
        {
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
