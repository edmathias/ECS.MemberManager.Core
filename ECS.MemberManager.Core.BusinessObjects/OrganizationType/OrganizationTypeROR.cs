


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
    public partial class OrganizationTypeROR : BusinessBase<OrganizationTypeROR>
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
            private set => LoadProperty(NameProperty, value); //2-3   
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(o => o.Notes);
        public virtual string Notes 
        {
            get => GetProperty(NotesProperty); //1-2
            private set => LoadProperty(NotesProperty, value); //2-3   
        }


        public static readonly PropertyInfo<CategoryOfOrganizationROC> CategoryOfOrganizationProperty = RegisterProperty<CategoryOfOrganizationROC>(o => o.CategoryOfOrganization);
        public CategoryOfOrganizationROC CategoryOfOrganization  
        {
            get => GetProperty(CategoryOfOrganizationProperty); //1-1
        
            private set => LoadProperty(CategoryOfOrganizationProperty, value); //2-1
        }    
 
        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); //1-2
            private set => LoadProperty(RowVersionProperty, value); //2-3   
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
                CategoryOfOrganization = (data.CategoryOfOrganization != null ? await CategoryOfOrganizationROC.GetCategoryOfOrganizationROC(data.CategoryOfOrganization) : null);
                RowVersion = data.RowVersion;
        }

        #endregion
    }
}
