

//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/25/2021 11:07:23
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
    public partial class AddressROC : ReadOnlyBase<AddressROC>
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
            private set => LoadProperty(Address1Property, value);    
        }

        public static readonly PropertyInfo<string> Address2Property = RegisterProperty<string>(o => o.Address2);
        public virtual string Address2 
        {
            get => GetProperty(Address2Property); 
            private set => LoadProperty(Address2Property, value);    
        }

        public static readonly PropertyInfo<string> CityProperty = RegisterProperty<string>(o => o.City);
        public virtual string City 
        {
            get => GetProperty(CityProperty); 
            private set => LoadProperty(CityProperty, value);    
        }

        public static readonly PropertyInfo<string> StateProperty = RegisterProperty<string>(o => o.State);
        public virtual string State 
        {
            get => GetProperty(StateProperty); 
            private set => LoadProperty(StateProperty, value);    
        }

        public static readonly PropertyInfo<string> PostCodeProperty = RegisterProperty<string>(o => o.PostCode);
        public virtual string PostCode 
        {
            get => GetProperty(PostCodeProperty); 
            private set => LoadProperty(PostCodeProperty, value);    
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(o => o.Notes);
        public virtual string Notes 
        {
            get => GetProperty(NotesProperty); 
            private set => LoadProperty(NotesProperty, value);    
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(o => o.LastUpdatedBy);
        public virtual string LastUpdatedBy 
        {
            get => GetProperty(LastUpdatedByProperty); 
            private set => LoadProperty(LastUpdatedByProperty, value);    
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty = RegisterProperty<SmartDate>(o => o.LastUpdatedDate);
        public virtual SmartDate LastUpdatedDate 
        {
            get => GetProperty(LastUpdatedDateProperty); 
            private set => LoadProperty(LastUpdatedDateProperty, value);    
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); 
            private set => LoadProperty(RowVersionProperty, value);    
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
