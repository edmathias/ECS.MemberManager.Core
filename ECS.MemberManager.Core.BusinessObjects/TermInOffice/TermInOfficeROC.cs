using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class TermInOfficeROC : ReadOnlyBase<TermInOfficeROC>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);

        public virtual int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }


        public static readonly PropertyInfo<PersonROC> PersonProperty = RegisterProperty<PersonROC>(o => o.Person);

        public PersonROC Person
        {
            get => GetProperty(PersonProperty);

            private set => LoadProperty(PersonProperty, value);
        }


        public static readonly PropertyInfo<OfficeROC> OfficeProperty = RegisterProperty<OfficeROC>(o => o.Office);

        public OfficeROC Office
        {
            get => GetProperty(OfficeProperty);

            private set => LoadProperty(OfficeProperty, value);
        }

        public static readonly PropertyInfo<SmartDate>
            StartDateProperty = RegisterProperty<SmartDate>(o => o.StartDate);

        public virtual SmartDate StartDate
        {
            get => GetProperty(StartDateProperty);
            private set => LoadProperty(StartDateProperty, value);
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty =
            RegisterProperty<string>(o => o.LastUpdatedBy);

        public virtual string LastUpdatedBy
        {
            get => GetProperty(LastUpdatedByProperty);
            private set => LoadProperty(LastUpdatedByProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty =
            RegisterProperty<SmartDate>(o => o.LastUpdatedDate);

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

        internal static async Task<TermInOfficeROC> GetTermInOfficeROC(TermInOffice childData)
        {
            return await DataPortal.FetchChildAsync<TermInOfficeROC>(childData);
        }

        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(TermInOffice data)
        {
            Id = data.Id;
            Person = (data.Person != null ? await PersonROC.GetPersonROC(data.Person) : null);
            Office = (data.Office != null ? await OfficeROC.GetOfficeROC(data.Office) : null);
            StartDate = data.StartDate;
            LastUpdatedBy = data.LastUpdatedBy;
            LastUpdatedDate = data.LastUpdatedDate;
            Notes = data.Notes;
            RowVersion = data.RowVersion;
        }

        #endregion
    }
}