using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class TitleROC : ReadOnlyBase<TitleROC>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }
        
        public static readonly PropertyInfo<string> AbbreviationProperty = RegisterProperty<string>(p => p.Abbreviation);
        public string Abbreviation
        {
            get => GetProperty(AbbreviationProperty);
            private set => LoadProperty(AbbreviationProperty, value);
        }

        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(p => p.Description);
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

        internal static async Task<TitleROC> GetTitleROC(Title childData)
        {
            return await DataPortal.FetchChildAsync<TitleROC>(childData);
        }

        #endregion

        #region Data Access Methods

        [FetchChild]
        private void Fetch(Title childData)
        {
            Id = childData.Id;
            Description = childData.Description;
            Abbreviation = childData.Abbreviation;
            DisplayOrder = childData.DisplayOrder;
            RowVersion = childData.RowVersion;
        }

         #endregion
    }
}