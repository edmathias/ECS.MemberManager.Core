using System;
using System.Collections.Generic;
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
    public class OrganizationEC : BusinessBase<OrganizationEC>
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
        
        public static readonly PropertyInfo<SmartDate> DateOfFirstContactProperty = RegisterProperty<SmartDate>(p => p.DateOfFirstContact);
        public SmartDate DateOfFirstContact
        {
            get => GetProperty(DateOfFirstContactProperty);
            set => SetProperty(DateOfFirstContactProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty = RegisterProperty<SmartDate>(p => p.LastUpdatedDate);
        public SmartDate LastUpdatedDate
        {
            get => GetProperty(LastUpdatedDateProperty);
            set => SetProperty(LastUpdatedDateProperty, value);
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(p => p.LastUpdatedBy);
        public string LastUpdatedBy
        {
            get => GetProperty(LastUpdatedByProperty);
            set => SetProperty(LastUpdatedByProperty, value);
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(p => p.Notes);
        public string Notes
        {
            get => GetProperty(NotesProperty);
            set => SetProperty(NotesProperty, value);
        }

        public static readonly PropertyInfo<OrganizationTypeEC> OrganizationTypeProperty =
            RegisterProperty<OrganizationTypeEC>(p => p.OrganizationType);
        public OrganizationTypeEC OrganizationType
        {
            get => GetProperty(OrganizationTypeProperty);
            set => SetProperty(OrganizationTypeProperty, value);
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

        internal static async Task<OrganizationEC> NewOrganization()
        {
            return await DataPortal.CreateChildAsync<OrganizationEC>();
        }

        internal static async Task<OrganizationEC> GetOrganization(Organization childData)
        {
            return await DataPortal.FetchChildAsync<OrganizationEC>(childData);
        }

        internal static async Task DeleteOrganization(int id)
        {
            await DataPortal.DeleteAsync<OrganizationEC>(id);
        }
        
        #endregion

        #region Data Access

        [FetchChild]
        private async void Fetch(Organization childData)
        {
            using (BypassPropertyChecks)
            {
                Id = childData.Id;
                this.Name = childData.Name;
                this.Notes = childData.Notes;
                this.LastUpdatedBy = childData.LastUpdatedBy;
                this.LastUpdatedDate = childData.LastUpdatedDate;
                this.DateOfFirstContact = childData.DateOfFirstContact;
                
                if(childData.OrganizationType != null)
                    OrganizationType = await DataPortal.FetchChildAsync<OrganizationTypeEC>(childData.OrganizationType);
                else
                {
                    OrganizationType = await OrganizationTypeEC.NewOrganizationType();
                }
            }
        }

        [InsertChild]
        private void Insert()
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOrganizationDal>();
            using (BypassPropertyChecks)
            {
                // format and store dto 
                var organizationToInsert = new Organization()
                {
                    OrganizationType = new OrganizationType(),
                    Name = "new organization",
                    DateOfFirstContact = DateOfFirstContact,
                    LastUpdatedBy = LastUpdatedBy,
                    LastUpdatedDate = LastUpdatedDate,
                    Notes = "no notes here",

                    // TODO: work with Emails and Addresses
                    EMails = new List<EMail>(),
                    Addresses = new List<Address>(),
                    CategoryOfOrganizations = new List<CategoryOfOrganization>(),
                    Phones = new List<Phone>()
                };

                Id = dal.Insert(organizationToInsert);
            }
        }

        [UpdateChild]
        private void Update()
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOrganizationDal>();
            var organizationToInsert = new Organization()
            {
                OrganizationType = new OrganizationType()
                {
                    Id = OrganizationType.Id,
                    Name = OrganizationType.Name,
                    Notes = OrganizationType.Notes,
                    CategoryOfOrganization = new CategoryOfOrganization()
                },
                DateOfFirstContact = DateOfFirstContact,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                // TODO: work with Emails and Addresses
                EMails = new List<EMail>(),
                Addresses = new List<Address>()
            };
            Id = dal.Insert(organizationToInsert);
        }

        [DeleteSelfChild]
        private void DeleteSelf()
        {
            Delete(this.Id);
        }

        [Delete]
        private void Delete(int id)
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOrganizationDal>();

            dal.Delete(id);
        }



        #endregion

 
    }
}