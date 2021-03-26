﻿

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
    public partial class TermInOfficeEC : BusinessBase<TermInOfficeEC>
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
 

        public static readonly PropertyInfo<OfficeEC> OfficeProperty = RegisterProperty<OfficeEC>(o => o.Office);
        public OfficeEC Office  
        {
            get => GetProperty(OfficeProperty); 
            set => SetProperty(OfficeProperty, value); 
        }    
 
        public static readonly PropertyInfo<SmartDate> StartDateProperty = RegisterProperty<SmartDate>(o => o.StartDate);
        public virtual SmartDate StartDate 
        {
            get => GetProperty(StartDateProperty); 
            set => SetProperty(StartDateProperty, value); 
   
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

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(o => o.Notes);
        public virtual string Notes 
        {
            get => GetProperty(NotesProperty); 
            set => SetProperty(NotesProperty, value); 
   
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); 
            set => SetProperty(RowVersionProperty, value); 
   
        }

        #endregion 

        #region Factory Methods
        internal static async Task<TermInOfficeEC> NewTermInOfficeEC()
        {
            return await DataPortal.CreateChildAsync<TermInOfficeEC>();
        }

        internal static async Task<TermInOfficeEC> GetTermInOfficeEC(TermInOffice childData)
        {
            return await DataPortal.FetchChildAsync<TermInOfficeEC>(childData);
        }  


        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(TermInOffice data)
        {
            using(BypassPropertyChecks)
            {
            Id = data.Id;
            Person = (data.Person != null ? await PersonEC.GetPersonEC(data.Person) : null);
            Office = (data.Office != null ? await OfficeEC.GetOfficeEC(data.Office) : null);
            StartDate = data.StartDate;
            LastUpdatedBy = data.LastUpdatedBy;
            LastUpdatedDate = data.LastUpdatedDate;
            Notes = data.Notes;
            RowVersion = data.RowVersion;
            }            
        }
        [InsertChild]
        private async Task Insert([Inject] ITermInOfficeDal dal)
        {
            FieldManager.UpdateChildren();

            var data = new TermInOffice()
            {

                Id = Id,
                Person = (Person != null ? new Person() { Id = Person.Id } : null),
                Office = (Office != null ? new Office() { Id = Office.Id } : null),
                StartDate = StartDate,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Insert(data);
            Id = insertedObj.Id;
            RowVersion = insertedObj.RowVersion;
        }

       [UpdateChild]
        private async Task Update([Inject] ITermInOfficeDal dal)
        {
            FieldManager.UpdateChildren();

            var data = new TermInOffice()
            {

                Id = Id,
                Person = (Person != null ? new Person() { Id = Person.Id } : null),
                Office = (Office != null ? new Office() { Id = Office.Id } : null),
                StartDate = StartDate,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Update(data);
            RowVersion = insertedObj.RowVersion;
        }

       
        [DeleteSelfChild]
        private async Task DeleteSelf([Inject] ITermInOfficeDal dal)
        {
            await Delete(Id,dal);
        }
       
        [Delete]
        private async Task Delete(int id, [Inject] ITermInOfficeDal dal)
        {
            await dal.Delete(id);
        }

        #endregion
    }
}
