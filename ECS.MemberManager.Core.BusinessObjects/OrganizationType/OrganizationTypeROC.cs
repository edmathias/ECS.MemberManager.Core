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
    public class OrganizationTypeROC :  ReadOnlyBase<OrganizationTypeROC>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name);
        public string Name
        {
            get => GetProperty(NameProperty);
            private set => LoadProperty(NameProperty, value);
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(p => p.Notes);
        public string Notes
        {
            get => GetProperty(NotesProperty);
            private set => LoadProperty(NotesProperty, value);
        }
        
        public static readonly PropertyInfo<CategoryOfOrganizationER> CategoryOfOrganizationProperty = RegisterProperty<CategoryOfOrganizationER>(p => p.CategoryOfOrganization);
        public CategoryOfOrganizationER CategoryOfOrganization
        {
            get => GetProperty(CategoryOfOrganizationProperty);
            private set => LoadProperty(CategoryOfOrganizationProperty, value);
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

 
        #region Data Access

        [FetchChild]
        private void Fetch(OrganizationType organizationType)
        {
            Id = organizationType.Id;
            Name = organizationType.Name;
            Notes = organizationType.Notes;
        }

        #endregion


        
    }
}