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
    public class PhoneEC : BusinessBase<PhoneEC>
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
        [Required,MaxLength(25)]
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

        public static readonly PropertyInfo<DateTime> LastUpdatedDateProperty = RegisterProperty<DateTime>(p => p.LastUpdatedDate);
        [Required]
        public DateTime LastUpdatedDate
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

        public static async Task<PhoneEC> NewPhone()
        {
            return await DataPortal.CreateAsync<PhoneEC>();
        }

        public static async Task<PhoneEC> GetPhone(Phone childData)
        {
            return await DataPortal.FetchAsync<PhoneEC>(childData);
        }

        public static async Task DeletePhone(int id)
        {
            await DataPortal.DeleteAsync<PhoneEC>(id);
        }
        
        #endregion
        
        #region DataPortal Methods
        
        
        [Fetch]
        private void Fetch(Phone childData)
        {
            using (BypassPropertyChecks)
            {
                Id = childData.Id;
                AreaCode = childData.AreaCode;                
                Number = childData.Number;
                Extension = childData.Extension;
                DisplayOrder = childData.DisplayOrder;
                LastUpdatedBy = childData.LastUpdatedBy;
                LastUpdatedDate = childData.LastUpdatedDate;
                Notes = childData.Notes;
            }
        }

        [Insert]
        private void Insert()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPhoneDal>();
            var phoneToInsert = new Phone();
            using (BypassPropertyChecks)
            {
                phoneToInsert.Id = Id;
                phoneToInsert.AreaCode = AreaCode;                
                phoneToInsert.Number = Number;
                phoneToInsert.Extension = Extension;
                phoneToInsert.DisplayOrder = DisplayOrder;
                phoneToInsert.LastUpdatedBy = LastUpdatedBy;
                phoneToInsert.LastUpdatedDate = LastUpdatedDate;
                phoneToInsert.Notes = Notes;
            }
            Id = dal.Insert(phoneToInsert);
        }
        
        [Update]
        private void Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPhoneDal>();
            var phoneToUpdate = dal.Fetch(Id);
            using (BypassPropertyChecks)
            {
                phoneToUpdate.AreaCode = AreaCode;                
                phoneToUpdate.Number = Number;
                phoneToUpdate.Extension = Extension;
                phoneToUpdate.DisplayOrder = DisplayOrder;
                phoneToUpdate.LastUpdatedBy = LastUpdatedBy;
                phoneToUpdate.LastUpdatedDate = LastUpdatedDate;
                phoneToUpdate.Notes = Notes;
            }

            dal.Update(phoneToUpdate);
        }

        [DeleteSelf]
        private void DeleteSelf()
        {
            Delete(this.Id);
        }
        
        [Delete]
        private void Delete(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPhoneDal>();
 
            dal.Delete(id);
        }

        #endregion
    }
}