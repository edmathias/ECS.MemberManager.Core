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
        
        #endregion
        
        #region Factory Methods

        public static async Task<PrivacyLevelER> NewPrivacyLevel()
        {
            return await DataPortal.CreateAsync<PrivacyLevelER>();
        }

        public static async Task<PrivacyLevelER> GetPrivacyLevel(int id)
        {
            return await DataPortal.FetchAsync<PrivacyLevelER>(id);
        }

        public static async Task DeletePrivacyLevel(int id)
        {
            await DataPortal.DeleteAsync<PrivacyLevelER>(id);
        }
        
        #endregion
        
        #region DataPortal Methods
                
        [Fetch]
        private void Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPrivacyLevelDal>();
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
            var dal = dalManager.GetProvider<IPrivacyLevelDal>();
            using (BypassPropertyChecks)
            {
                var documentTypeToInsert = new PrivacyLevel
                {
                    Description = this.Description,
                    Notes = this.Notes
                };
                dal.Insert(documentTypeToInsert);
                Id = documentTypeToInsert.Id;
            }
        }
        
        [Update]
        private void Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPrivacyLevelDal>();
            using (BypassPropertyChecks)
            {
                var documentTypeToUpdate = new PrivacyLevel()
                {
                    Id = this.Id,
                    Description = this.Description,
                    Notes = this.Notes
                };
                dal.Update(documentTypeToUpdate);
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
            var dal = dalManager.GetProvider<IPrivacyLevelDal>();
 
            dal.Delete(id);
        }

        #endregion
    }
}