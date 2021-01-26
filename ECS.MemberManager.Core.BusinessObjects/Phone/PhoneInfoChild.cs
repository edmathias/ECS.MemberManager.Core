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
    public class PhoneInfoChild : ReadOnlyBase<PhoneInfoChild>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);

        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<string> PhoneTypeProperty = RegisterProperty<string>(p => p.PhoneType);

        public string PhoneType
        {
            get => GetProperty(PhoneTypeProperty);
            private set => LoadProperty(PhoneTypeProperty, value);
        }

        public static readonly PropertyInfo<string> AreaCodeProperty = RegisterProperty<string>(p => p.AreaCode);

        public string AreaCode
        {
            get => GetProperty(AreaCodeProperty);
            private set => LoadProperty(AreaCodeProperty, value);
        }

        public static readonly PropertyInfo<string> NumberProperty = RegisterProperty<string>(p => p.Number);

        public string Number
        {
            get => GetProperty(NumberProperty);
            private set => LoadProperty(NumberProperty, value);
        }

        public static readonly PropertyInfo<string> ExtensionProperty = RegisterProperty<string>(p => p.Extension);

        public string Extension
        {
            get => GetProperty(ExtensionProperty);
            private set => LoadProperty(ExtensionProperty, value);
        }

        public static readonly PropertyInfo<int> DisplayOrderProperty = RegisterProperty<int>(p => p.DisplayOrder);

        public int DisplayOrder
        {
            get => GetProperty(DisplayOrderProperty);
            private set => LoadProperty(DisplayOrderProperty, value);
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

        [Required]
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

        public static async Task<PhoneInfoChild> GetPhoneInfoChild(Phone childData)
        {
            return await DataPortal.FetchChildAsync<PhoneInfoChild>(childData);
        }

        #endregion

        #region DataPortal Methods

        [FetchChild]
        private void FetchChild(Phone childData)
        {
            Id = childData.Id;
            PhoneType = childData.PhoneType;
            AreaCode = childData.AreaCode;
            Number = childData.Number;
            Extension = childData.Extension;
            DisplayOrder = childData.DisplayOrder;
            LastUpdatedDate = childData.LastUpdatedDate;
            LastUpdatedBy = childData.LastUpdatedBy;
            Notes = childData.Notes;
            RowVersion = childData.RowVersion;
        }

        #endregion
    }
}