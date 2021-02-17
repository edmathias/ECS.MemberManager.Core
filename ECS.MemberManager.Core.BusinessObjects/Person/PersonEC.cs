


using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class PersonEC : BusinessBase<PersonEC>
    {
        #region Business Methods 

         public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public virtual int Id 
        {
            get => GetProperty(IdProperty); 
            private set => LoadProperty(IdProperty, value); 
   
        }        
        public static readonly PropertyInfo<TitleEC> TitleProperty = RegisterProperty<TitleEC>(o => o.Title);
        public TitleEC Title 
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
        public EMailEC EMail 
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

        public static async Task<PersonEC> NewPersonEC()
        {
            return await DataPortal.CreateChildAsync<PersonEC>();
        }

        public static async Task<PersonEC> GetPersonEC(Person childData)
        {
            return await DataPortal.FetchChildAsync<PersonEC>(childData);
        }

        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(Person childData)
        {
            using (BypassPropertyChecks)
            {
                Id = childData.Id;
                Title = (childData.Title != null ? await TitleEC.GetTitleEC( new Title() { Id = childData.Title.Id }) : null);
                LastName = childData.LastName;
                MiddleName = childData.MiddleName;
                FirstName = childData.FirstName;
                DateOfFirstContact = childData.DateOfFirstContact;
                BirthDate = childData.BirthDate;
                LastUpdatedBy = childData.LastUpdatedBy;
                LastUpdatedDate = childData.LastUpdatedDate;
                Code = childData.Code;
                Notes = childData.Notes;
                EMail = (childData.EMail != null ? await EMailEC.GetEMailEC( new EMail() { Id = childData.EMail.Id }) : null);
                RowVersion = childData.RowVersion;
            }
        }

        [InsertChild]
        private async Task Insert()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPersonDal>();
            var data = new Person()
            {
                Id = Id,
                Title = (Title != null ? new Title() { Id = Title.Id } : null),
                LastName = LastName,
                MiddleName = MiddleName,
                FirstName = FirstName,
                DateOfFirstContact = DateOfFirstContact,
                BirthDate = BirthDate,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Code = Code,
                Notes = Notes,
                EMail = (EMail != null ? new EMail() { Id = EMail.Id } : null),
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Insert(data);
            Id = insertedObj.Id;
            RowVersion = insertedObj.RowVersion;
        }

        [UpdateChild]
        private async Task Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPersonDal>();

            var objToUpdate = new Person()
            {
                Id = Id,
                Title = (Title != null ? new Title() { Id = Title.Id } : null),
                LastName = LastName,
                MiddleName = MiddleName,
                FirstName = FirstName,
                DateOfFirstContact = DateOfFirstContact,
                BirthDate = BirthDate,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Code = Code,
                Notes = Notes,
                EMail = (EMail != null ? new EMail() { Id = EMail.Id } : null),
                RowVersion = RowVersion,
        
            };

            var updatedObj = await dal.Update(objToUpdate);
            RowVersion = updatedObj.RowVersion;
        }

        [DeleteSelfChild]
        private async Task DeleteSelf()
        {
            await Delete(Id);
        }
        
        private async Task Delete(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPersonDal>();
           
            await dal.Delete(id);
        }


        #endregion
    }
}
