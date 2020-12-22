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
    public class OrganizationTypeEC :  BusinessBase<OrganizationTypeEC>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name);
        [Required,MaxLength(50)]
        public string Name
        {
            get => GetProperty(NameProperty);
            set => SetProperty(NameProperty, value);
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(p => p.Notes);
        public string Notes
        {
            get => GetProperty(NotesProperty);
            set => SetProperty(NotesProperty, value);
        }

        public static readonly PropertyInfo<CategoryOfOrganizationECL> CategoryOfOrganizationsProperty = RegisterProperty<CategoryOfOrganizationECL>(p => p.CategoryOfOrganization);
        public CategoryOfOrganizationECL CategoryOfOrganizations
        {
            get => GetProperty(CategoryOfOrganizationsProperty);
            set => SetProperty(CategoryOfOrganizationsProperty, value);
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

        public static async Task<OrganizationTypeEC> NewOrganizationType()
        {
            return await DataPortal.CreateAsync<OrganizationTypeEC>();
        }

        public static async Task<OrganizationTypeEC> GetOrganizationType(Organization childData)
        {
            return await DataPortal.FetchAsync<OrganizationTypeEC>(childData);
        }

        public static async Task DeleteOrganizationType(int id)
        {
            await DataPortal.DeleteAsync<OrganizationTypeEC>(id);
        }

        #endregion

        #region Data Access

        [FetchChild]
        private void Fetch(Organization childData)
        {
            using (BypassPropertyChecks)
            {
                Id = childData.Id;
                Name = childData.Name;
                Notes = childData.Notes;
                CategoryOfOrganization =
                    CategoryOfOrganizationECL.GetCategoryOfOrganizationList(childData.CategoryOfOrganizations);
                // TODO: get categoryoforganization
            }
        }

        [Insert]
        private void Insert()
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOrganizationTypeDal>();
            
            using (BypassPropertyChecks)
            {
                var organizationTypeToInsert = new OrganizationType()
                {
                    Name = Name,
                    Notes = Notes
                };

                Id = dal.Insert(organizationTypeToInsert);
            }
        }

        [Update]
        private void Update()
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOrganizationTypeDal>();
            using (BypassPropertyChecks)
            {
                // format dto and update dal 
                var organizationTypeToUpdate = dal.Fetch(Id);
                organizationTypeToUpdate.Name = Name;
                organizationTypeToUpdate.Notes = Notes;
                
                dal.Update(organizationTypeToUpdate);
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
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOrganizationTypeDal>();

            dal.Delete(id);
        }


        #endregion


        
    }
}