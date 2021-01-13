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
    public class TitleInfo : ReadOnlyBase<TitleInfo>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);

        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<string>
            AbbreviationProperty = RegisterProperty<string>(p => p.Abbreviation);

        [Required, MaxLength(10)]
        public string Abbreviation
        {
            get => GetProperty(AbbreviationProperty);
            private set => LoadProperty(AbbreviationProperty, value);
        }

        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(p => p.Description);

        [MaxLength(50)]
        public string Description
        {
            get => GetProperty(DescriptionProperty);
            private set => LoadProperty(DescriptionProperty, value);
        }

        public static readonly PropertyInfo<int> DisplayOrderProperty = RegisterProperty<int>(p => p.DisplayOrder);

        public int DisplayOrder
        {
            get => GetProperty(DisplayOrderProperty);
            private set => LoadProperty(DisplayOrderProperty, value);
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

        public static async Task<TitleInfo> NewTitleInfo()
        {
            return await DataPortal.CreateAsync<TitleInfo>();
        }

        public static async Task<TitleInfo> GetTitleInfo(Title childData)
        {
            return await DataPortal.FetchChildAsync<TitleInfo>(childData);
        }

        public static async Task<TitleInfo> GetTitleInfo(int id)
        {
            return await DataPortal.FetchAsync<TitleInfo>(id);
        }

        public static async Task DeleteTitleInfo(int id)
        {
            await DataPortal.DeleteAsync<TitleInfo>(id);
        }

        #endregion

        #region Data Access Methods

        [FetchChild]
        private void Fetch(Title childData)
        {
            Id = childData.Id;
            Description = childData.Description;
            Abbreviation = childData.Abbreviation;
            DisplayOrder = DisplayOrder;
        }

        [Fetch]
        private async Task Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITitleDal>();
            var data = await dal.Fetch(id);

            Fetch(data);
        }

        #endregion
    }
}