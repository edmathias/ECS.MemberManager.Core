﻿


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 04/07/2021 09:32:23
//******************************************************************************    

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
    public partial class OrganizationROR : BusinessBase<OrganizationROR>
    {
        #region Business Methods 
         public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public virtual int Id 
        {
            get => GetProperty(IdProperty); 
            private set => LoadProperty(IdProperty, value);    
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(o => o.Name);
        public virtual string Name 
        {
            get => GetProperty(NameProperty); 
            private set => LoadProperty(NameProperty, value);    
        }

        public static readonly PropertyInfo<SmartDate> DateOfFirstContactProperty = RegisterProperty<SmartDate>(o => o.DateOfFirstContact);
        public virtual SmartDate DateOfFirstContact 
        {
            get => GetProperty(DateOfFirstContactProperty); 
            private set => LoadProperty(DateOfFirstContactProperty, value);    
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(o => o.LastUpdatedBy);
        public virtual string LastUpdatedBy 
        {
            get => GetProperty(LastUpdatedByProperty); 
            private set => LoadProperty(LastUpdatedByProperty, value);    
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty = RegisterProperty<SmartDate>(o => o.LastUpdatedDate);
        public virtual SmartDate LastUpdatedDate 
        {
            get => GetProperty(LastUpdatedDateProperty); 
            private set => LoadProperty(LastUpdatedDateProperty, value);    
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(o => o.Notes);
        public virtual string Notes 
        {
            get => GetProperty(NotesProperty); 
            private set => LoadProperty(NotesProperty, value);    
        }


        public static readonly PropertyInfo<OrganizationTypeROC> OrganizationTypeProperty = RegisterProperty<OrganizationTypeROC>(o => o.OrganizationType);
        public OrganizationTypeROC OrganizationType  
        {
            get => GetProperty(OrganizationTypeProperty); 
        
            private set => LoadProperty(OrganizationTypeProperty, value); 
        }    
 

        public static readonly PropertyInfo<CategoryOfOrganizationROC> CategoryOfOrganizationProperty = RegisterProperty<CategoryOfOrganizationROC>(o => o.CategoryOfOrganization);
        public CategoryOfOrganizationROC CategoryOfOrganization  
        {
            get => GetProperty(CategoryOfOrganizationProperty); 
        
            private set => LoadProperty(CategoryOfOrganizationProperty, value); 
        }    
 
        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); 
            private set => LoadProperty(RowVersionProperty, value);    
        }

        #endregion 

        #region Factory Methods
        public static async Task<OrganizationROR> GetOrganizationROR(int id)
        {
            return await DataPortal.FetchAsync<OrganizationROR>(id);
        }  


        #endregion

        #region Data Access Methods

        [Fetch]
        private async Task Fetch(int id, [Inject] IDal<Organization> dal)
        {
            var data = await dal.Fetch(id);

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
