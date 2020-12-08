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
    public class TitleER : BusinessBase<TitleER>
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

        public static async Task<TitleER> NewTitle()
        {
            return await DataPortal.CreateAsync<TitleER>();
        }

        public static async Task<TitleER> GetTitle(int id)
        {
            return await DataPortal.FetchAsync<TitleER>(id);
        }

        public static async Task DeleteTitle(int id)
        {
            await DataPortal.DeleteAsync<TitleER>(id);
        }
        
        #endregion

        #region Data Access

        [Fetch]
        private void Fetch(int id)
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITitleDal>();
            var data = dal.Fetch(id);
            using (BypassPropertyChecks)
            {
                Id = data.Id;
                Abbreviation = data.Abbreviation;
                Description = data.Description;
                DisplayOrder = data.DisplayOrder;
            }
        }

        [Insert]
        private void Insert()
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITitleDal>();
            using (BypassPropertyChecks)
            {
                var titleToInsert = new Title()
                {
                    Abbreviation = Abbreviation,
                    Description = Description,
                    DisplayOrder = DisplayOrder
                };
                
                Id = dal.Insert(titleToInsert);
            }
        }

        [Update]
        private void Update()
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITitleDal>();
            using (BypassPropertyChecks)
            {
                var titleToUpdate = dal.Fetch(Id);
                titleToUpdate.Abbreviation = Abbreviation;
                titleToUpdate.Description = Description;
                titleToUpdate.DisplayOrder = DisplayOrder;
                
                dal.Update(titleToUpdate);
            }
        }

        [DeleteSelf]
        private void DeleteSelf()
        {
            Delete(this.Id);
        }

        [Delete]
        private void Delete(int id)
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITitleDal>();

            dal.Delete(id);
        }


        #endregion

        
    }
}