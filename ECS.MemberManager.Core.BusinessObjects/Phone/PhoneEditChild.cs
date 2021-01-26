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
    public class PhoneEditChild : BusinessBase<PhoneEditChild>
    {
        #region Business Methods
        
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<string> PhoneTypeProperty = RegisterProperty<string>(p => p.PhoneType);
        [Required,MaxLength(10)]
        public string PhoneType
        {
            get => GetProperty(PhoneTypeProperty);
            set => SetProperty(PhoneTypeProperty, value);
        }
        
        public static readonly PropertyInfo<string> AreaCodeProperty = RegisterProperty<string>(p => p.AreaCode);
        [Required,MaxLength(3)]
        public string AreaCode
        {
            get => GetProperty(AreaCodeProperty);
            set => SetProperty(AreaCodeProperty, value);
        }
       
        public static readonly PropertyInfo<string> NumberProperty = RegisterProperty<string>(p => p.Number);
        [Required,MaxLength(25)]
        public string Number
        {
            get => GetProperty(NumberProperty);
            set => SetProperty(NumberProperty, value);
        }

        public static readonly PropertyInfo<string> ExtensionProperty = RegisterProperty<string>(p => p.Extension);
        [MaxLength(25)]
        public string Extension
        {
            get => GetProperty(ExtensionProperty);
            set => SetProperty(ExtensionProperty, value);
        }
        
        public static readonly PropertyInfo<int> DisplayOrderProperty = RegisterProperty<int>(p => p.DisplayOrder);
        public int DisplayOrder
        {
            get => GetProperty(DisplayOrderProperty);
            set => SetProperty(DisplayOrderProperty, value);
        }
        
        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(p => p.LastUpdatedBy);
        [Required,MaxLength(255)]
        public string LastUpdatedBy
        {
            get => GetProperty(LastUpdatedByProperty);
            set => SetProperty(LastUpdatedByProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty = RegisterProperty<SmartDate>(p => p.LastUpdatedDate);
        [Required]
        public SmartDate LastUpdatedDate
        {
            get => GetProperty(LastUpdatedDateProperty);
            set => SetProperty(LastUpdatedDateProperty, value);
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(p => p.Notes);
        public string Notes
        {
            get => GetProperty(NotesProperty);
            set => SetProperty(NotesProperty, value);
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(p => p.RowVersion);
        public byte[] RowVersion
        {
            get => GetProperty(RowVersionProperty);
            set => SetProperty(RowVersionProperty, value);
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

        public static async Task<PhoneEditChild> GetPhoneEditChild(Phone childData)
        {
            return await DataPortal.FetchChildAsync<PhoneEditChild>(childData);
        }

        #endregion
        
        #region DataPortal Methods
        
        [FetchChild]
        private void FetchChild(Phone childData)
        {
            using (BypassPropertyChecks)
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
           
        }
        
        [InsertChild]
        private async Task InsertChild()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPhoneDal>();
            var phoneToUpdate = new ECS.MemberManager.Core.EF.Domain.Phone();
            using (BypassPropertyChecks)
            {
                phoneToUpdate.PhoneType = PhoneType;
                phoneToUpdate.AreaCode = AreaCode;
                phoneToUpdate.Number = Number;
                phoneToUpdate.Extension = Extension;
                phoneToUpdate.DisplayOrder = DisplayOrder;
                phoneToUpdate.LastUpdatedDate = LastUpdatedDate;
                phoneToUpdate.LastUpdatedBy = LastUpdatedBy;
                phoneToUpdate.Notes = Notes;
                phoneToUpdate.RowVersion = RowVersion;
            }
            var insertedPhone = await dal.Insert(phoneToUpdate);
            Id = insertedPhone.Id;
            RowVersion = insertedPhone.RowVersion;
        }


        [UpdateChild]
        private async Task UpdateChild()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPhoneDal>();
            var phoneToUpdate = await dal.Fetch(Id);
            using (BypassPropertyChecks)
            {
                phoneToUpdate.PhoneType = PhoneType;
                phoneToUpdate.AreaCode = AreaCode;
                phoneToUpdate.Number = Number;
                phoneToUpdate.Extension = Extension;
                phoneToUpdate.DisplayOrder = DisplayOrder;
                phoneToUpdate.LastUpdatedDate = LastUpdatedDate;
                phoneToUpdate.LastUpdatedBy = LastUpdatedBy;
                phoneToUpdate.Notes = Notes;
                phoneToUpdate.RowVersion = RowVersion;
            }

            var updatedPhone = await dal.Update(phoneToUpdate);
            RowVersion = updatedPhone.RowVersion;
        }

        [DeleteSelfChild]
        private async Task DeleteSelfChild()
        {
            await Delete(this.Id);
        }
        
        [Delete]
        private async Task Delete(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPhoneDal>();
 
            await dal.Delete(id);
        }

        #endregion
    }
}