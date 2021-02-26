

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
    public partial class TermInOfficeER : BusinessBase<TermInOfficeER>
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
        public static async Task<TermInOfficeER> NewTermInOfficeER()
        {
            return await DataPortal.CreateAsync<TermInOfficeER>();
        }

        public static async Task<TermInOfficeER> GetTermInOfficeER(int id)
        {
            return await DataPortal.FetchAsync<TermInOfficeER>(id);
        }  

        public static async Task DeleteTermInOfficeER(int id)
        {
            await DataPortal.DeleteAsync<TermInOfficeER>(id);
        } 


        #endregion

        #region Data Access Methods

        [Fetch]
        private async Task Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITermInOfficeDal>();
            var data = await dal.Fetch(id);
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
        [Insert]
        private async Task Insert()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITermInOfficeDal>();
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

       [Update]
        private async Task Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITermInOfficeDal>();
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
            Id = insertedObj.Id;
            RowVersion = insertedObj.RowVersion;
        }

       
        [DeleteSelfChild]
        private async Task DeleteSelf()
        {
            await Delete(Id);
        }
       
        [Delete]
        private async Task Delete(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITermInOfficeDal>();
           
            await dal.Delete(id);
        }

        #endregion
    }
}
