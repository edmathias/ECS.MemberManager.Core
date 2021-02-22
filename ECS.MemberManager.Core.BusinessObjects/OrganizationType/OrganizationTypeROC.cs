


using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class OrganizationTypeROC : ReadOnlyBase<OrganizationTypeROC>
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

        public static async Task<OrganizationTypeROC> GetOrganizationTypeROC(OrganizationType childData)
        {
            return await DataPortal.FetchChildAsync<OrganizationTypeROC>(childData);
        }

        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(OrganizationType childData)
        {
            Id = childData.Id;
            Name = childData.Name;
            Notes = childData.Notes;
            if(childData.CategoryOfOrganization != null )
            {
                CategoryOfOrganization = await CategoryOfOrganizationROC.GetCategoryOfOrganizationROC(childData.CategoryOfOrganization);
            }
            RowVersion = childData.RowVersion;
        }

        #endregion
    }
}
