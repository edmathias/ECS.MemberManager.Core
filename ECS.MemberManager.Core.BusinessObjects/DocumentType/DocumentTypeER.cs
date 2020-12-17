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
    public class DocumentTypeER : BusinessBase<DocumentTypeER>
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

        public static async Task<DocumentTypeER> NewDocumentType()
        {
            return await DataPortal.CreateAsync<DocumentTypeER>();
        }

        public static async Task<DocumentTypeER> GetDocumentType(int id)
        {
            return await DataPortal.FetchAsync<DocumentTypeER>(id);
        }

        public static async Task DeleteDocumentType(int id)
        {
            await DataPortal.DeleteAsync<DocumentTypeER>(id);
        }
        
        #endregion
        
        #region DataPortal Methods
                
        [Fetch]
        private void Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IDocumentTypeDal>();
            var data = dal.Fetch(id);
            using (BypassPropertyChecks)
            {
                Id = data.Id;
                Description = data.Description;
                LastUpdatedBy = data.LastUpdatedBy;
                LastUpdatedDate = data.LastUpdatedDate;
                Notes = data.Notes;
            }
        }

        [Insert]
        private void Insert()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IDocumentTypeDal>();
            using (BypassPropertyChecks)
            {
                var documentTypeToInsert = new DocumentType
                {
                    Description = this.Description,
                    LastUpdatedDate = this.LastUpdatedDate,
                    LastUpdatedBy = this.LastUpdatedBy,
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
            var dal = dalManager.GetProvider<IDocumentTypeDal>();
            using (BypassPropertyChecks)
            {
                var documentTypeToUpdate = new DocumentType()
                {
                    Id = this.Id,
                    Description = this.Description,
                    LastUpdatedDate = this.LastUpdatedDate,
                    LastUpdatedBy = this.LastUpdatedBy,
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
            var dal = dalManager.GetProvider<IDocumentTypeDal>();
 
            dal.Delete(id);
        }

        #endregion
    }
}