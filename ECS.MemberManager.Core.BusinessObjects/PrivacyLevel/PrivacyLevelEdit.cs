using System;
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
    public class PrivacyLevelEdit : BusinessBase<PrivacyLevelEdit>
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

        public static async Task<PrivacyLevelEdit> NewPrivacyLevelEdit()
        {
            return await DataPortal.CreateAsync<PrivacyLevelEdit>();
        }

        public static async Task<PrivacyLevelEdit> GetPrivacyLevelEdit(int id)
        {
            return await DataPortal.FetchAsync<PrivacyLevelEdit>(id);
        }

        public static async Task<PrivacyLevelEdit> GetPrivacyLevelEdit(PrivacyLevel childData)
        {
            return await DataPortal.FetchChildAsync<PrivacyLevelEdit>(childData);
        }
        
        public static async Task DeletePrivacyLevelEdit(int id)
        {
            await DataPortal.DeleteAsync<PrivacyLevelEdit>(id);
        }
        
        #endregion
        
        #region DataPortal Methods
                
        [Fetch]
        private async Task Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPrivacyLevelDal>();
            var data = await dal.Fetch(id);

            Fetch(data);
        }

        [FetchChild]
        private void Fetch(PrivacyLevel childData)
        {
            using (BypassPropertyChecks)
            {
                Id = childData.Id;
                Description = childData.Description;
                Notes = childData.Notes;
                RowVersion = childData.RowVersion;
            }
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
            var dal = dalManager.GetProvider<IPrivacyLevelDal>();
            using (BypassPropertyChecks)
            {
                var documentTypeToInsert = new PrivacyLevel
                {
                    Description = this.Description,
                    Notes = this.Notes
                };
                var insertedPrivacyLevel = await dal.Insert(documentTypeToInsert);
                Id = insertedPrivacyLevel.Id;
                RowVersion = insertedPrivacyLevel.RowVersion;

            }
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
            var dal = dalManager.GetProvider<IPrivacyLevelDal>();
            using (BypassPropertyChecks)
            {
                var documentTypeToUpdate = new PrivacyLevel()
                {
                    Id = this.Id,
                    Description = this.Description,
                    Notes = this.Notes,
                    RowVersion = RowVersion
                };
                var updatedPrivacyLevel = await dal.Update(documentTypeToUpdate);
                RowVersion = updatedPrivacyLevel.RowVersion;
            }
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
            var dal = dalManager.GetProvider<IPrivacyLevelDal>();
 
            await dal.Delete(id);
        }

        #endregion
    }
}