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
    public class PersonROR : BusinessBase<PersonROR>
    {
        #region Business Methods
        
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<TitleROC> TitleProperty = RegisterProperty<TitleROC>(p => p.Title);
        public TitleROC Title
        {
            get => GetProperty(TitleProperty);
            private set => LoadProperty(TitleProperty, value);
        }

        public static readonly PropertyInfo<EMailROC> EMailProperty = RegisterProperty<EMailROC>(p => p.EMail);
        public EMailROC EMail
        {
            get => GetProperty(EMailProperty);
            private set => LoadProperty(EMailProperty, value);
        }

        public static readonly PropertyInfo<string> LastNameProperty = RegisterProperty<string>(p => p.LastName);
        [Required,MaxLength(50)]
        public string LastName
        {
            get => GetProperty(LastNameProperty);
            private set => LoadProperty(LastNameProperty, value);
        }

        public static readonly PropertyInfo<string> MiddleNameProperty = RegisterProperty<string>(p => p.MiddleName);
        [MaxLength(50)]
        public string MiddleName
        {
            get => GetProperty(MiddleNameProperty);
            private set => LoadProperty(MiddleNameProperty, value);
        }

        public static readonly PropertyInfo<string> FirstNameProperty = RegisterProperty<string>(p => p.FirstName);
        [MaxLength(50)]
        public string FirstName
        {
            get => GetProperty(FirstNameProperty);
            private set => LoadProperty(FirstNameProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> DateOfFirstContactProperty = RegisterProperty<SmartDate>(p => p.DateOfFirstContact);
        public SmartDate DateOfFirstContact
        {
            get => GetProperty(DateOfFirstContactProperty);
            private set => LoadProperty(DateOfFirstContactProperty, value);
        }
        
        public static readonly PropertyInfo<SmartDate> BirthDateProperty = RegisterProperty<SmartDate>(p => p.BirthDate);
        public SmartDate BirthDate
        {
            get => GetProperty(BirthDateProperty);
            private set => LoadProperty(BirthDateProperty, value);
        }
        
        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(p => p.LastUpdatedBy);
        [Required,MaxLength(255)]
        public string LastUpdatedBy
        {
            get => GetProperty(LastUpdatedByProperty);
            private set => LoadProperty(LastUpdatedByProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty = RegisterProperty<SmartDate>(p => p.LastUpdatedDate);
        [Required]
        public SmartDate LastUpdatedDate
        {
            get => GetProperty(LastUpdatedDateProperty);
            private set => LoadProperty(LastUpdatedDateProperty, value);
        }

        public static readonly PropertyInfo<string> CodeProperty = RegisterProperty<string>(p => p.Code);
        [MaxLength(5)]
        public string Code
        {
            get => GetProperty(CodeProperty);
            private set => LoadProperty(CodeProperty, value);
        }
        
        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(p => p.Notes);
        public string Notes
        {
            get => GetProperty(NotesProperty);
            private set => LoadProperty(NotesProperty, value);
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

        public static async Task<PersonROR> GetPersonROR(int id)
        {
            return await DataPortal.FetchAsync<PersonROR>(id);
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
                Title = data.Title != null ? await TitleROC.GetTitleROC(data.Title) : null;
                EMail = data.EMail != null ? await EMailROC.GetEMailROC(data.EMail) : null;
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
         #endregion
    }
}