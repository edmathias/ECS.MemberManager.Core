﻿using System;
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
    public class EMailER : BusinessBase<EMailER>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<EMailTypeROC> EMailTypeProperty = RegisterProperty<EMailTypeROC>(p => p.EMailType);
        public EMailTypeROC EMailType
        {
            get => GetProperty(EMailTypeProperty);
            set => SetProperty(EMailTypeProperty, value);
        }

        public static readonly PropertyInfo<string> EMailAddressProperty = RegisterProperty<string>(p => p.EMailAddress);
        [Required,MaxLength(255)]
        public string EMailAddress
        {
            get => GetProperty(EMailAddressProperty);
            set => SetProperty(EMailAddressProperty, value);
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
        private async Task FetchAsync(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailDal>();
            var data = await dal.Fetch(id);

            await Fetch(data);
        }
        
        private async Task Fetch(EMail childData)
        {
            using (BypassPropertyChecks)
            {
                Id = childData.Id;
                EMailAddress = childData.EMailAddress;
                EMailType = await EMailTypeROC.GetEMailTypeROC(childData.EMailType);
                LastUpdatedBy = childData.LastUpdatedBy;
                LastUpdatedDate = childData.LastUpdatedDate;
                Notes = childData.Notes;
                RowVersion = childData.RowVersion;
            }
        }

        [Insert]
        private async Task Insert()
        {
            await InsertChild();
        }

        private async Task InsertChild()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailDal>();

            var data = new EMail()
            {
                EMailType = new EMailType()
                {
                    Id = EMailType.Id,
                    Description = EMailType.Description,
                    Notes = EMailType.Notes,
                    RowVersion = EMailType.RowVersion
                }, 
                EMailAddress = EMailAddress,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes
            };
 
            var insertedEMail = await dal.Insert(data);
            Id = insertedEMail.Id;
            RowVersion = insertedEMail.RowVersion;
        }

        [Update]
        private async Task Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailDal>();

            var data = new EMail()
            {
                Id = Id, 
                EMailType = new EMailType()
                {
                    Id = EMailType.Id,
                    Description = EMailType.Description,
                    Notes = EMailType.Notes,
                    RowVersion = EMailType.RowVersion
                }, 
                EMailAddress = EMailAddress,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
                RowVersion = RowVersion
            };
 
            var insertedEMail = await dal.Update(data);
            RowVersion = insertedEMail.RowVersion;
        }
        
        [DeleteSelf]
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