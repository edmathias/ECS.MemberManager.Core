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
    public class CategoryOfPersonROC : ReadOnlyBase<CategoryOfPersonROC>
    {
        #region Business Methods
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<string> CategoryProperty = RegisterProperty<string>(p => p.Category);
        [Required, MaxLength(35)]
        public string Category
        {
            get => GetProperty(CategoryProperty);
            private set => LoadProperty(CategoryProperty, value);
        }

        public static readonly PropertyInfo<int> DisplayOrderProperty = RegisterProperty<int>(p => p.DisplayOrder);
        public int DisplayOrder
        {
            get => GetProperty(DisplayOrderProperty);
            private set => LoadProperty(DisplayOrderProperty, value);
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
        
        [CreateChild]
        private void Create()
        {
        }

        [FetchChild]
        private void FetchChild(CategoryOfPerson categoryOfPerson)
        {
            this.Id = categoryOfPerson.Id;
            Category = categoryOfPerson.Category;
            DisplayOrder = categoryOfPerson.DisplayOrder;
        }
        
        #endregion

 
    }
}