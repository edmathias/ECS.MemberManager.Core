﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class MemberStatusER : BusinessBase<MemberStatusER>
    {
        #region Business Methods
        
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(p => p.Description);
        [Required,MaxLength(50)]
        public string Description
        {
            get => GetProperty(DescriptionProperty);
            set => SetProperty(DescriptionProperty, value);
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(p => p.Notes);
        public string Notes
        {
            get => GetProperty(NotesProperty);
            set => SetProperty(NotesProperty, value);
        }
        
        #endregion
        
        #region Factory Methods

        public static async Task<MemberStatusER> NewMemberStatus()
        {
            return await DataPortal.CreateAsync<MemberStatusER>();
        }

        public static async Task<MemberStatusER> GetMemberStatus(int id)
        {
            return await DataPortal.FetchAsync<MemberStatusER>(id);
        }

        public static async Task DeleteMemberStatus(int id)
        {
            await DataPortal.DeleteAsync<MemberStatusER>(id);
        }
        
        #endregion
        
        #region Data Access 

        [Fetch]
        private void Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMemberStatusDal>();
            var data = dal.Fetch(id);
            using (BypassPropertyChecks)
            {
                Id = data.Id;
                Description = data.Description;
                Notes = data.Notes;
            }
        }

        [Insert] 
        private void Insert()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMemberStatusDal>();
            using (BypassPropertyChecks)
            {
                var memberStatus = new ECS.MemberManager.Core.EF.Domain.MemberStatus 
                    { Description = this.Description, Notes = this.Notes };
                Id = dal.Insert(memberStatus);
            }
        }
        
        [Update]
        protected override void DataPortal_Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMemberStatusDal>();
            using (BypassPropertyChecks)
            {
                var memberStatus = new MemberStatus
                    {Id = this.Id, Description = this.Description, Notes = this.Notes};
                dal.Update(memberStatus);
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
            var dal = dalManager.GetProvider<IMemberStatusDal>();
 
            dal.Delete(id);
        }
        
        #endregion
        
    }
}