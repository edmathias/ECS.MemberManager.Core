using System;
using System.ComponentModel.DataAnnotations;
using Csla;
using Csla.Rules.CommonRules;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class AddressER : BusinessBase<AddressER>
    {
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            private set { LoadProperty(IdProperty, value); }
        }

        public static readonly PropertyInfo<string> Address1Property = RegisterProperty<string>(p => p.Address1);
        [Required,MaxLength(35)]
        public string Address1
        {
            get { return GetProperty(Address1Property); }
            set { SetProperty(Address1Property, value); }
        }

        public static readonly PropertyInfo<string> Address2Property = RegisterProperty<string>(p => p.Address2);
        [MaxLength(35)]
        public string Address2
        {
            get { return GetProperty(Address2Property); }
            set { SetProperty(Address2Property, value); }
        }

        public static readonly PropertyInfo<string> CityProperty = RegisterProperty<string>(p => p.City);
        [Required, MaxLength(50)]
        public string City
        {
            get { return GetProperty(CityProperty); }
            set { SetProperty(CityProperty, value); }
        }

        public static readonly PropertyInfo<string> StateProperty = RegisterProperty<string>(p => p.State);
        [Required, MinLength(2),MaxLength(2)]
        public string State
        {
            get { return GetProperty(StateProperty); }
            set { SetProperty(StateProperty, value); }
        }

        public static readonly PropertyInfo<string> PostCodeProperty = RegisterProperty<string>(p => p.PostCode);
        [Required,MaxLength(9)]
        public string PostCode
        {
            get { return GetProperty(PostCodeProperty); }
            set { SetProperty(PostCodeProperty, value); }
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(p => p.Notes);
        public string Notes
        {
            get { return GetProperty(NotesProperty); }
            set { SetProperty(NotesProperty, value); }
        }
        
        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(p => p.LastUpdatedBy);
        [Required,MaxLength(255)]
        public string LastUpdatedBy
        {
            get { return GetProperty(LastUpdatedByProperty); }
            set { SetProperty(LastUpdatedByProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> LastUpdatedDateProperty = RegisterProperty<DateTime>(p => p.LastUpdatedDate);
        [Required]
        public DateTime LastUpdatedDate
        {
            get { return GetProperty(LastUpdatedDateProperty); }
            set { SetProperty(LastUpdatedDateProperty, value); }
        }

        public static AddressER NewAddressER()
        {
            return DataPortal.Create<AddressER>();
        }

        public static AddressER GetAddressER(int id)
        {
            return DataPortal.Fetch<AddressER>(id);
        }

        public static void DeleteAddressER(int id)
        {
            DataPortal.Delete<AddressER>(id);
        }
    }
}