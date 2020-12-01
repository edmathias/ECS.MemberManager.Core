﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Csla;
using ECS.BizBricks.CRM.Core.EF.Domain;
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
            get { return GetProperty(IdProperty); }
            private set { LoadProperty(IdProperty, value); }
        }
       
        public static readonly PropertyInfo<string> EMailAddressProperty = RegisterProperty<string>(p => p.EMailAddress);
        [Required,MaxLength(255)]
        public string EMailAddress
        {
            get { return GetProperty(EMailAddressProperty); }
            set { SetProperty(EMailAddressProperty, value); }
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(p => p.LastUpdatedBy);
        [Required]
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
        
        public static readonly PropertyInfo<EMailTypeER> EMailTypeProperty = RegisterProperty<EMailTypeER>(p => p.EMailType);
        public EMailTypeER EMailType
        {
            get { return GetProperty(EMailTypeProperty); }
            set { SetProperty(EMailTypeProperty, value); }
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

        public async static Task<EMailER> NewEMail()
        {
            return await DataPortal.CreateAsync<EMailER>();
        }

        public async static Task<EMailER> GetEMail(int id)
        {
            return await DataPortal.FetchAsync<EMailER>(id);
        }

        public async static Task DeleteEMail(int id)
        {
            await DataPortal.DeleteAsync<EMailER>(id);
        }
        
        #endregion

        #region Data Access

        [Fetch]
        private void Fetch(int id)
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailDal>();
            var data = dal.Fetch(id);
            using (BypassPropertyChecks)
            {
                Id = data.Id;
                EMailAddress = data.EMailAddress;
                EMailType = EMailTypeER.NewEMailType().Result;
                EMailType.Id = data.EMailType.Id;
                EMailType.Description = data.EMailType.TypeDescription;
                EMailType.Notes = data.EMailType.Notes;
                LastUpdatedBy = data.LastUpdatedBy;
                LastUpdatedDate = data.LastUpdatedDate;
                Notes = data.Notes;
                // TODO: many-to-many
            }
        }

        [Insert]
        private void Insert()
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailDal>();
            using (BypassPropertyChecks)
            {
                var eMailToInsert = new EMail()
                {
                    EMailAddress = this.EMailAddress,
                    EMailType = new EMailType()
                        {Id = EMailType.Id, TypeDescription = EMailType.Description, Notes = EMailType.Notes},
                    LastUpdatedBy = this.LastUpdatedBy,
                    LastUpdatedDate = DateTime.Now,
                    Notes = this.Notes,
                    // TODO: many-to-many
                    Organizations = new List<Organization>(),
                    Persons = new List<Person>()
                };

                var id = dal.Insert(eMailToInsert);
                this.Id = id;
            }
        }

        [Update]
        private void Update()
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailDal>();
            using (BypassPropertyChecks)
            {
                var eMailToUpdate = new EMail()
                {
                    Id = this.Id,
                    EMailAddress = this.EMailAddress,
                    EMailType = new EMailType()
                        {Id = this.EMailType.Id, TypeDescription = this.EMailType.Description, Notes = this.EMailType.Notes},
                    LastUpdatedBy = this.LastUpdatedBy,
                    LastUpdatedDate = DateTime.Now,
                    Notes = this.Notes,
                    // TODO: many-to-many
                    Organizations = new List<Organization>(),
                    Persons = new List<Person>()
                };
                
                dal.Update(eMailToUpdate);
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
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailDal>();

            dal.Delete(id);
        }


        #endregion
       
    }
}