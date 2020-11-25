using System;
using System.ComponentModel.DataAnnotations;
using Csla;
using Csla.Rules.CommonRules;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class PhoneER : BusinessBase<PhoneER>
    {
        #region Business Methods
        
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            private set { LoadProperty(IdProperty, value); }
        }

        public static readonly PropertyInfo<string> PhoneTypeProperty = RegisterProperty<string>(p => p.PhoneType);
        [Required,MaxLength(10)]
        public string PhoneType
        {
            get { return GetProperty(PhoneTypeProperty); }
            set { SetProperty(PhoneTypeProperty, value); }
        }
        
        public static readonly PropertyInfo<string> AreaCodeProperty = RegisterProperty<string>(p => p.AreaCode);
        [Required,MaxLength(3)]
        public string AreaCode
        {
            get { return GetProperty(AreaCodeProperty); }
            set { SetProperty(AreaCodeProperty, value); }
        }
       
        public static readonly PropertyInfo<string> NumberProperty = RegisterProperty<string>(p => p.Number);
        [Required,MaxLength(25)]
        public string Number
        {
            get { return GetProperty(NumberProperty); }
            set { SetProperty(NumberProperty, value); }
        }

        public static readonly PropertyInfo<string> ExtensionProperty = RegisterProperty<string>(p => p.Extension);
        [Required,MaxLength(25)]
        public string Extension
        {
            get { return GetProperty(ExtensionProperty); }
            set { SetProperty(ExtensionProperty, value); }
        }
        
        public static readonly PropertyInfo<int> DisplayOrderProperty = RegisterProperty<int>(p => p.DisplayOrder);
        public int DisplayOrder
        {
            get { return GetProperty(DisplayOrderProperty); }
            set { SetProperty(DisplayOrderProperty, value); }
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

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(p => p.Notes);
        public string Notes
        {
            get { return GetProperty(NotesProperty); }
            set { SetProperty(NotesProperty, value); }
        }
        
        #endregion
        
        #region Factory Methods

        public static PhoneER NewPhoneER()
        {
            return DataPortal.Create<PhoneER>();
        }

        public static PhoneER GetPhoneER(int id)
        {
            return DataPortal.Fetch<PhoneER>(id);
        }

        public static void DeletePhoneER(int id)
        {
            DataPortal.Delete<PhoneER>(id);
        }
        
        #endregion
        
        #region DataPortal Methods
        
                [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Fetch(int id)
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPhoneDal>();
            var data = dal.Fetch(id);
            using (BypassPropertyChecks)
            {
                Id = data.Id;
                LastUpdatedBy = data.LastUpdatedBy;
                LastUpdatedDate = data.LastUpdatedDate;
                Notes = data.Notes;
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPhoneDal>();
            var documentTypeToInsert = new ECS.MemberManager.Core.EF.Domain.Phone();
            using (BypassPropertyChecks)
            {
                documentTypeToInsert.LastUpdatedDate = this.LastUpdatedDate;
                documentTypeToInsert.LastUpdatedBy = this.LastUpdatedBy;
                documentTypeToInsert.Notes = this.Notes; 
            }
            Id = dal.Insert(documentTypeToInsert);
        }
        
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPhoneDal>();
            var documentTypeToUpdate = dal.Fetch(Id);
            using (BypassPropertyChecks)
            {
                documentTypeToUpdate.LastUpdatedDate = this.LastUpdatedDate;
                documentTypeToUpdate.LastUpdatedBy = this.LastUpdatedBy;
                documentTypeToUpdate.Notes = this.Notes; 
            }

            dal.Update(documentTypeToUpdate);
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(this.Id);
        }
        
        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(int id)
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPhoneDal>();
 
            dal.Delete(id);
        }

        #endregion
    }
}