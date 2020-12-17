using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class OfficeER : BusinessBase<OfficeER>
    {
        #region Business Methods
        
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }
       
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name);
        [Required,MaxLength(50)]
        public string Name
        {
            get => GetProperty(NameProperty);
            set => SetProperty(NameProperty, value);
        }
        
        public static readonly PropertyInfo<int> TermProperty = RegisterProperty<int>(p => p.Term);
        public int Term
        {
            get => GetProperty(TermProperty);
            set => SetProperty(TermProperty, value);
        }

        [MaxLength(25)]
        public static readonly PropertyInfo<string> CalendarPeriodProperty = RegisterProperty<string>(p => p.CalendarPeriod);
        public string CalendarPeriod
        {
            get => GetProperty(CalendarPeriodProperty);
            set => SetProperty(CalendarPeriodProperty, value);
        }

        public static readonly PropertyInfo<int> ChosenHowProperty = RegisterProperty<int>(p => p.ChosenHow);
        public int ChosenHow
        {
            get => GetProperty(ChosenHowProperty);
            set => SetProperty(ChosenHowProperty, value);
        }

        public static readonly PropertyInfo<string> AppointerProperty = RegisterProperty<string>(p => p.Appointer);
        [MaxLength(50)]
        public string Appointer
        {
            get => GetProperty(AppointerProperty);
            set => SetProperty(AppointerProperty, value);
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(p => p.LastUpdatedBy);
        [Required,MaxLength(255)]
        public string LastUpdatedBy
        {
            get => GetProperty(LastUpdatedByProperty);
            set => SetProperty(LastUpdatedByProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty = RegisterProperty<SmartDate>(p => p.LastUpdatedDate);
        [Required]
        public SmartDate LastUpdatedDate
        {
            get => GetProperty(LastUpdatedDateProperty);
            set => SetProperty(LastUpdatedDateProperty, value);
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(p => p.Notes);
        public string Notes
        {
            get => GetProperty(NotesProperty);
            set => SetProperty(NotesProperty, value);
        }

        // TODO: add public properties and methods


        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();

            // TODO: add business rules
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }
        
        #endregion

        #region Factory Methods

        public static async Task<OfficeER> NewOffice()
        {
            return await DataPortal.CreateAsync<OfficeER>();
        }

        public static async Task<OfficeER> GetOffice(int id)
        {
            return await DataPortal.FetchAsync<OfficeER>(id);
        }

        public static async Task DeleteOffice(int id)
        {
            await DataPortal.DeleteAsync<OfficeER>(id);
        }

        #endregion

        #region Data Access

        [Fetch]
        private void Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOfficeDal>();
            var data = dal.Fetch(id);
            using (BypassPropertyChecks)
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
            }
        }

        [Insert]
        private void Insert()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider <IOfficeDal> ();
            using (BypassPropertyChecks)
            {
                var officeToInsert = new EF.Domain.Office()
                {
                    Name = this.Name,
                    Term = this.Term,
                    CalendarPeriod = this.CalendarPeriod,
                    ChosenHow = this.ChosenHow,
                    Appointer = this.Appointer,
                    LastUpdatedBy = this.LastUpdatedBy,
                    LastUpdatedDate = this.LastUpdatedDate,
                    Notes = this.Notes
                };
                
                Id = dal.Insert(officeToInsert);
            }
        }

        [Update]
        private void Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider <IOfficeDal>();
            var officeToUpdate = dal.Fetch(this.Id);
            
            using (BypassPropertyChecks)
            {
                officeToUpdate.Name = this.Name;
                officeToUpdate.Term = this.Term;
                officeToUpdate.CalendarPeriod = this.CalendarPeriod;
                officeToUpdate.ChosenHow = this.ChosenHow;
                officeToUpdate.Appointer = this.Appointer;
                officeToUpdate.LastUpdatedBy = this.LastUpdatedBy;
                officeToUpdate.LastUpdatedDate = this.LastUpdatedDate;
                officeToUpdate.Notes = this.Notes;
            }
            dal.Update(officeToUpdate);
        }

        [DeleteSelf]
        private void DeleteSelf()
        {
            Delete(this.Id);
        }

        [Delete]
        private void Delete(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider <IOfficeDal>();

            dal.Delete(id);
        }


        
        #endregion

    }
}