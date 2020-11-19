using System;
using System.ComponentModel;
using Csla;

namespace ECS.MemberManager.Core.BusinessObjects.Title
{
    [Serializable] 
    public partial class TitleRO : Csla.ReadOnlyBase<TitleRO>
    {
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            private set { LoadProperty(IdProperty, value); }
        }
        
        public static readonly PropertyInfo<string> AbbreviationProperty = RegisterProperty<string>(p => p.Abbreviation);
        public string Abbreviation
        {
            get { return GetProperty(AbbreviationProperty); }
            private set { LoadProperty(AbbreviationProperty, value); }
        }

        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(p => p.Description);
        public string Description
        {
            get { return GetProperty(DescriptionProperty); }
            private set { LoadProperty(DescriptionProperty, value); }
        }

        public static readonly PropertyInfo<int> DisplayOrderProperty = RegisterProperty<int>(p => p.DisplayOrder);
        public int DisplayOrder
        {
            get { return GetProperty(DisplayOrderProperty); }
            private set { LoadProperty(DisplayOrderProperty, value); }
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
        
        public static TitleRO GetTitleRO(int id)
        {
            return DataPortal.Fetch<TitleRO>(id);
            // TODO: call DAL to load values into object
        }
    }
}