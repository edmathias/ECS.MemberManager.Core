using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Csla;
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
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(p => p.Description);
        [Required, MaxLength(50)]
        public string Description
        {
            get => GetProperty(DescriptionProperty);
            set => SetProperty(DescriptionProperty, value);
        }
      
        public static readonly PropertyInfo<int> LevelProperty = RegisterProperty<int>(p => p.Level);
        public int Level
        {
            get => GetProperty(LevelProperty);
            set => SetProperty(LevelProperty, value);
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
        
        #endregion
        
        #region Factory Methods

        public static async Task<MembershipTypeER> NewMembershipType()
        {
            return await DataPortal.CreateAsync<MembershipTypeER>();
        }

        public static async Task<MembershipTypeER> GetMembershipType(int id)
        {
            return await DataPortal.FetchAsync<MembershipTypeER>(id);
        }

        public static async Task DeleteMembershipType(int id)
        {
            await DataPortal.DeleteAsync<MembershipTypeER>(id);
        }
        
        #endregion
        
        #region DataPortal Methods
        
        [Fetch]
        private void Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
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

        [Insert]
        private void Insert()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMembershipTypeDal>();
            var membershipTypeToInsert = new ECS.MemberManager.Core.EF.Domain.MembershipType();
            using (BypassPropertyChecks)
            {
                membershipTypeToInsert.Description = this.Description;
                membershipTypeToInsert.LastUpdatedDate = this.LastUpdatedDate;
                membershipTypeToInsert.LastUpdatedBy = this.LastUpdatedBy;
                membershipTypeToInsert.Notes = this.Notes;
                membershipTypeToInsert.Level = this.Level;
            }
            Id = dal.Insert(membershipTypeToInsert);
        }

        [Update] 
        private void Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMembershipTypeDal>();
            var membershipTypeToUpdate = dal.Fetch(Id);
            using (BypassPropertyChecks)
            {
                membershipTypeToUpdate.Description = this.Description;
                membershipTypeToUpdate.Level = this.Level;
                membershipTypeToUpdate.LastUpdatedDate = this.LastUpdatedDate;
                membershipTypeToUpdate.LastUpdatedBy = this.LastUpdatedBy;
                membershipTypeToUpdate.Notes = this.Notes; 
            }

            dal.Update(membershipTypeToUpdate);
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
            var dal = dalManager.GetProvider<IMembershipTypeDal>();
 
            dal.Delete(id);
        }
        
        #endregion
    }
}