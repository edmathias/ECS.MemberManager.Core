


using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class OrganizationROR : ReadOnlyBase<OrganizationROR>
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
        public static readonly PropertyInfo<OrganizationTypeROC> OrganizationTypeProperty = RegisterProperty<OrganizationTypeROC>(o => o.OrganizationType);
        public OrganizationTypeROC OrganizationType 
        {
            get => GetProperty(OrganizationTypeProperty); 
            private set => LoadProperty(OrganizationTypeProperty, value); 
        }        

        public static readonly PropertyInfo<SmartDate> DateOfFirstContactProperty = RegisterProperty<SmartDate>(o => o.DateOfFirstContact);
        public SmartDate DateOfFirstContact 
        {
            get => GetProperty(DateOfFirstContactProperty); 
            private set => LoadProperty(DateOfFirstContactProperty, value); 
        
        }        
        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(o => o.LastUpdatedBy);
        public string LastUpdatedBy 
        {
            get => GetProperty(LastUpdatedByProperty); 
            private set => LoadProperty(LastUpdatedByProperty, value); 
        
        }        
        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty = RegisterProperty<SmartDate>(o => o.LastUpdatedDate);
        public SmartDate LastUpdatedDate 
        {
            get => GetProperty(LastUpdatedDateProperty); 
            private set => LoadProperty(LastUpdatedDateProperty, value); 
        
        }        
        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(o => o.Notes);
        public string Notes 
        {
            get => GetProperty(NotesProperty); 
            private set => LoadProperty(NotesProperty, value); 
        
        }        
        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public byte[] RowVersion 
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
        private async Task Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOrganizationDal>();
            var data = await dal.Fetch(id);

            Id = data.Id;
            Name = data.Name;
            if(data.OrganizationType != null )
            {
                OrganizationType = await OrganizationTypeROC.GetOrganizationTypeROC(data.OrganizationType);
            }
            DateOfFirstContact = data.DateOfFirstContact;
            LastUpdatedBy = data.LastUpdatedBy;
            LastUpdatedDate = data.LastUpdatedDate;
            Notes = data.Notes;
            RowVersion = data.RowVersion;
        }

        #endregion
    }
}
