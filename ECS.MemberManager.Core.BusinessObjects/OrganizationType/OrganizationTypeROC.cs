using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class OrganizationTypeROC : ReadOnlyBase<OrganizationTypeROC>
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

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(o => o.Notes);

        public virtual string Notes
        {
            get => GetProperty(NotesProperty);
            private set => LoadProperty(NotesProperty, value);
        }


        public static readonly PropertyInfo<CategoryOfOrganizationROC> CategoryOfOrganizationProperty =
            RegisterProperty<CategoryOfOrganizationROC>(o => o.CategoryOfOrganization);

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

        internal static async Task<OrganizationTypeROC> GetOrganizationTypeROC(OrganizationType childData)
        {
            return await DataPortal.FetchChildAsync<OrganizationTypeROC>(childData);
        }

        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(OrganizationType data)
        {
            Id = data.Id;
            Name = data.Name;
            Notes = data.Notes;
            CategoryOfOrganization = (data.CategoryOfOrganization != null
                ? await CategoryOfOrganizationROC.GetCategoryOfOrganizationROC(data.CategoryOfOrganization)
                : null);
            RowVersion = data.RowVersion;
        }

        #endregion
    }
}