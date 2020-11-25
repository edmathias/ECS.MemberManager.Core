using System;
using System.ComponentModel.DataAnnotations;
using Csla;
using Csla.Rules.CommonRules;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class AddressER : BusinessBase<AddressER>
    {
 
        #region Business Methods 
        
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

        #endregion
        
        #region Factory Methods
        
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
       
        #endregion
        
        #region DataPortal Methods

        [Create]
        [RunLocal]
        private void Create()
        {
            BusinessRules.CheckRules();
        }
 
        [Fetch]
        private void Fetch(int id)
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IAddressDal>();
            var data = dal.Fetch(id);
            using (BypassPropertyChecks)
            {
                Id = data.Id;
                Notes = data.Notes;
            }
        }

        [Insert]
        private void Insert()
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IAddressDal>();
            var addressToInsert = new ECS.MemberManager.Core.EF.Domain.Address();
            using (BypassPropertyChecks)
            {
                addressToInsert.Address1 = this.Address1;
                addressToInsert.Address2 = this.Address2;
                addressToInsert.City = this.City;
                addressToInsert.State = this.State;
                addressToInsert.PostCode = this.PostCode;
                addressToInsert.LastUpdatedDate = this.LastUpdatedDate;
                addressToInsert.LastUpdatedBy = this.LastUpdatedBy;
                addressToInsert.Notes = this.Notes; 
            }
            Id = dal.Insert(addressToInsert);
        }
        
        [Update]
        private void Update()
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IAddressDal>();
            using (BypassPropertyChecks)
            {
                var addressToUpdate = new Address()
                {
                    Address1 = this.Address1,
                    Address2 = this.Address2,
                    City = this.City,
                    State = this.State,
                    PostCode = this.PostCode,
                    LastUpdatedDate = this.LastUpdatedDate,
                    LastUpdatedBy = this.LastUpdatedBy,
                    Notes = this.Notes,
                };
                dal.Update(addressToUpdate);
            }

        }

        [DeleteSelf]
        private void DeleteSelf()
        {
            Delete(this.Id);
        }
        
        [Delete]
        private void Delete(int id)
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IAddressDal>();
 
            dal.Delete(id);
        }
        
        #endregion
    }
}