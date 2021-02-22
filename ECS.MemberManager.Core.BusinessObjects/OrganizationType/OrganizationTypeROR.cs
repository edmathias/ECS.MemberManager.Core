


using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class OrganizationTypeROR : ReadOnlyBase<OrganizationTypeROR>
    {
        #region Business Methods

         public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public int Id 
        {
            get => GetProperty(IdProperty); 
            private set => LoadProperty(IdProperty, value); 
        
        }        
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(o => o.Name);
        public string Name 
        {
            get => GetProperty(NameProperty); 
            private set => LoadProperty(NameProperty, value); 
        
        }        
        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(o => o.Notes);
        public string Notes 
        {
            get => GetProperty(NotesProperty); 
            private set => LoadProperty(NotesProperty, value); 
        
        }        
        public static readonly PropertyInfo<CategoryOfOrganizationROC> CategoryOfOrganizationProperty = RegisterProperty<CategoryOfOrganizationROC>(o => o.CategoryOfOrganization);
        public CategoryOfOrganizationROC CategoryOfOrganization 
        {
            get => GetProperty(CategoryOfOrganizationProperty); 
            private set => LoadProperty(CategoryOfOrganizationProperty, value); 
        }        

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); 
            private set => LoadProperty(RowVersionProperty, value); 
        
        }        
        #endregion 

        #region Factory Methods

        public static async Task<OrganizationTypeROR> GetOrganizationTypeROR(int id)
        {
            return await DataPortal.FetchAsync<OrganizationTypeROR>(id);
        }

        #endregion

        #region Data Access Methods

        [Fetch]
        private async Task Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOrganizationTypeDal>();
            var data = await dal.Fetch(id);

            Id = data.Id;
            Name = data.Name;
            Notes = data.Notes;
            if(data.CategoryOfOrganization != null )
            {
                CategoryOfOrganization = await CategoryOfOrganizationROC.GetCategoryOfOrganizationROC(data.CategoryOfOrganization);
            }
            RowVersion = data.RowVersion;
        }

        #endregion
    }
}
