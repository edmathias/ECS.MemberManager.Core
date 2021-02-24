

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
    public partial class OfficeEC : BusinessBase<OfficeEC>
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
            set => SetProperty(NameProperty, value); //2-4
   
        }

        public static readonly PropertyInfo<int> TermProperty = RegisterProperty<int>(o => o.Term);
        public virtual int Term 
        {
            get => GetProperty(TermProperty); //1-2
            set => SetProperty(TermProperty, value); //2-4
   
        }

        public static readonly PropertyInfo<string> CalendarPeriodProperty = RegisterProperty<string>(o => o.CalendarPeriod);
        public virtual string CalendarPeriod 
        {
            get => GetProperty(CalendarPeriodProperty); //1-2
            set => SetProperty(CalendarPeriodProperty, value); //2-4
   
        }

        public static readonly PropertyInfo<int> ChosenHowProperty = RegisterProperty<int>(o => o.ChosenHow);
        public virtual int ChosenHow 
        {
            get => GetProperty(ChosenHowProperty); //1-2
            set => SetProperty(ChosenHowProperty, value); //2-4
   
        }

        public static readonly PropertyInfo<string> AppointerProperty = RegisterProperty<string>(o => o.Appointer);
        public virtual string Appointer 
        {
            get => GetProperty(AppointerProperty); //1-2
            set => SetProperty(AppointerProperty, value); //2-4
   
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
        internal static async Task<OfficeEC> NewOfficeEC()
        {
            return await DataPortal.CreateChildAsync<OfficeEC>();
        }

        internal static async Task<OfficeEC> GetOfficeEC(Office childData)
        {
            return await DataPortal.FetchChildAsync<OfficeEC>(childData);
        }  


        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(Office data)
        {
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
        [InsertChild]
        private async Task Insert()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOfficeDal>();
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

       [UpdateChild]
        private async Task Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOfficeDal>();
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
            var dal = dalManager.GetProvider<IOfficeDal>();
           
            await dal.Delete(id);
        }

        #endregion
    }
}
