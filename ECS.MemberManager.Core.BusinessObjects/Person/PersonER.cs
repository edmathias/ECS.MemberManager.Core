


using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class PersonER : BusinessBase<PersonER>
    {
        #region Business Methods 

         public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public virtual int Id 
        {
            get => GetProperty(IdProperty); 
            private set => LoadProperty(IdProperty, value); 
        
        }        
        public static readonly PropertyInfo<TitleEC> TitleProperty = RegisterProperty<TitleEC>(o => o.Title);
        public virtual TitleEC Title 
        {
            get => GetProperty(TitleProperty); 
            set => SetProperty(TitleProperty, value); 
        }        

        public static readonly PropertyInfo<string> LastNameProperty = RegisterProperty<string>(o => o.LastName);
        public virtual string LastName 
        {
            get => GetProperty(LastNameProperty); 
            set => SetProperty(LastNameProperty, value); 
        
        }        
        public static readonly PropertyInfo<string> MiddleNameProperty = RegisterProperty<string>(o => o.MiddleName);
        public virtual string MiddleName 
        {
            get => GetProperty(MiddleNameProperty); 
            set => SetProperty(MiddleNameProperty, value); 
        
        }        
        public static readonly PropertyInfo<string> FirstNameProperty = RegisterProperty<string>(o => o.FirstName);
        public virtual string FirstName 
        {
            get => GetProperty(FirstNameProperty); 
            set => SetProperty(FirstNameProperty, value); 
        
        }        
        public static readonly PropertyInfo<SmartDate> DateOfFirstContactProperty = RegisterProperty<SmartDate>(o => o.DateOfFirstContact);
        public virtual SmartDate DateOfFirstContact 
        {
            get => GetProperty(DateOfFirstContactProperty); 
            set => SetProperty(DateOfFirstContactProperty, value); 
        
        }        
        public static readonly PropertyInfo<SmartDate> BirthDateProperty = RegisterProperty<SmartDate>(o => o.BirthDate);
        public virtual SmartDate BirthDate 
        {
            get => GetProperty(BirthDateProperty); 
            set => SetProperty(BirthDateProperty, value); 
        
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
        public static readonly PropertyInfo<string> CodeProperty = RegisterProperty<string>(o => o.Code);
        public virtual string Code 
        {
            get => GetProperty(CodeProperty); 
            set => SetProperty(CodeProperty, value); 
        
        }        
        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(o => o.Notes);
        public virtual string Notes 
        {
            get => GetProperty(NotesProperty); 
            set => SetProperty(NotesProperty, value); 
        
        }        
        public static readonly PropertyInfo<EMailEC> EMailProperty = RegisterProperty<EMailEC>(o => o.EMail);
        public virtual EMailEC EMail 
        {
            get => GetProperty(EMailProperty); 
            set => SetProperty(EMailProperty, value); 
        }        

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); 
            set => SetProperty(RowVersionProperty, value); 
        
        }        
        #endregion 

        #region Factory Methods

        public static async Task<PersonER> NewPersonER()
        {
            return await DataPortal.CreateAsync<PersonER>();
        }

        public static async Task<PersonER> GetPersonER(int id)
        {
            return await DataPortal.FetchAsync<PersonER>(id);
        }

        public static async Task DeletePersonER(int id)
        {
            await DataPortal.DeleteAsync<PersonER>(id);
        }

        #endregion

        #region Data Access Methods

        [Fetch]
        private async Task Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPersonDal>();
            var data = await dal.Fetch(id);

            using (BypassPropertyChecks)
            {
                Id = data.Id;
                Title = (data.Title != null ? await TitleEC.GetTitleEC( new Title() { Id = data.Title.Id }) : null);
                LastName = data.LastName;
                MiddleName = data.MiddleName;
                FirstName = data.FirstName;
                DateOfFirstContact = data.DateOfFirstContact;
                BirthDate = data.BirthDate;
                LastUpdatedBy = data.LastUpdatedBy;
                LastUpdatedDate = data.LastUpdatedDate;
                Code = data.Code;
                Notes = data.Notes;
                EMail = (data.EMail != null ? await EMailEC.GetEMailEC( new EMail() { Id = data.EMail.Id }) : null);
                RowVersion = data.RowVersion;
            }
        }

        [Insert]
        private async Task Insert()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPersonDal>();
            var data = new Person()
            {
                Id = this.Id,
                Title = (this.Title != null ? new Title() { Id = this.Title.Id } : null),
                LastName = this.LastName,
                MiddleName = this.MiddleName,
                FirstName = this.FirstName,
                DateOfFirstContact = this.DateOfFirstContact,
                BirthDate = this.BirthDate,
                LastUpdatedBy = this.LastUpdatedBy,
                LastUpdatedDate = this.LastUpdatedDate,
                Code = this.Code,
                Notes = this.Notes,
                EMail = (this.EMail != null ? new EMail() { Id = this.EMail.Id } : null),
                RowVersion = this.RowVersion,
            };

            var insertedObj = await dal.Insert(data);
            Id = insertedObj.Id;
            RowVersion = insertedObj.RowVersion;
        }

        [Update]
        private async Task Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPersonDal>();

            var objToUpdate = new Person()
            {
                Id = this.Id,
                Title = (this.Title != null ? new Title() { Id = this.Title.Id } : null),
                LastName = this.LastName,
                MiddleName = this.MiddleName,
                FirstName = this.FirstName,
                DateOfFirstContact = this.DateOfFirstContact,
                BirthDate = this.BirthDate,
                LastUpdatedBy = this.LastUpdatedBy,
                LastUpdatedDate = this.LastUpdatedDate,
                Code = this.Code,
                Notes = this.Notes,
                EMail = (this.EMail != null ? new EMail() { Id = this.EMail.Id } : null),
                RowVersion = this.RowVersion,
        
            };

            var updatedObj = await dal.Update(objToUpdate);
            RowVersion = updatedObj.RowVersion;
        }

        [DeleteSelf]
        private async Task DeleteSelf()
        {
            await Delete(Id);
        }
        
        [Delete]
        private async Task Delete(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPersonDal>();
           
            await dal.Delete(id);
        }


        #endregion
    }
}
