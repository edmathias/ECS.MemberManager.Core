

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
    public partial class AddressROC : ReadOnlyBase<AddressROC>
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
            private set => LoadProperty(Address1Property, value); //2-3   
        }

        public static readonly PropertyInfo<string> Address2Property = RegisterProperty<string>(o => o.Address2);
        public virtual string Address2 
        {
            get => GetProperty(Address2Property); //1-2
            private set => LoadProperty(Address2Property, value); //2-3   
        }

        public static readonly PropertyInfo<string> CityProperty = RegisterProperty<string>(o => o.City);
        public virtual string City 
        {
            get => GetProperty(CityProperty); //1-2
            private set => LoadProperty(CityProperty, value); //2-3   
        }

        public static readonly PropertyInfo<string> StateProperty = RegisterProperty<string>(o => o.State);
        public virtual string State 
        {
            get => GetProperty(StateProperty); //1-2
            private set => LoadProperty(StateProperty, value); //2-3   
        }

        public static readonly PropertyInfo<string> PostCodeProperty = RegisterProperty<string>(o => o.PostCode);
        public virtual string PostCode 
        {
            get => GetProperty(PostCodeProperty); //1-2
            private set => LoadProperty(PostCodeProperty, value); //2-3   
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(o => o.Notes);
        public virtual string Notes 
        {
            get => GetProperty(NotesProperty); //1-2
            private set => LoadProperty(NotesProperty, value); //2-3   
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(o => o.LastUpdatedBy);
        public virtual string LastUpdatedBy 
        {
            get => GetProperty(LastUpdatedByProperty); //1-2
            private set => LoadProperty(LastUpdatedByProperty, value); //2-3   
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty = RegisterProperty<SmartDate>(o => o.LastUpdatedDate);
        public virtual SmartDate LastUpdatedDate 
        {
            get => GetProperty(LastUpdatedDateProperty); //1-2
            private set => LoadProperty(LastUpdatedDateProperty, value); //2-3   
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); //1-2
            private set => LoadProperty(RowVersionProperty, value); //2-3   
        }

        #endregion 

        #region Factory Methods
        internal static async Task<AddressROC> GetAddressROC(Address childData)
        {
            return await DataPortal.FetchChildAsync<AddressROC>(childData);
        }  


        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(Address data)
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

        #endregion
    }
}
