﻿

//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 04/14/2021 08:41:01
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
    public partial class PersonalNoteER : BusinessBase<PersonalNoteER>
    {
        #region Business Methods
 
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public virtual int Id 
        {
            get => GetProperty(IdProperty); 
            private set => LoadProperty(IdProperty, value);    
        }


        public static readonly PropertyInfo<PersonEC> PersonProperty = RegisterProperty<PersonEC>(o => o.Person);
        public PersonEC Person  
        {
            get => GetProperty(PersonProperty); 
            set => SetProperty(PersonProperty, value); 
        }    
 
        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(o => o.Description);
        public virtual string Description 
        {
            get => GetProperty(DescriptionProperty); 
            set => SetProperty(DescriptionProperty, value); 
   
        }

        public static readonly PropertyInfo<SmartDate> StartDateProperty = RegisterProperty<SmartDate>(o => o.StartDate);
        public virtual SmartDate StartDate 
        {
            get => GetProperty(StartDateProperty); 
            set => SetProperty(StartDateProperty, value); 
   
        }

        public static readonly PropertyInfo<SmartDate> DateEndProperty = RegisterProperty<SmartDate>(o => o.DateEnd);
        public virtual SmartDate DateEnd 
        {
            get => GetProperty(DateEndProperty); 
            set => SetProperty(DateEndProperty, value); 
   
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(o => o.LastUpdatedBy);
        public virtual string LastUpdatedBy 
        {
            get => GetProperty(LastUpdatedByProperty); 
            set => SetProperty(LastUpdatedByProperty, value); 
   
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty = RegisterProperty<SmartDate>(o => o.LastUpdatedDate);
        public virtual SmartDate LastUpdatedDate 
        {
            get => GetProperty(LastUpdatedDateProperty); 
            set => SetProperty(LastUpdatedDateProperty, value); 
   
        }

        public static readonly PropertyInfo<string> NoteProperty = RegisterProperty<string>(o => o.Note);
        public virtual string Note 
        {
            get => GetProperty(NoteProperty); 
            set => SetProperty(NoteProperty, value); 
   
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); 
            set => SetProperty(RowVersionProperty, value); 
   
        }

        #endregion 

        #region Factory Methods
        public static async Task<PersonalNoteER> NewPersonalNoteER()
        {
            return await DataPortal.CreateAsync<PersonalNoteER>();
        }

        public static async Task<PersonalNoteER> GetPersonalNoteER(int id)
        {
            return await DataPortal.FetchAsync<PersonalNoteER>(id);
        }  

        public static async Task DeletePersonalNoteER(int id)
        {
            await DataPortal.DeleteAsync<PersonalNoteER>(id);
        } 


        #endregion

        #region Data Access Methods

        [Fetch]
        private async Task Fetch(int id, [Inject] IDal<PersonalNote> dal)
        {
            var data = await dal.Fetch(id);

            using(BypassPropertyChecks)
            {
            Id = data.Id;
            Person = (data.Person != null ? await PersonEC.GetPersonEC(data.Person) : null);
            Description = data.Description;
            StartDate = data.StartDate;
            DateEnd = data.DateEnd;
            LastUpdatedBy = data.LastUpdatedBy;
            LastUpdatedDate = data.LastUpdatedDate;
            Note = data.Note;
            RowVersion = data.RowVersion;
            }            
        }
        [Insert]
        private async Task Insert([Inject] IDal<PersonalNote> dal)
        {
            FieldManager.UpdateChildren();

            var data = new PersonalNote()
            {

                Id = Id,
                Person = (Person != null ? new Person() { Id = Person.Id } : null),
                Description = Description,
                StartDate = StartDate,
                DateEnd = DateEnd,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Note = Note,
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Insert(data);
            Id = insertedObj.Id;
            RowVersion = insertedObj.RowVersion;
        }

       [Update]
        private async Task Update([Inject] IDal<PersonalNote> dal)
        {
            FieldManager.UpdateChildren();

            var data = new PersonalNote()
            {

                Id = Id,
                Person = (Person != null ? new Person() { Id = Person.Id } : null),
                Description = Description,
                StartDate = StartDate,
                DateEnd = DateEnd,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Note = Note,
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Update(data);
            RowVersion = insertedObj.RowVersion;
        }

        [DeleteSelf]
        private async Task DeleteSelf([Inject] IDal<PersonalNote> dal)
        {
            await Delete(Id,dal);
        }
       
        [Delete]
        private async Task Delete(int id, [Inject] IDal<PersonalNote> dal)
        {
            await dal.Delete(id);
        }

        #endregion
    }
}
