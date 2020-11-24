﻿using System;
using System.ComponentModel.DataAnnotations;
using Csla;
using Csla.Rules.CommonRules;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class DocumentTypeER : BusinessBase<DocumentTypeER>
    {
        #region Business Methods
        
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            private set { LoadProperty(IdProperty, value); }
        }

        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(p => p.Description);
        [Required, MaxLength(50)]
        public string Description
        {
            get { return GetProperty(DescriptionProperty); }
            set { SetProperty(DescriptionProperty, value); }
        }
       
        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(p => p.LastUpdatedBy);
        [Required,MaxLength(255)]
        public string LastUpdatedBy
        {
            get { return GetProperty(LastUpdatedByProperty); }
            set { SetProperty(LastUpdatedByProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> LastUpdatedDateProperty = RegisterProperty<DateTime>(p => p.LastUpdatedDate);
        [Required]
        public DateTime LastUpdatedDate
        {
            get { return GetProperty(LastUpdatedDateProperty); }
            set { SetProperty(LastUpdatedDateProperty, value); }
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(p => p.Notes);
        public string Notes
        {
            get { return GetProperty(NotesProperty); }
            set { SetProperty(NotesProperty, value); }
        }
        
        #endregion
        
        #region Factory Methods

        public static DocumentTypeER NewDocumentTypeER()
        {
            return DataPortal.Create<DocumentTypeER>();
        }

        public static DocumentTypeER GetDocumentTypeER(int id)
        {
            return DataPortal.Fetch<DocumentTypeER>(id);
        }

        public static void DeleteDocumentTypeER(int id)
        {
            DataPortal.Delete<DocumentTypeER>(id);
        }
        
        #endregion
    }
}