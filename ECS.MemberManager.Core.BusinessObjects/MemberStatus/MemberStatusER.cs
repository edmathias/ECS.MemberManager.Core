using System;
using System.ComponentModel.DataAnnotations;
using Csla;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class MemberStatusER : BusinessBase<MemberStatusER>
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

        public static MemberStatusER NewMemberStatusER()
        {
            return DataPortal.Create<MemberStatusER>();
        }

        public static MemberStatusER GetMemberStatusER(int id)
        {
            return DataPortal.Fetch<MemberStatusER>(id);
        }

        public static void DeleteMemberStatusER(int id)
        {
            DataPortal.Delete<MemberStatusER>(id);
        }
        
        #endregion
    }
}