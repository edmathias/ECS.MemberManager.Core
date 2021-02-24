


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
    public partial class OrganizationROC : BusinessBase<OrganizationROC>
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

        public static readonly PropertyInfo<SmartDate> DateOfFirstContactProperty = RegisterProperty<SmartDate>(o => o.DateOfFirstContact);
        public virtual SmartDate DateOfFirstContact 
        {
            get => GetProperty(DateOfFirstContactProperty); //1-2
            private set => LoadProperty(DateOfFirstContactProperty, value); //2-3   
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(o => o.LastUpdatedBy);
        public virtual string LastUpdatedBy 
        {
            get => GetProperty(LastUpdatedByProperty); //1-2
            private set => LoadProperty(LastUpdatedByProperty, value); //2-3   
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty = RegisterProperty<SmartDate>(o => o.LastUpdatedDate);
        public virtual SmartDate LastUpdatedDate 
        {
            get => GetProperty(LastUpdatedDateProperty); //1-2
            private set => LoadProperty(LastUpdatedDateProperty, value); //2-3   
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(o => o.Notes);
        public virtual string Notes 
        {
            get => GetProperty(NotesProperty); //1-2
            private set => LoadProperty(NotesProperty, value); //2-3   
        }


        public static readonly PropertyInfo<OrganizationTypeROC> OrganizationTypeProperty = RegisterProperty<OrganizationTypeROC>(o => o.OrganizationType);
        public OrganizationTypeROC OrganizationType  
        {
            get => GetProperty(OrganizationTypeProperty); //1-1
        
            private set => LoadProperty(OrganizationTypeProperty, value); //2-1
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
        internal static async Task<OrganizationROC> GetOrganizationROC(Organization childData)
        {
            return await DataPortal.FetchChildAsync<OrganizationROC>(childData);
        }  


        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(Organization data)
        {
                Id = data.Id;
                Name = data.Name;
                DateOfFirstContact = data.DateOfFirstContact;
                LastUpdatedBy = data.LastUpdatedBy;
                LastUpdatedDate = data.LastUpdatedDate;
                Notes = data.Notes;
                OrganizationType = (data.OrganizationType != null ? await OrganizationTypeROC.GetOrganizationTypeROC(data.OrganizationType) : null);
                CategoryOfOrganization = (data.CategoryOfOrganization != null ? await CategoryOfOrganizationROC.GetCategoryOfOrganizationROC(data.CategoryOfOrganization) : null);
                RowVersion = data.RowVersion;
        }

        #endregion
    }
}
