using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class TitleEC : BusinessBase<TitleEC>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);

        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        [Required, MaxLength(10)]
        public static readonly PropertyInfo<string>
            AbbreviationProperty = RegisterProperty<string>(p => p.Abbreviation);

        public string Abbreviation
        {
            get => GetProperty(AbbreviationProperty);
            set => SetProperty(AbbreviationProperty, value);
        }

        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(p => p.Description);

        [Required, MaxLength(50)]
        public string Description
        {
            get => GetProperty(DescriptionProperty);
            set => SetProperty(DescriptionProperty, value);
        }

        public static readonly PropertyInfo<int> DisplayOrderProperty = RegisterProperty<int>(p => p.DisplayOrder);

        public int DisplayOrder
        {
            get => GetProperty(DisplayOrderProperty);
            set => SetProperty(DisplayOrderProperty, value);
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(p => p.RowVersion);

        public byte[] RowVersion
        {
            get => GetProperty(RowVersionProperty);
            private set => LoadProperty(RowVersionProperty, value);
        }

        #endregion

        #region Factory Methods

        internal static async Task<TitleEC> NewTitleEC()
        {
            return await DataPortal.CreateChildAsync<TitleEC>();
        }

        internal static async Task<TitleEC> GetTitleEC(Title data)
        {
            return await DataPortal.FetchChildAsync<TitleEC>(data);
        }

        #endregion

        #region Data Access Methods

        [FetchChild]
        private void FetchChild(Title childData)
        {
            using (BypassPropertyChecks)
            {
                Id = childData.Id;
                Description = childData.Description;
                Abbreviation = childData.Abbreviation;
                DisplayOrder = childData.DisplayOrder;
                RowVersion = childData.RowVersion;
            }
        }

        [InsertChild]
        private async Task InsertChild()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITitleDal>();
            var data = new Title()
            {
                Description = Description,
                Abbreviation = Abbreviation,
                DisplayOrder = DisplayOrder
            };

            var insertedTitle = await dal.Insert(data);
            Id = insertedTitle.Id;
            RowVersion = insertedTitle.RowVersion;
        }

        [UpdateChild]
        private async Task UpdateChild()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITitleDal>();

            var emailTypeToUpdate = new Title()
            {
                Id = Id,
                Description = Description,
                Abbreviation = Abbreviation,
                DisplayOrder = DisplayOrder,
                RowVersion = RowVersion
            };

            var updatedEmail = await dal.Update(emailTypeToUpdate);
            RowVersion = updatedEmail.RowVersion;
        }

        [DeleteSelfChild]
        private async Task DeleteSelfChild()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITitleDal>();

            await dal.Delete(Id);
        }

        #endregion
    }
}