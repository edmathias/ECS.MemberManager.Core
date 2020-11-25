﻿using System;
using System.ComponentModel.DataAnnotations;
using Csla;
using Csla.Rules.CommonRules;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class MembershipTypeER : BusinessBase<MembershipTypeER>
    {
        #region Business Methods
        
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            private set { LoadProperty(IdProperty, value); }
        }

        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(p => p.Description);
        [Required, MaxLength(50)]
        public string Description
        {
            get { return GetProperty(DescriptionProperty); }
            set { SetProperty(DescriptionProperty, value); }
        }
      
        public static readonly PropertyInfo<int> LevelProperty = RegisterProperty<int>(p => p.Level);
        public int Level
        {
            get { return GetProperty(LevelProperty); }
            set { SetProperty(LevelProperty, value); }
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

        public static MembershipTypeER NewMembershipTypeER()
        {
            return DataPortal.Create<MembershipTypeER>();
        }

        public static MembershipTypeER GetMembershipTypeER(int id)
        {
            return DataPortal.Fetch<MembershipTypeER>(id);
        }

        public static void DeleteMembershipTypeER(int id)
        {
            DataPortal.Delete<MembershipTypeER>(id);
        }
        
        #endregion
        
        #region DataPortal Methods
               [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Fetch(int id)
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMembershipTypeDal>();
            var data = dal.Fetch(id);
            using (BypassPropertyChecks)
            {
                Id = data.Id;
                Description = data.Description;
                Level = data.Level;
                LastUpdatedBy = data.LastUpdatedBy;
                LastUpdatedDate = data.LastUpdatedDate;
                Notes = data.Notes;
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMembershipTypeDal>();
            var documentTypeToInsert = new ECS.MemberManager.Core.EF.Domain.MembershipType();
            using (BypassPropertyChecks)
            {
                documentTypeToInsert.Description = this.Description;
                documentTypeToInsert.LastUpdatedDate = this.LastUpdatedDate;
                documentTypeToInsert.LastUpdatedBy = this.LastUpdatedBy;
                documentTypeToInsert.Notes = this.Notes;
                documentTypeToInsert.Level = this.Level;
            }
            Id = dal.Insert(documentTypeToInsert);
        }
        
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMembershipTypeDal>();
            var documentTypeToUpdate = dal.Fetch(Id);
            using (BypassPropertyChecks)
            {
                documentTypeToUpdate.Description = this.Description;
                documentTypeToUpdate.Level = this.Level;
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
            var dal = dalManager.GetProvider<IMembershipTypeDal>();
 
            dal.Delete(id);
        }
        
        #endregion
    }
}