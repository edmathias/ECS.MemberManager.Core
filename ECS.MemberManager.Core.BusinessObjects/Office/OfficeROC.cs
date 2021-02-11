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
    public class OfficeROC : BusinessBase<OfficeROC>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name);
        public string Name
        {
            get => GetProperty(NameProperty);
            private set => LoadProperty(NameProperty, value);
        }

        public static readonly PropertyInfo<int> TermProperty = RegisterProperty<int>(p => p.Term);
        public int Term
        {
            get => GetProperty(TermProperty);
            private set => LoadProperty(TermProperty, value);
        }

        public static readonly PropertyInfo<string> CalendarPeriodProperty =
            RegisterProperty<string>(p => p.CalendarPeriod);
        public string CalendarPeriod
        {
            get => GetProperty(CalendarPeriodProperty);
            private set => LoadProperty(CalendarPeriodProperty, value);
        }

        public static readonly PropertyInfo<int> ChosenHowProperty = RegisterProperty<int>(p => p.ChosenHow);
        public int ChosenHow
        {
            get => GetProperty(ChosenHowProperty);
            private set => LoadProperty(ChosenHowProperty, value);
        }

        public static readonly PropertyInfo<string> AppointerProperty = RegisterProperty<string>(p => p.Appointer);
        public string Appointer
        {
            get => GetProperty(AppointerProperty);
            private set => LoadProperty(AppointerProperty, value);
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty =
            RegisterProperty<string>(p => p.LastUpdatedBy);
        public string LastUpdatedBy
        {
            get => GetProperty(LastUpdatedByProperty);
            private set => LoadProperty(LastUpdatedByProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty =
            RegisterProperty<SmartDate>(p => p.LastUpdatedDate);
        public SmartDate LastUpdatedDate
        {
            get => GetProperty(LastUpdatedDateProperty);
            private set => LoadProperty(LastUpdatedDateProperty, value);
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(p => p.Notes);
        public string Notes
        {
            get => GetProperty(NotesProperty);
            private set => LoadProperty(NotesProperty, value);
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

        public static async Task<OfficeROC> GetOfficeROC(Office childData)
        {
            return await DataPortal.FetchChildAsync<OfficeROC>(childData);
        }

        #endregion

        #region Data Access Methods

        [FetchChild]
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

        #endregion
    }
}