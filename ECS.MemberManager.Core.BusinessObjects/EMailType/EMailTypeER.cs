using System;
using System.ComponentModel.DataAnnotations;
using Csla;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class EMailTypeER : BusinessBase<EMailTypeER>
    {
        #region Business Methods
        
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            private set { LoadProperty(IdProperty, value); }
        }

        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(p => p.Description);
        [Required,MaxLength(255)]
        public string Description
        {
            get { return GetProperty(DescriptionProperty); }
            set { SetProperty(DescriptionProperty, value); }
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(p => p.Notes);
        public string Notes
        {
            get { return GetProperty(NotesProperty); }
            set { SetProperty(NotesProperty, value); }
        }
        
        #endregion
        
        #region Factory Methods

        public static EMailTypeER NewEMailTypeER()
        {
            return DataPortal.Create<EMailTypeER>();
        }

        public static EMailTypeER GetEMailTypeER(int id)
        {
            return DataPortal.Fetch<EMailTypeER>(id);
        }

        public static void DeleteEMailTypeER(int id)
        {
            DataPortal.Delete<EMailTypeER>(id);
        }
        
        #endregion
    }
}