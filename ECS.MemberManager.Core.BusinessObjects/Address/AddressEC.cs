

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
    public partial class AddressEC : BusinessBase<AddressEC>
    {
        #region Business Methods
 
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public virtual int Id 
        {
            get => GetProperty(IdProperty); //1-2
            private set => LoadProperty(IdProperty, value); //2-3   
        }

        public static readonly PropertyInfo<string> Address1Property = RegisterProperty<string>(o => o.Address1);
        public virtual string Address1 
        {
            get => GetProperty(Address1Property); //1-2
            set => SetProperty(Address1Property, value); //2-4
   
        }

        public static readonly PropertyInfo<string> Address2Property = RegisterProperty<string>(o => o.Address2);
        public virtual string Address2 
        {
            get => GetProperty(Address2Property); //1-2
            set => SetProperty(Address2Property, value); //2-4
   
        }

        public static readonly PropertyInfo<string> CityProperty = RegisterProperty<string>(o => o.City);
        public virtual string City 
        {
            get => GetProperty(CityProperty); //1-2
            set => SetProperty(CityProperty, value); //2-4
   
        }

        public static readonly PropertyInfo<string> StateProperty = RegisterProperty<string>(o => o.State);
        public virtual string State 
        {
            get => GetProperty(StateProperty); //1-2
            set => SetProperty(StateProperty, value); //2-4
   
        }

        public static readonly PropertyInfo<string> PostCodeProperty = RegisterProperty<string>(o => o.PostCode);
        public virtual string PostCode 
        {
            get => GetProperty(PostCodeProperty); //1-2
            set => SetProperty(PostCodeProperty, value); //2-4
   
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(o => o.Notes);
        public virtual string Notes 
        {
            get => GetProperty(NotesProperty); //1-2
            set => SetProperty(NotesProperty, value); //2-4
   
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

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); //1-2
            set => SetProperty(RowVersionProperty, value); //2-4
   
        }

        #endregion 

        #region Factory Methods
        internal static async Task<AddressEC> NewAddressEC()
        {
            return await DataPortal.CreateChildAsync<AddressEC>();
        }

        internal static async Task<AddressEC> GetAddressEC(Address childData)
        {
            return await DataPortal.FetchChildAsync<AddressEC>(childData);
        }  


        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(Address data)
        {
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
        [InsertChild]
        private async Task Insert()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IAddressDal>();
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

       [UpdateChild]
        private async Task Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IAddressDal>();
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
            var dal = dalManager.GetProvider<IAddressDal>();
           
            await dal.Delete(id);
        }

        #endregion
    }
}
