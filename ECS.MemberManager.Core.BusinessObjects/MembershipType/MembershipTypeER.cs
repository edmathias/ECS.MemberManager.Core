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
    public partial class OfficeER : BusinessBase<OfficeER>
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
            set => SetProperty(NameProperty, value); 
   
        }

        public static readonly PropertyInfo<int> TermProperty = RegisterProperty<int>(o => o.Term);
        public virtual int Term 
        {
            get => GetProperty(TermProperty); 
            set => SetProperty(TermProperty, value); 
   
        }

        public static readonly PropertyInfo<string> CalendarPeriodProperty = RegisterProperty<string>(o => o.CalendarPeriod);
        public virtual string CalendarPeriod 
        {
            get => GetProperty(CalendarPeriodProperty); 
            set => SetProperty(CalendarPeriodProperty, value); 
   
        }

        public static readonly PropertyInfo<int> ChosenHowProperty = RegisterProperty<int>(o => o.ChosenHow);
        public virtual int ChosenHow 
        {
            get => GetProperty(ChosenHowProperty); 
            set => SetProperty(ChosenHowProperty, value); 
   
        }

        public static readonly PropertyInfo<string> AppointerProperty = RegisterProperty<string>(o => o.Appointer);
        public virtual string Appointer 
        {
            get => GetProperty(AppointerProperty); 
            set => SetProperty(AppointerProperty, value); 
   
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
        public static async Task<OfficeER> NewOfficeER()
        {
            return await DataPortal.CreateAsync<OfficeER>();
        }

        public static async Task<OfficeER> GetOfficeER(int id)
        {
            return await DataPortal.FetchAsync<OfficeER>(id);
        }  

        public static async Task DeleteOfficeER(int id)
        {
            await DataPortal.DeleteAsync<OfficeER>(id);
        } 


        #endregion

        #region Data Access Methods

        [Fetch]
        private async Task Fetch(int id, [Inject] IDal<Office> dal)
        {
            var data = await dal.Fetch(id);

            using(BypassPropertyChecks)
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
        }
        [Insert]
        private async Task Insert([Inject] IDal<Office> dal)
        {
            FieldManager.UpdateChildren();

            var data = new Office()
            {

                Id = Id,
                Name = Name,
                Term = Term,
                CalendarPeriod = CalendarPeriod,
                ChosenHow = ChosenHow,
                Appointer = Appointer,
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
        private async Task Update([Inject] IDal<Office> dal)
        {
            FieldManager.UpdateChildren();

            var data = new Office()
            {

                Id = Id,
                Name = Name,
                Term = Term,
                CalendarPeriod = CalendarPeriod,
                ChosenHow = ChosenHow,
                Appointer = Appointer,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Update(data);
            RowVersion = insertedObj.RowVersion;
        }

        [DeleteSelf]
        private async Task DeleteSelf([Inject] IDal<Office> dal)
        {
            await Delete(Id,dal);
        }
       
        [Delete]
        private async Task Delete(int id, [Inject] IDal<Office> dal)
        {
            await dal.Delete(id);
        }

        #endregion
    }
}
