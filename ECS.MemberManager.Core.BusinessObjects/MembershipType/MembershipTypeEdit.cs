using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Csla;
using Csla.Serialization.Mobile;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class MembershipTypeEdit : BusinessBase<MembershipTypeEdit>
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

        public static readonly PropertyInfo<string> LastUpdatedByProperty =
            RegisterProperty<string>(p => p.LastUpdatedBy);

        [Required, MaxLength(255)]
        public string LastUpdatedBy
        {
            get => GetProperty(LastUpdatedByProperty);
            set => SetProperty(LastUpdatedByProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty =
            RegisterProperty<SmartDate>(p => p.LastUpdatedDate);

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

        #endregion

        #region Factory Methods

        public static async Task<MembershipTypeEdit> NewMembershipTypeEdit()
        {
            return await DataPortal.CreateAsync<MembershipTypeEdit>();
        }

        public static async Task<MembershipTypeEdit> GetMembershipTypeEdit(int id)
        {
            return await DataPortal.FetchAsync<MembershipTypeEdit>(id);
        }

        public static async Task<MembershipTypeEdit> GetMembershipTypeEdit(MembershipType childData)
        {
            return await DataPortal.FetchChildAsync<MembershipTypeEdit>(childData);
        }
        
        
        public static async Task DeleteMembershipTypeEdit(int id)
        {
            await DataPortal.DeleteAsync<MembershipTypeEdit>(id);
        }

        #endregion

        #region DataPortal Methods

        [FetchChild]
        private void Fetch(MembershipType childData)
        {
            using (BypassPropertyChecks)
            {
                Id = childData.Id;
                Description = childData.Description;
                Level = childData.Level;
                LastUpdatedBy = childData.LastUpdatedBy;
                LastUpdatedDate = childData.LastUpdatedDate;
                Notes = childData.Notes;
                RowVersion = childData.RowVersion;
            }
        }

        [Fetch]
        private async Task Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMembershipTypeDal>();
            var childData = await dal.Fetch(id);

            Fetch(childData);
        }

         
        [Insert]
        private async Task Insert()
        {
            await InsertChild();
        }


        [InsertChild]
        private async Task InsertChild()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMembershipTypeDal>();

            var data = new MembershipType()
            {
                Description = Description,
                LastUpdatedDate = LastUpdatedDate,
                LastUpdatedBy = LastUpdatedBy,
                Notes = Notes,
                Level = Level
            };

            var insertedMembership = await dal.Insert(data);
            Id = insertedMembership.Id;
            RowVersion = insertedMembership.RowVersion;
        }

        [Update]
        private async Task Update()
        {
            await UpdateChild();
        }

        [UpdateChild]
        private async Task UpdateChild()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMembershipTypeDal>();

            var membershipTypeToUpdate = new MembershipType()
            {
                Id = Id,
                Description = Description,
                Level = Level,
                LastUpdatedDate = LastUpdatedDate,
                LastUpdatedBy = LastUpdatedBy,
                Notes = this.Notes,
                RowVersion = RowVersion
            };

            var updatedMembership = await dal.Update(membershipTypeToUpdate);
            RowVersion = updatedMembership.RowVersion;
        }

        [DeleteSelfChild]
        private async Task DeleteSelf()
        {
            await Delete(this.Id);
        }

        [Delete]
        private async Task Delete(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMembershipTypeDal>();

            await dal.Delete(id);
        }

        #endregion
    }
}