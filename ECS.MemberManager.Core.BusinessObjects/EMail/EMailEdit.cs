﻿using System;
using System.Collections.Generic;
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
    public class EMailEdit : BusinessBase<EMailEdit>
    {
        #region Business Methods
        
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }
       
        public static readonly PropertyInfo<string> EMailAddressProperty = RegisterProperty<string>(p => p.EMailAddress);
        [Required,MaxLength(255),EmailAddress()]
        public string EMailAddress
        {
            get => GetProperty(EMailAddressProperty);
            set => SetProperty(EMailAddressProperty, value);
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(p => p.LastUpdatedBy);
        [Required]
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
        
        public static readonly PropertyInfo<EMailTypeEdit> EMailTypeProperty = RegisterProperty<EMailTypeEdit>(p => p.EMailType);
        [Required]
        public EMailTypeEdit EMailType
        {
            get => GetProperty(EMailTypeProperty);
            set => SetProperty(EMailTypeProperty, value);
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

        public static async Task<EMailEdit> NewEMail()
        {
            return await DataPortal.CreateAsync<EMailEdit>();
        }

        public static async Task<EMailEdit> GetEMail(int id)
        {
            return await DataPortal.FetchAsync<EMailEdit>(id);
        }

        public static async Task DeleteEMail(int id)
        {
            await DataPortal.DeleteAsync<EMailEdit>(id);
        }
        
        #endregion

        #region Data Access

        [Fetch]
        private void Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var eMailDal = dalManager.GetProvider<IEMailDal>();
            var eMail = eMailDal.Fetch(id);
            var eMailTypeDal = dalManager.GetProvider<IEMailTypeDal>();
            var eMailType = eMailTypeDal.Fetch(eMail.EMailTypeId); 
            using (BypassPropertyChecks)
            {
                Id = eMail.Id;
                EMailAddress = eMail.EMailAddress;
                EMailType = DataPortal.FetchChild<EMailTypeEdit>(eMailType);
                LastUpdatedBy = eMail.LastUpdatedBy;
                LastUpdatedDate = eMail.LastUpdatedDate;
                Notes = eMail.Notes;
                RowVersion = eMail.RowVersion;
            }
        }

        [Fetch]
        private void Fetch(EMail childData)
        {
            using var dalManager = DalFactory.GetManager();
            var eMailDal = dalManager.GetProvider<IEMailDal>();
            var eMail = eMailDal.Fetch(Id);
            var eMailTypeDal = dalManager.GetProvider<IEMailTypeDal>();
            var eMailType = eMailTypeDal.Fetch(eMail.EMailTypeId); 
            using (BypassPropertyChecks)
            {
                Id = eMail.Id;
                EMailAddress = eMail.EMailAddress;
                EMailType = DataPortal.FetchChild<EMailTypeEdit>(eMailType);
                LastUpdatedBy = eMail.LastUpdatedBy;
                LastUpdatedDate = eMail.LastUpdatedDate;
                Notes = eMail.Notes;
                RowVersion = eMail.RowVersion;
                // TODO: many-to-many
            }
        }
        
        [Insert]
        private void Insert()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailDal>();
            using (BypassPropertyChecks)
            {
                var eMailToInsert = new EMail()
                {
                    EMailAddress = EMailAddress,
                    EMailTypeId = EMailType.Id,
                    LastUpdatedBy = LastUpdatedBy,
                    LastUpdatedDate = LastUpdatedDate,
                    Notes = Notes,
                };

                var insertedEMail = dal.Insert(eMailToInsert);
                Id = insertedEMail.Id;
                RowVersion = insertedEMail.RowVersion;
            }
        }

        [Update]
        private void Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailDal>();
            using (BypassPropertyChecks)
            {
                var eMailToUpdate = new EMail()
                {
                    Id = this.Id,
                    EMailAddress = this.EMailAddress,
                    EMailTypeId = EMailType.Id,
                    LastUpdatedBy = this.LastUpdatedBy,
                    LastUpdatedDate = this.LastUpdatedDate,
                    Notes = this.Notes,
                    RowVersion = this.RowVersion,
                };
                
                RowVersion = dal.Update(eMailToUpdate).RowVersion;
            }
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
            var dal = dalManager.GetProvider<IEMailDal>();

            dal.Delete(id);
        }


        #endregion
       
    }
}