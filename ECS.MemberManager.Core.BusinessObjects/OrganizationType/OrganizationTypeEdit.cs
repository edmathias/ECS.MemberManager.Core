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
    ----
    [Serializable]
    public class OrganizationTypeEdit : BusinessBase<OrganizationTypeEdit>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);

        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<CategoryOfOrganizationER> CategoryOfOrganizationProperty = RegisterProperty<CategoryOfOrganizationER>(p => p.CategoryOfOrganization);
        public CategoryOfOrganizationER CategoryOfOrganization
        {
            get => GetProperty(CategoryOfOrganizationProperty);
            set => SetProperty(CategoryOfOrganizationProperty, value);
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name);
        [Required,MaxLength(50)]
        public string Name
        {
            get => GetProperty(NameProperty);
            set => SetProperty(NameProperty, value);
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(p => p.Notes);
        [MaxLength(255)]
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

        public static async Task<OrganizationTypeEdit> NewOrganizationTypeEdit()
        {
            return await DataPortal.CreateAsync<OrganizationTypeEdit>();
        }

        public static async Task<OrganizationTypeEdit> GetOrganizationTypeEdit(int id)
        {
            return await DataPortal.FetchAsync<OrganizationTypeEdit>(id);
        }

        public static async Task<OrganizationTypeEdit> GetOrganizationTypeEdit(OrganizationType childData)
        {
            return await DataPortal.FetchChildAsync<OrganizationTypeEdit>(childData);
        }
        
        public static async Task DeleteOrganizationTypeEdit(int id)
        {
            await DataPortal.DeleteAsync<OrganizationTypeEdit>(id);
        }

        #endregion

        #region DataPortal Methods

        [Fetch]
        private async Task FetchAsync(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOrganizationTypeDal>();
            var childData = await dal.Fetch(id);

            await Fetch(childData);
        }
        
        [FetchChild]
        private async Task Fetch(OrganizationType childData)
        {
            using (BypassPropertyChecks)
            {
                Id = childData.Id;
                Name = childData.Name;
                CategoryOfOrganization = await
                    CategoryOfOrganizationER.GetCategoryOfOrganizationER(childData.CategoryOfOrganizationId);
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
            var dal = dalManager.GetProvider<IOrganizationTypeDal>();

            var data = new OrganizationType()
            {
                CategoryOfOrganizationId = CategoryOfOrganization.Id,
                Name = Name,
                Notes = Notes
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
            var dal = dalManager.GetProvider<IOrganizationTypeDal>();

            var organizationTypeToUpdate = new OrganizationType()
            {
                Id = Id,
                Name = Name,
                CategoryOfOrganizationId = CategoryOfOrganization.Id,
                Notes = this.Notes,
                RowVersion = RowVersion
            };

            var updatedMembership = await dal.Update(organizationTypeToUpdate);
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
            var dal = dalManager.GetProvider<IOrganizationTypeDal>();

            await dal.Delete(id);
        }

        #endregion
    }
}