﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Csla;
using Csla.Serialization.Mobile;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class OrganizationTypeInfo : ReadOnlyBase<OrganizationTypeInfo>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);

        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<CategoryOfOrganizationEdit> CategoryOfOrganizationProperty =
            RegisterProperty<CategoryOfOrganizationEdit>(p => p.CategoryOfOrganization);

        public CategoryOfOrganizationEdit CategoryOfOrganization
        {
            get => GetProperty(CategoryOfOrganizationProperty);
            set => LoadProperty(CategoryOfOrganizationProperty, value);
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name);

        [Required, MaxLength(50)]
        public string Name
        {
            get => GetProperty(NameProperty);
            set => LoadProperty(NameProperty, value);
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(p => p.Notes);

        [MaxLength(255)]
        public string Notes
        {
            get => GetProperty(NotesProperty);
            set => LoadProperty(NotesProperty, value);
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(p => p.RowVersion);

        public byte[] RowVersion
        {
            get => GetProperty(RowVersionProperty);
            private set => LoadProperty(RowVersionProperty, value);
        }

        #endregion

        #region Factory Methods

        public static async Task<OrganizationTypeInfo> GetOrganizationTypeInfo(int id)
        {
            return await DataPortal.FetchAsync<OrganizationTypeInfo>(id);
        }

        public static async Task<OrganizationTypeInfo> GetOrganizationTypeInfo(OrganizationType childData)
        {
            return await DataPortal.FetchChildAsync<OrganizationTypeInfo>(childData);
        }

        #endregion

        #region DataPortal Methods

        [Fetch]
        private async Task FetchAsync(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOrganizationTypeDal>();
            var childData = await dal.Fetch(id);

            await Fetch(childData);
        }

        [FetchChild]
        private async Task Fetch(OrganizationType childData)
        {
            Id = childData.Id;
            Name = childData.Name;
            CategoryOfOrganization = await
                CategoryOfOrganizationEdit.GetCategoryOfOrganizationEdit(childData.CategoryOfOrganizationId);
            Notes = childData.Notes;
            RowVersion = childData.RowVersion;
        }

        #endregion
    }
}