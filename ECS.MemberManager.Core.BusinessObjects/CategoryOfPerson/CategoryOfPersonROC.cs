

using System;
using System.Collections.Generic; 
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class CategoryOfPersonROC : ReadOnlyBase<CategoryOfPersonROC>
    {
        #region Business Methods 
         public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public virtual int Id 
        {
            get => GetProperty(IdProperty); //1-2
            private set => LoadProperty(IdProperty, value); //2-3   
        }

        public static readonly PropertyInfo<string> CategoryProperty = RegisterProperty<string>(o => o.Category);
        public virtual string Category 
        {
            get => GetProperty(CategoryProperty); //1-2
            private set => LoadProperty(CategoryProperty, value); //2-3   
        }

        public static readonly PropertyInfo<int> DisplayOrderProperty = RegisterProperty<int>(o => o.DisplayOrder);
        public virtual int DisplayOrder 
        {
            get => GetProperty(DisplayOrderProperty); //1-2
            private set => LoadProperty(DisplayOrderProperty, value); //2-3   
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); //1-2
            private set => LoadProperty(RowVersionProperty, value); //2-3   
        }

        #endregion 

        #region Factory Methods
        internal static async Task<CategoryOfPersonROC> GetCategoryOfPersonROC(CategoryOfPerson childData)
        {
            return await DataPortal.FetchChildAsync<CategoryOfPersonROC>(childData);
        }  


        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(CategoryOfPerson data)
        {
                Id = data.Id;
                Category = data.Category;
                DisplayOrder = data.DisplayOrder;
                RowVersion = data.RowVersion;
        }

        #endregion
    }
}
