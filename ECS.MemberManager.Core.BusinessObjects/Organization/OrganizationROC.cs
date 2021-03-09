

//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/09/2021 14:34:27
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
    public partial class OrganizationROC : ReadOnlyBase<OrganizationROC>
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
 
        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); 
            private set => LoadProperty(RowVersionProperty, value);    
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
            RowVersion = data.RowVersion;
        }

        #endregion
    }
}
