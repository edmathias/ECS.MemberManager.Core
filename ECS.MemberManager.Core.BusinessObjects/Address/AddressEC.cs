﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Csla;
using Csla.Data;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class AddressEC : BusinessBase<AddressEC>
    {
 
        #region Business Methods 
        
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<string> Address1Property = RegisterProperty<string>(p => p.Address1);
        [Required,MaxLength(35)]
        public string Address1
        {
            get => GetProperty(Address1Property);
            set => SetProperty(Address1Property, value);
        }

        public static readonly PropertyInfo<string> Address2Property = RegisterProperty<string>(p => p.Address2);
        [MaxLength(35)]
        public string Address2
        {
            get => GetProperty(Address2Property);
            set => SetProperty(Address2Property, value);
        }

        public static readonly PropertyInfo<string> CityProperty = RegisterProperty<string>(p => p.City);
        [Required, MaxLength(50)]
        public string City
        {
            get => GetProperty(CityProperty);
            set => SetProperty(CityProperty, value);
        }

        public static readonly PropertyInfo<string> StateProperty = RegisterProperty<string>(p => p.State);
        [Required, MinLength(2),MaxLength(2)]
        public string State
        {
            get => GetProperty(StateProperty);
            set => SetProperty(StateProperty, value);
        }

        public static readonly PropertyInfo<string> PostCodeProperty = RegisterProperty<string>(p => p.PostCode);
        [Required,MaxLength(9)]
        public string PostCode
        {
            get => GetProperty(PostCodeProperty);
            set => SetProperty(PostCodeProperty, value);
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(p => p.Notes);
        public string Notes
        {
            get => GetProperty(NotesProperty);
            set => SetProperty(NotesProperty, value);
        }
        
        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(p => p.LastUpdatedBy);
        [Required,MaxLength(255)]
        public string LastUpdatedBy
        {
            get => GetProperty(LastUpdatedByProperty);
            set => SetProperty(LastUpdatedByProperty, value);
        }

        public static readonly PropertyInfo<DateTime> LastUpdatedDateProperty = RegisterProperty<DateTime>(p => p.LastUpdatedDate);
        [Required]
        public DateTime LastUpdatedDate
        {
            get => GetProperty(LastUpdatedDateProperty);
            set => SetProperty(LastUpdatedDateProperty, value);
        }

        #endregion
        
        #region Factory Methods
        
        public static async Task<AddressEC> NewAddress()
        {
            return await DataPortal.CreateChildAsync<AddressEC>();
        }

        public static async Task<AddressEC> GetAddress(Address childData)
        {
            return await DataPortal.FetchChildAsync<AddressEC>(childData);
        }

        public static async Task DeleteAddress(int id)
        {
            await DataPortal.DeleteAsync<AddressEC>(id);
        }
       
        #endregion
        
        #region DataPortal Methods

        [Create]
        [RunLocal]
        private void Create()
        {
            BusinessRules.CheckRules();
        }
 
        [FetchChild]
        private void Fetch(Address childData)
        {
            MarkAsChild();
            
            using (BypassPropertyChecks)
            {
                Id = childData.Id;
                Address1 = childData.Address1;
                Address2 = childData.Address2;
                City = childData.City;
                State = childData.State;
                PostCode = childData.PostCode;
                LastUpdatedBy = childData.LastUpdatedBy;
                LastUpdatedDate = childData.LastUpdatedDate;
                Notes = childData.Notes;
            }
            
        }

        [InsertChild]
        private void Insert()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IAddressDal>();
            using (BypassPropertyChecks)
            {
                var addressToInsert = new Address()
                {
                    Address1 = this.Address1,
                    Address2 = this.Address2,
                    City = this.City,
                    State = this.State,
                    PostCode = this.PostCode,
                    LastUpdatedDate = this.LastUpdatedDate,
                    LastUpdatedBy = this.LastUpdatedBy,
                    Notes = this.Notes
                };
                
                Id = dal.Insert(addressToInsert);
            }
        }
        
        [UpdateChild]
        private void Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IAddressDal>();
            using (BypassPropertyChecks)
            {
                var addressToUpdate = dal.Fetch(Id);
                addressToUpdate.Address1 = this.Address1;
                addressToUpdate.Address2 = this.Address2;
                addressToUpdate.City = this.City;
                addressToUpdate.State = this.State;
                addressToUpdate.PostCode = this.PostCode;
                addressToUpdate.LastUpdatedDate = this.LastUpdatedDate;
                addressToUpdate.LastUpdatedBy = this.LastUpdatedBy;
                addressToUpdate.Notes = this.Notes;
                    
                dal.Update(addressToUpdate);
            }

        }

        [DeleteSelfChild]
        private void DeleteSelf()
        {
            Delete(this.Id);
        }
        
        [Delete]
        private void Delete(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IAddressDal>();
 
            dal.Delete(id);
        }
        
        #endregion
    }
}