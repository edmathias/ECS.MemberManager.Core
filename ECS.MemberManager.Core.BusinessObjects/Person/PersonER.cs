using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class PersonER : BusinessBase<PersonER>
    {
        #region Business Methods
        
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<TitleEC> TitleProperty = RegisterProperty<TitleEC>(p => p.Title);
        public TitleEC Title
        {
            get => GetProperty(TitleProperty);
            set => SetProperty(TitleProperty, value);
        }

        public static readonly PropertyInfo<EMailEC> EMailProperty = RegisterProperty<EMailEC>(p => p.EMail);
        public EMailEC EMail
        {
            get => GetProperty(EMailProperty);
            set => SetProperty(EMailProperty, value);
        }

        public static readonly PropertyInfo<string> LastNameProperty = RegisterProperty<string>(p => p.LastName);
        [Required,MaxLength(50)]
        public string LastName
        {
            get => GetProperty(LastNameProperty);
            set => SetProperty(LastNameProperty, value);
        }

        public static readonly PropertyInfo<string> MiddleNameProperty = RegisterProperty<string>(p => p.MiddleName);
        [MaxLength(50)]
        public string MiddleName
        {
            get => GetProperty(MiddleNameProperty);
            set => SetProperty(MiddleNameProperty, value);
        }

        public static readonly PropertyInfo<string> FirstNameProperty = RegisterProperty<string>(p => p.FirstName);
        [MaxLength(50)]
        public string FirstName
        {
            get => GetProperty(FirstNameProperty);
            set => SetProperty(FirstNameProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> DateOfFirstContactProperty = RegisterProperty<SmartDate>(p => p.DateOfFirstContact);
        public SmartDate DateOfFirstContact
        {
            get => GetProperty(DateOfFirstContactProperty);
            set => SetProperty(DateOfFirstContactProperty, value);
        }
        
        public static readonly PropertyInfo<SmartDate> BirthDateProperty = RegisterProperty<SmartDate>(p => p.BirthDate);
        public SmartDate BirthDate
        {
            get => GetProperty(BirthDateProperty);
            set => SetProperty(BirthDateProperty, value);
        }
        
        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(p => p.LastUpdatedBy);
        [Required,MaxLength(255)]
        public string LastUpdatedBy
        {
            get => GetProperty(LastUpdatedByProperty);
            set => SetProperty(LastUpdatedByProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty = RegisterProperty<SmartDate>(p => p.LastUpdatedDate);
        [Required]
        public SmartDate LastUpdatedDate
        {
            get => GetProperty(LastUpdatedDateProperty);
            set => SetProperty(LastUpdatedDateProperty, value);
        }

        public static readonly PropertyInfo<string> CodeProperty = RegisterProperty<string>(p => p.Code);
        [MaxLength(5)]
        public string Code
        {
            get => GetProperty(CodeProperty);
            set => SetProperty(CodeProperty, value);
        }
        
        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(p => p.Notes);
        public string Notes
        {
            get => GetProperty(NotesProperty);
            set => SetProperty(NotesProperty, value);
        }
       
        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(p => p.RowVersion);
        public byte[] RowVersion
        {
            get => GetProperty(RowVersionProperty);
            private set => LoadProperty(RowVersionProperty, value);
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
                Title = await TitleEC.GetTitleEC(data.Title);
                EMail = await EMailEC.GetEMailEC(data.EMail);
                LastName = data.LastName;
                MiddleName = data.MiddleName;
                FirstName = data.FirstName;
                DateOfFirstContact = data.DateOfFirstContact;
                BirthDate = data.BirthDate;                
                LastUpdatedBy = data.LastUpdatedBy;
                LastUpdatedDate = data.LastUpdatedDate;
                Code = data.Code;
                Notes = data.Notes;
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
                Title = new Title() { Id = Title.Id },
                EMail = new EMail() { Id = EMail.Id },
                LastName = LastName,
                MiddleName = MiddleName,
                FirstName = FirstName,
                DateOfFirstContact = DateOfFirstContact,
                BirthDate = BirthDate,                
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Code = Code,
                Notes = Notes
            };

            var insertedPerson = await dal.Insert(data);
            Id = insertedPerson.Id;
            RowVersion = insertedPerson.RowVersion;
        }

        [Update]
        private async Task Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPersonDal>();

            var emailTypeToUpdate = new Person()
            {
                Id = Id,
                Title = new Title() { Id = Title.Id },
                EMail = new EMail() { Id = EMail.Id },
                LastName = LastName,
                MiddleName = MiddleName,
                FirstName = FirstName,
                DateOfFirstContact = DateOfFirstContact,
                BirthDate = BirthDate,                
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Code = Code,
                Notes = Notes,
                RowVersion = RowVersion
            };

            var updatedEmail = await dal.Update(emailTypeToUpdate);
            RowVersion = updatedEmail.RowVersion;
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