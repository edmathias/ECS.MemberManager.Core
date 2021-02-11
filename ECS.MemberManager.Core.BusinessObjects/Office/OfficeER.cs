using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

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

        [Required, MaxLength(50)]
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

        public static readonly PropertyInfo<string> CalendarPeriodProperty =
            RegisterProperty<string>(p => p.CalendarPeriod);

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

        [MaxLength(50)]
        public static readonly PropertyInfo<string> AppointerProperty = RegisterProperty<string>(p => p.Appointer);

        public string Appointer
        {
            get => GetProperty(AppointerProperty);
            set => SetProperty(AppointerProperty, value);
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty =
            RegisterProperty<string>(p => p.LastUpdatedBy);

        [Required, MaxLength(255)]
        public string LastUpdatedBy
        {
            get => GetProperty(LastUpdatedByProperty);
            set => SetProperty(LastUpdatedByProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty =
            RegisterProperty<SmartDate>(p => p.LastUpdatedDate);

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

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(p => p.RowVersion);

        public byte[] RowVersion
        {
            get => GetProperty(RowVersionProperty);
            private set => LoadProperty(RowVersionProperty, value);
        }

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

        public static async Task<OfficeER> NewOfficeER()
        {
            return await DataPortal.CreateAsync<OfficeER>();
        }

        public static async Task<OfficeER> GetOfficeER(Office childData)
        {
            return await DataPortal.FetchChildAsync<OfficeER>(childData);
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
        private async Task Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOfficeDal>();
            var data = await dal.Fetch(id);

            Fetch(data);
        }

        private void Fetch(Office childData)
        {
            using (BypassPropertyChecks)
            {
                Id = childData.Id;
                Name = childData.Name;
                Term = childData.Term;
                CalendarPeriod = childData.CalendarPeriod;
                ChosenHow = childData.ChosenHow;
                Appointer = childData.Appointer;
                LastUpdatedBy = childData.LastUpdatedBy;
                LastUpdatedDate = childData.LastUpdatedDate;
                Notes = childData.Notes;
                RowVersion = childData.RowVersion;
            }
        }

        [Insert]
        private async Task Insert()
        {
            await InsertChild();
        }

        private async Task InsertChild()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOfficeDal>();
            var data = new Office()
            {
                Name = Name,
                Term = Term,
                CalendarPeriod = CalendarPeriod,
                ChosenHow = ChosenHow,
                Appointer = Appointer,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
            };

            var insertedOffice = await dal.Insert(data);
            Id = insertedOffice.Id;
            RowVersion = insertedOffice.RowVersion;
        }

        [Update]
        private async Task Update()
        {
            await ChildUpdate();
        }

        private async Task ChildUpdate()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOfficeDal>();

            var officeToUpdate = new Office()
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
                RowVersion = RowVersion
            };

            var updatedEmail = await dal.Update(officeToUpdate);
            RowVersion = updatedEmail.RowVersion;
        }

        [DeleteSelf]
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