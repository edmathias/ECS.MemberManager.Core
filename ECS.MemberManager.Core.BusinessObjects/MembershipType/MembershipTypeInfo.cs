using System;
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
    public class MembershipTypeInfo : ReadOnlyBase<MembershipTypeInfo>
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

        public static readonly PropertyInfo<int> LevelProperty = RegisterProperty<int>(p => p.Level);

        public int Level
        {
            get => GetProperty(LevelProperty);
            private set => LoadProperty(LevelProperty, value);
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty =
            RegisterProperty<string>(p => p.LastUpdatedBy);

        [Required, MaxLength(255)]
        public string LastUpdatedBy
        {
            get => GetProperty(LastUpdatedByProperty);
            private set => LoadProperty(LastUpdatedByProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty =
            RegisterProperty<SmartDate>(p => p.LastUpdatedDate);

        [Required]
        public SmartDate LastUpdatedDate
        {
            get => GetProperty(LastUpdatedDateProperty);
            private set => LoadProperty(LastUpdatedDateProperty, value);
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

        #endregion

        #region Factory Methods

        public static async Task<MembershipTypeInfo> NewMembershipTypeInfo()
        {
            return await DataPortal.CreateAsync<MembershipTypeInfo>();
        }

        public static async Task<MembershipTypeInfo> GetMembershipTypeInfo(int id)
        {
            return await DataPortal.FetchAsync<MembershipTypeInfo>(id);
        }

        public static async Task<MembershipTypeInfo> GetMembershipTypeInfo(MembershipType childData)
        {
            return await DataPortal.FetchAsync<MembershipTypeInfo>(childData);
        }

        public static async Task DeleteMembershipTypeInfo(int id)
        {
            await DataPortal.DeleteAsync<MembershipTypeInfo>(id);
        }

        #endregion

        #region DataPortal Methods

        [Fetch]
        private async Task Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMembershipTypeDal>();
            var childData = await dal.Fetch(id);

            Id = childData.Id;
            Description = childData.Description;
            Level = childData.Level;
            LastUpdatedBy = childData.LastUpdatedBy;
            LastUpdatedDate = childData.LastUpdatedDate;
            Notes = childData.Notes;
            RowVersion = childData.RowVersion;
        }

        [Fetch]
        private async Task Fetch(MembershipType childData)
        {
            Id = childData.Id;
            Description = childData.Description;
            Level = childData.Level;
            LastUpdatedBy = childData.LastUpdatedBy;
            LastUpdatedDate = childData.LastUpdatedDate;
            Notes = childData.Notes;
            RowVersion = childData.RowVersion;
        }

        
        #endregion
    }
}