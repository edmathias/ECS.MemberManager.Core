using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class MembershipTypeEC : BusinessBase<MembershipTypeEC>
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

        #endregion

        #region Factory Methods

        public static async Task<MembershipTypeEC> NewMembershipType()
        {
            return await DataPortal.CreateChildAsync<MembershipTypeEC>();
        }

        public static async Task<MembershipTypeEC> GetMembershipType(MembershipType childData)
        {
            return await DataPortal.FetchChildAsync<MembershipTypeEC>(childData);
        }

        public static async Task DeleteMembershipType(int id)
        {
            await DataPortal.DeleteAsync<MembershipTypeEC>(id);
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
            }
        }

        [InsertChild]
        private void InsertChild()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMembershipTypeDal>();

            var documentTypeToInsert = new MembershipType()
            {
                Description = Description,
                LastUpdatedDate = LastUpdatedDate,
                LastUpdatedBy = LastUpdatedBy,
                Notes = Notes,
                Level = Level
            };

            Id = dal.Insert(documentTypeToInsert);
        }

        [UpdateChild]
        private void UpdateChild()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMembershipTypeDal>();
            
            var documentTypeToUpdate = dal.Fetch(Id);
            documentTypeToUpdate.Description = Description;
            documentTypeToUpdate.Level = Level;
            documentTypeToUpdate.LastUpdatedDate = LastUpdatedDate;
            documentTypeToUpdate.LastUpdatedBy = LastUpdatedBy;
            documentTypeToUpdate.Notes = Notes;

            dal.Update(documentTypeToUpdate);
        }

        [DeleteSelfChild]
        private void DeleteSelf()
        {
            Delete(Id);
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