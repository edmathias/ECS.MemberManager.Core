

//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 04/07/2021 09:31:23
//******************************************************************************    

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
    public partial class AddressER : BusinessBase<AddressER>
    {
        #region Business Methods
 
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public virtual int Id 
        {
            get => GetProperty(IdProperty); 
            private set => LoadProperty(IdProperty, value);    
        }

        public static readonly PropertyInfo<string> Address1Property = RegisterProperty<string>(o => o.Address1);
        public virtual string Address1 
        {
            get => GetProperty(Address1Property); 
            set => SetProperty(Address1Property, value); 
   
        }

        public static readonly PropertyInfo<string> Address2Property = RegisterProperty<string>(o => o.Address2);
        public virtual string Address2 
        {
            get => GetProperty(Address2Property); 
            set => SetProperty(Address2Property, value); 
   
        }

        public static readonly PropertyInfo<string> CityProperty = RegisterProperty<string>(o => o.City);
        public virtual string City 
        {
            get => GetProperty(CityProperty); 
            set => SetProperty(CityProperty, value); 
   
        }

        public static readonly PropertyInfo<string> StateProperty = RegisterProperty<string>(o => o.State);
        public virtual string State 
        {
            get => GetProperty(StateProperty); 
            set => SetProperty(StateProperty, value); 
   
        }

        public static readonly PropertyInfo<string> PostCodeProperty = RegisterProperty<string>(o => o.PostCode);
        public virtual string PostCode 
        {
            get => GetProperty(PostCodeProperty); 
            set => SetProperty(PostCodeProperty, value); 
   
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(o => o.Notes);
        public virtual string Notes 
        {
            get => GetProperty(NotesProperty); 
            set => SetProperty(NotesProperty, value); 
   
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

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); 
            set => SetProperty(RowVersionProperty, value); 
   
        }

        #endregion 

        #region Factory Methods
        public static async Task<AddressER> NewAddressER()
        {
            return await DataPortal.CreateAsync<AddressER>();
        }

        public static async Task<AddressER> GetAddressER(int id)
        {
            return await DataPortal.FetchAsync<AddressER>(id);
        }  

        public static async Task DeleteAddressER(int id)
        {
            await DataPortal.DeleteAsync<AddressER>(id);
        } 


        #endregion

        #region Data Access Methods

        [Fetch]
        private async Task Fetch(int id, [Inject] IDal<Address> dal)
        {
            var data = await dal.Fetch(id);

            using(BypassPropertyChecks)
            {
            Id = data.Id;
            Address1 = data.Address1;
            Address2 = data.Address2;
            City = data.City;
            State = data.State;
            PostCode = data.PostCode;
            Notes = data.Notes;
            LastUpdatedBy = data.LastUpdatedBy;
            LastUpdatedDate = data.LastUpdatedDate;
            RowVersion = data.RowVersion;
            }            
        }
        [Insert]
        private async Task Insert([Inject] IDal<Address> dal)
        {
            FieldManager.UpdateChildren();

            var data = new Address()
            {

                Id = Id,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                State = State,
                PostCode = PostCode,
                Notes = Notes,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Insert(data);
            Id = insertedObj.Id;
            RowVersion = insertedObj.RowVersion;
        }

       [Update]
        private async Task Update([Inject] IDal<Address> dal)
        {
            FieldManager.UpdateChildren();

            var data = new Address()
            {

                Id = Id,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                State = State,
                PostCode = PostCode,
                Notes = Notes,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Update(data);
            RowVersion = insertedObj.RowVersion;
        }

        [DeleteSelf]
        private async Task DeleteSelf([Inject] IDal<Address> dal)
        {
            await Delete(Id,dal);
        }
       
        [Delete]
        private async Task Delete(int id, [Inject] IDal<Address> dal)
        {
            await dal.Delete(id);
        }

        #endregion
    }
}
