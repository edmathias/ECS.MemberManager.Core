

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
    public partial class EMailER : BusinessBase<EMailER>
    {
        #region Business Methods
 
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public virtual int Id 
        {
            get => GetProperty(IdProperty); //1-2
            private set => LoadProperty(IdProperty, value); //2-3   
        }

        public static readonly PropertyInfo<string> EMailAddressProperty = RegisterProperty<string>(o => o.EMailAddress);
        public virtual string EMailAddress 
        {
            get => GetProperty(EMailAddressProperty); //1-2
            set => SetProperty(EMailAddressProperty, value); //2-4
   
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

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(o => o.Notes);
        public virtual string Notes 
        {
            get => GetProperty(NotesProperty); //1-2
            set => SetProperty(NotesProperty, value); //2-4
   
        }


        public static readonly PropertyInfo<EMailTypeEC> EMailTypeProperty = RegisterProperty<EMailTypeEC>(o => o.EMailType);
        public EMailTypeEC EMailType  
        {
            get => GetProperty(EMailTypeProperty); //1-1
            set => SetProperty(EMailTypeProperty, value); //2-2
        }    
 
        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); //1-2
            set => SetProperty(RowVersionProperty, value); //2-4
   
        }

        #endregion 

        #region Factory Methods
        public static async Task<EMailER> NewEMailER()
        {
            return await DataPortal.CreateAsync<EMailER>();
        }

        public static async Task<EMailER> GetEMailER(int id)
        {
            return await DataPortal.FetchAsync<EMailER>(id);
        }  

        public static async Task DeleteEMailER(int id)
        {
            await DataPortal.DeleteAsync<EMailER>(id);
        } 


        #endregion

        #region Data Access Methods

        [Fetch]
        private async Task Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailDal>();
            var data = await dal.Fetch(id);
            using(BypassPropertyChecks)
            {
                Id = data.Id;
                EMailAddress = data.EMailAddress;
                LastUpdatedBy = data.LastUpdatedBy;
                LastUpdatedDate = data.LastUpdatedDate;
                Notes = data.Notes;
                EMailType = (data.EMailType != null ? await EMailTypeEC.GetEMailTypeEC(data.EMailType) : null);
                RowVersion = data.RowVersion;
            }            
        }
        [Insert]
        private async Task Insert()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailDal>();
            var data = new EMail()
            {

                Id = Id,
                EMailAddress = EMailAddress,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
                EMailType = (EMailType != null ? new EMailType() { Id = EMailType.Id } : null),
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Insert(data);
            Id = insertedObj.Id;
            RowVersion = insertedObj.RowVersion;
        }

       [Update]
        private async Task Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailDal>();
            var data = new EMail()
            {

                Id = Id,
                EMailAddress = EMailAddress,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
                EMailType = (EMailType != null ? new EMailType() { Id = EMailType.Id } : null),
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
            var dal = dalManager.GetProvider<IEMailDal>();
           
            await dal.Delete(id);
        }

        #endregion
    }
}
