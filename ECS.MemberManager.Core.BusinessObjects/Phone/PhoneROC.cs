

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
    public partial class PhoneROC : ReadOnlyBase<PhoneROC>
    {
        #region Business Methods
 
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public virtual int Id 
        {
            get => GetProperty(IdProperty); 
            private set => LoadProperty(IdProperty, value);    
        }

        public static readonly PropertyInfo<string> PhoneTypeProperty = RegisterProperty<string>(o => o.PhoneType);
        public virtual string PhoneType 
        {
            get => GetProperty(PhoneTypeProperty); 
            private set => LoadProperty(PhoneTypeProperty, value);    
        }

        public static readonly PropertyInfo<string> AreaCodeProperty = RegisterProperty<string>(o => o.AreaCode);
        public virtual string AreaCode 
        {
            get => GetProperty(AreaCodeProperty); 
            private set => LoadProperty(AreaCodeProperty, value);    
        }

        public static readonly PropertyInfo<string> NumberProperty = RegisterProperty<string>(o => o.Number);
        public virtual string Number 
        {
            get => GetProperty(NumberProperty); 
            private set => LoadProperty(NumberProperty, value);    
        }

        public static readonly PropertyInfo<string> ExtensionProperty = RegisterProperty<string>(o => o.Extension);
        public virtual string Extension 
        {
            get => GetProperty(ExtensionProperty); 
            private set => LoadProperty(ExtensionProperty, value);    
        }

        public static readonly PropertyInfo<int> DisplayOrderProperty = RegisterProperty<int>(o => o.DisplayOrder);
        public virtual int DisplayOrder 
        {
            get => GetProperty(DisplayOrderProperty); 
            private set => LoadProperty(DisplayOrderProperty, value);    
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
        internal static async Task<PhoneROC> GetPhoneROC(Phone childData)
        {
            return await DataPortal.FetchChildAsync<PhoneROC>(childData);
        }  


        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(Phone data)
        {
                Id = data.Id;
                PhoneType = data.PhoneType;
                AreaCode = data.AreaCode;
                Number = data.Number;
                Extension = data.Extension;
                DisplayOrder = data.DisplayOrder;
                LastUpdatedBy = data.LastUpdatedBy;
                LastUpdatedDate = data.LastUpdatedDate;
                Notes = data.Notes;
                RowVersion = data.RowVersion;
        }

        #endregion
    }
}
