

using System;
using System.Collections.Generic; 
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class OrganizationTypeEC : BusinessBase<OrganizationTypeEC>
    {
        #region Business Methods
 
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public virtual int Id 
        {
            get => GetProperty(IdProperty); //1-2
            private set => LoadProperty(IdProperty, value); //2-3   
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(o => o.Name);
        public virtual string Name 
        {
            get => GetProperty(NameProperty); //1-2
            set => SetProperty(NameProperty, value); //2-4
   
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(o => o.Notes);
        public virtual string Notes 
        {
            get => GetProperty(NotesProperty); //1-2
            set => SetProperty(NotesProperty, value); //2-4
   
        }


        public static readonly PropertyInfo<CategoryOfOrganizationEC> CategoryOfOrganizationProperty = RegisterProperty<CategoryOfOrganizationEC>(o => o.CategoryOfOrganization);
        public CategoryOfOrganizationEC CategoryOfOrganization  
        {
            get => GetProperty(CategoryOfOrganizationProperty); //1-1
            set => SetProperty(CategoryOfOrganizationProperty, value); //2-2
        }    
 
        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); //1-2
            set => SetProperty(RowVersionProperty, value); //2-4
   
        }

        #endregion 

        #region Factory Methods
        internal static async Task<OrganizationTypeEC> NewOrganizationTypeEC()
        {
            return await DataPortal.CreateChildAsync<OrganizationTypeEC>();
        }

        internal static async Task<OrganizationTypeEC> GetOrganizationTypeEC(OrganizationType childData)
        {
            return await DataPortal.FetchChildAsync<OrganizationTypeEC>(childData);
        }  


        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(OrganizationType data)
        {
            using(BypassPropertyChecks)
            {
                Id = data.Id;
                Name = data.Name;
                Notes = data.Notes;
                CategoryOfOrganization = (data.CategoryOfOrganization != null ? await CategoryOfOrganizationEC.GetCategoryOfOrganizationEC(data.CategoryOfOrganization) : null);
                RowVersion = data.RowVersion;
            }            
        }
        [InsertChild]
        private async Task Insert()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOrganizationTypeDal>();
            var data = new OrganizationType()
            {

                Id = Id,
                Name = Name,
                Notes = Notes,
                CategoryOfOrganization = (CategoryOfOrganization != null ? new CategoryOfOrganization() { Id = CategoryOfOrganization.Id } : null),
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Insert(data);
            Id = insertedObj.Id;
            RowVersion = insertedObj.RowVersion;
        }

       [UpdateChild]
        private async Task Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOrganizationTypeDal>();
            var data = new OrganizationType()
            {

                Id = Id,
                Name = Name,
                Notes = Notes,
                CategoryOfOrganization = (CategoryOfOrganization != null ? new CategoryOfOrganization() { Id = CategoryOfOrganization.Id } : null),
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Update(data);
            Id = insertedObj.Id;
            RowVersion = insertedObj.RowVersion;
        }

       
        [DeleteSelfChild]
        private async Task DeleteSelf()
        {
            await Delete(Id);
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
