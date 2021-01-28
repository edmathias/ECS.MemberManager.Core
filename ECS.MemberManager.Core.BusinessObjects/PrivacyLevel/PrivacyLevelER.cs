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
    public class PrivacyLevelER : BusinessBase<PrivacyLevelER>
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

        public static async Task<PrivacyLevelER> NewPrivacyLevelER()
        {
            return await DataPortal.CreateAsync<PrivacyLevelER>();
        }

        public static async Task<PrivacyLevelER> GetPrivacyLevelER(int id)
        {
            return await DataPortal.FetchAsync<PrivacyLevelER>(id);
        }

        public static async Task DeletePrivacyLevelER(int id)
        {
            await DataPortal.DeleteAsync<PrivacyLevelER>(id);
        }

        #endregion

        #region Data Access Methods
 
        [Fetch]
        private async Task Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPrivacyLevelDal>();
            var data = await dal.Fetch(id);

            using (BypassPropertyChecks)
            {
                Id = data.Id;
                Description = data.Description;
                Notes = data.Notes;
                RowVersion = data.RowVersion;
            }
        }

        [Insert]
        private async Task Insert()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPrivacyLevelDal>();
            var data = new PrivacyLevel()
            {
                Description = Description,
                Notes = Notes
            };

            var insertedPrivacyLevel = await dal.Insert(data);
            Id = insertedPrivacyLevel.Id;
            RowVersion = insertedPrivacyLevel.RowVersion;
        }

        [Update]
        private async Task Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPrivacyLevelDal>();

            var paymentSourceToUpdate = new PrivacyLevel()
            {
                Id = Id,
                Description = Description,
                Notes = Notes,
                RowVersion = RowVersion,
            };

            var updatedStatus = await dal.Update(paymentSourceToUpdate);
            RowVersion = updatedStatus.RowVersion;
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
            var dal = dalManager.GetProvider<IPrivacyLevelDal>();
           
            await dal.Delete(id);
        }

        #endregion
    }
}