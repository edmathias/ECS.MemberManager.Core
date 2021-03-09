

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
    public partial class PhoneEC : BusinessBase<PhoneEC>
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
            set => SetProperty(PhoneTypeProperty, value); 
   
        }

        public static readonly PropertyInfo<string> AreaCodeProperty = RegisterProperty<string>(o => o.AreaCode);
        public virtual string AreaCode 
        {
            get => GetProperty(AreaCodeProperty); 
            set => SetProperty(AreaCodeProperty, value); 
   
        }

        public static readonly PropertyInfo<string> NumberProperty = RegisterProperty<string>(o => o.Number);
        public virtual string Number 
        {
            get => GetProperty(NumberProperty); 
            set => SetProperty(NumberProperty, value); 
   
        }

        public static readonly PropertyInfo<string> ExtensionProperty = RegisterProperty<string>(o => o.Extension);
        public virtual string Extension 
        {
            get => GetProperty(ExtensionProperty); 
            set => SetProperty(ExtensionProperty, value); 
   
        }

        public static readonly PropertyInfo<int> DisplayOrderProperty = RegisterProperty<int>(o => o.DisplayOrder);
        public virtual int DisplayOrder 
        {
            get => GetProperty(DisplayOrderProperty); 
            set => SetProperty(DisplayOrderProperty, value); 
   
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

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(o => o.Notes);
        public virtual string Notes 
        {
            get => GetProperty(NotesProperty); 
            set => SetProperty(NotesProperty, value); 
   
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); 
            set => SetProperty(RowVersionProperty, value); 
   
        }

        #endregion 

        #region Factory Methods
        internal static async Task<PhoneEC> NewPhoneEC()
        {
            return await DataPortal.CreateChildAsync<PhoneEC>();
        }

        internal static async Task<PhoneEC> GetPhoneEC(Phone childData)
        {
            return await DataPortal.FetchChildAsync<PhoneEC>(childData);
        }  


        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(Phone data)
        {
            using(BypassPropertyChecks)
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
        }
        [InsertChild]
        private async Task Insert()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPhoneDal>();
            var data = new Phone()
            {

                Id = Id,
                PhoneType = PhoneType,
                AreaCode = AreaCode,
                Number = Number,
                Extension = Extension,
                DisplayOrder = DisplayOrder,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
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
            var dal = dalManager.GetProvider<IPhoneDal>();
            var data = new Phone()
            {

                Id = Id,
                PhoneType = PhoneType,
                AreaCode = AreaCode,
                Number = Number,
                Extension = Extension,
                DisplayOrder = DisplayOrder,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Update(data);
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
            var dal = dalManager.GetProvider<IPhoneDal>();
           
            await dal.Delete(id);
        }

        #endregion
    }
}
