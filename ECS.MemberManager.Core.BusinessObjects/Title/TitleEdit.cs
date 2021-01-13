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
    public class TitleEdit : BusinessBase<TitleEdit>
    {
        #region Business Methods
        
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<string> AbbreviationProperty = RegisterProperty<string>(p => p.Abbreviation);
        [Required, MaxLength(10)]
        public string Abbreviation
        {
            get => GetProperty(AbbreviationProperty);
            set => SetProperty(AbbreviationProperty, value);
        }

        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(p => p.Description);
        [MaxLength(50)]
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

        public static async Task<TitleEdit> NewTitleEdit()
        {
            return await DataPortal.CreateAsync<TitleEdit>();
        }

        public static async Task<TitleEdit> GetTitleEdit(Title childData)
        {
            return await DataPortal.FetchChildAsync<TitleEdit>(childData);
        }

        public static async Task<TitleEdit> GetTitleEdit(int id)
        {
            return await DataPortal.FetchAsync<TitleEdit>(id);
        }

        public static async Task DeleteTitleEdit(int id)
        {
            await DataPortal.DeleteAsync<TitleEdit>(id);
        }

        #endregion

        #region Data Access Methods
        
        [FetchChild]
        private void Fetch(Title childData)
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

        [Fetch]
        private async Task Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITitleDal>();
            var data = await dal.Fetch(id);

            Fetch(data);
        }

        [Insert]
        private async Task Insert()
        {
            await InsertChild();
        }

        [InsertChild]
        private async Task InsertChild()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITitleDal>();
            var data = new Title()
            {
                Abbreviation = Abbreviation,
                Description = Description,
                DisplayOrder = DisplayOrder
            };

            var insertedTitle = await dal.Insert(data);
            Id = insertedTitle.Id;
            RowVersion = insertedTitle.RowVersion;
        }

        [Update]
        private async Task Update()
        {
            await ChildUpdate();
        }

        [UpdateChild]
        private async Task ChildUpdate()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITitleDal>();

            var emailTypeToUpdate = new Title()
            {
                Id = Id,
                Abbreviation = Abbreviation,
                Description = Description,
                DisplayOrder = DisplayOrder,
                RowVersion = RowVersion
            };

            var updatedEmail = await dal.Update(emailTypeToUpdate);
            RowVersion = updatedEmail.RowVersion;
        }
        
        [DeleteSelfChild]
        private async Task DeleteSelf()
        {
            await Delete(Id);
        }

        [Delete]
        private async Task Delete(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITitleDal>();
           
            await dal.Delete(id);
        }

        #endregion
    }
}