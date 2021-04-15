

//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 04/14/2021 08:41:16
//******************************************************************************    

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
    public partial class PersonalNoteROC : ReadOnlyBase<PersonalNoteROC>
    {
        #region Business Methods
 
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public virtual int Id 
        {
            get => GetProperty(IdProperty); 
            private set => LoadProperty(IdProperty, value);    
        }


        public static readonly PropertyInfo<PersonROC> PersonProperty = RegisterProperty<PersonROC>(o => o.Person);
        public PersonROC Person  
        {
            get => GetProperty(PersonProperty); 
        
            private set => LoadProperty(PersonProperty, value); 
        }    
 
        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(o => o.Description);
        public virtual string Description 
        {
            get => GetProperty(DescriptionProperty); 
            private set => LoadProperty(DescriptionProperty, value);    
        }

        public static readonly PropertyInfo<SmartDate> StartDateProperty = RegisterProperty<SmartDate>(o => o.StartDate);
        public virtual SmartDate StartDate 
        {
            get => GetProperty(StartDateProperty); 
            private set => LoadProperty(StartDateProperty, value);    
        }

        public static readonly PropertyInfo<SmartDate> DateEndProperty = RegisterProperty<SmartDate>(o => o.DateEnd);
        public virtual SmartDate DateEnd 
        {
            get => GetProperty(DateEndProperty); 
            private set => LoadProperty(DateEndProperty, value);    
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(o => o.LastUpdatedBy);
        public virtual string LastUpdatedBy 
        {
            get => GetProperty(LastUpdatedByProperty); 
            private set => LoadProperty(LastUpdatedByProperty, value);    
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty = RegisterProperty<SmartDate>(o => o.LastUpdatedDate);
        public virtual SmartDate LastUpdatedDate 
        {
            get => GetProperty(LastUpdatedDateProperty); 
            private set => LoadProperty(LastUpdatedDateProperty, value);    
        }

        public static readonly PropertyInfo<string> NoteProperty = RegisterProperty<string>(o => o.Note);
        public virtual string Note 
        {
            get => GetProperty(NoteProperty); 
            private set => LoadProperty(NoteProperty, value);    
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); 
            private set => LoadProperty(RowVersionProperty, value);    
        }

        #endregion 

        #region Factory Methods
        internal static async Task<PersonalNoteROC> GetPersonalNoteROC(PersonalNote childData)
        {
            return await DataPortal.FetchChildAsync<PersonalNoteROC>(childData);
        }  


        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(PersonalNote data)
        {
            Id = data.Id;
            Person = (data.Person != null ? await PersonROC.GetPersonROC(data.Person) : null);
            Description = data.Description;
            StartDate = data.StartDate;
            DateEnd = data.DateEnd;
            LastUpdatedBy = data.LastUpdatedBy;
            LastUpdatedDate = data.LastUpdatedDate;
            Note = data.Note;
            RowVersion = data.RowVersion;
        }

        #endregion
    }
}
