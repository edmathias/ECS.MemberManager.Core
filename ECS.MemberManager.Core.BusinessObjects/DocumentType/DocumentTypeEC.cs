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
    public class DocumentTypeEC : BusinessBase<DocumentTypeEC>
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

        public static async Task<DocumentTypeEC> NewDocumentType()
        {
            return await DataPortal.CreateChildAsync<DocumentTypeEC>();
        }

        public static async Task<DocumentTypeEC> GetDocumentType(DocumentType childData)
        {
            return await DataPortal.FetchChildAsync<DocumentTypeEC>(childData);
        }

        #endregion
        
        #region DataPortal Methods
        
        [CreateChild]
        private void Create()
        {
            MarkAsChild();
            
            BusinessRules.CheckRules();
        }         
                
        [FetchChild]
        private void Fetch(DocumentType childData)
        {
            using (BypassPropertyChecks)
            {
                Id = childData.Id;
                Description = childData.Description;
                Notes = childData.Notes;
                LastUpdatedBy = childData.LastUpdatedBy;
                LastUpdatedDate = childData.LastUpdatedDate;
            }

            BusinessRules.CheckRules();
        }

        [InsertChild]
        private void Insert()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IDocumentTypeDal>();
            var documentTypeToInsert = new DocumentType
            {
                Description = Description,
                Notes = Notes,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate
            };
            dal.Insert(documentTypeToInsert);
            Id = documentTypeToInsert.Id;
        }
        
        [UpdateChild]
        private void Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IDocumentTypeDal>();
            
            var documentTypeToUpdate = dal.Fetch(Id);
            documentTypeToUpdate.Description = Description;
            documentTypeToUpdate.LastUpdatedDate = LastUpdatedDate;
            documentTypeToUpdate.LastUpdatedBy = LastUpdatedBy;
            documentTypeToUpdate.Notes = Notes;
            
            dal.Update(documentTypeToUpdate);
        }

        [DeleteSelfChild]
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