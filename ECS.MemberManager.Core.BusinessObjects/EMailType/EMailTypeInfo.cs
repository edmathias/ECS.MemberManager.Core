﻿using System;
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
    public class EMailTypeInfo : ReadOnlyBase<EMailTypeInfo>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);

        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(p => p.Description);

        [Required, MaxLength(50)]
        public string Description
        {
            get => GetProperty(DescriptionProperty);
            private set => LoadProperty(DescriptionProperty, value);
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(p => p.Notes);

        public string Notes
        {
            get => GetProperty(NotesProperty);
            private set => LoadProperty(NotesProperty, value);
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(p => p.RowVersion);

        public byte[] RowVersion
        {
            get => GetProperty(RowVersionProperty);
            private set => LoadProperty(RowVersionProperty, value);
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

        public static async Task<EMailTypeInfo> GetEMailTypeInfo(EMailType childData)
        {
            return await DataPortal.FetchChildAsync<EMailTypeInfo>(childData);
        }

        public static async Task<EMailTypeInfo> GetEMailTypeInfo(int id)
        {
            return await DataPortal.FetchAsync<EMailTypeInfo>(id);
        }

        #endregion

        #region Data Access Methods

        [Fetch]
        private void Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailTypeDal>();
            
            var data = dal.Fetch(id);
            
            Fetch(data);
        }

        [FetchChild]
        private void Fetch(EMailType childData)
        {
            Id = childData.Id;
            Description = childData.Description;
            Notes = childData.Notes;
            RowVersion = childData.RowVersion;
        }

        #endregion
    }
}