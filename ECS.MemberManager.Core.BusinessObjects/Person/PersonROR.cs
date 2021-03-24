//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/23/2021 09:57:38
//******************************************************************************    

using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class PersonROR : BusinessBase<PersonROR>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);

        public virtual int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }


        public static readonly PropertyInfo<TitleROC> TitleProperty = RegisterProperty<TitleROC>(o => o.Title);

        public TitleROC Title
        {
            get => GetProperty(TitleProperty);

            private set => LoadProperty(TitleProperty, value);
        }

        public static readonly PropertyInfo<string> LastNameProperty = RegisterProperty<string>(o => o.LastName);

        public virtual string LastName
        {
            get => GetProperty(LastNameProperty);
            private set => LoadProperty(LastNameProperty, value);
        }

        public static readonly PropertyInfo<string> MiddleNameProperty = RegisterProperty<string>(o => o.MiddleName);

        public virtual string MiddleName
        {
            get => GetProperty(MiddleNameProperty);
            private set => LoadProperty(MiddleNameProperty, value);
        }

        public static readonly PropertyInfo<string> FirstNameProperty = RegisterProperty<string>(o => o.FirstName);

        public virtual string FirstName
        {
            get => GetProperty(FirstNameProperty);
            private set => LoadProperty(FirstNameProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> DateOfFirstContactProperty =
            RegisterProperty<SmartDate>(o => o.DateOfFirstContact);

        public virtual SmartDate DateOfFirstContact
        {
            get => GetProperty(DateOfFirstContactProperty);
            private set => LoadProperty(DateOfFirstContactProperty, value);
        }

        public static readonly PropertyInfo<SmartDate>
            BirthDateProperty = RegisterProperty<SmartDate>(o => o.BirthDate);

        public virtual SmartDate BirthDate
        {
            get => GetProperty(BirthDateProperty);
            private set => LoadProperty(BirthDateProperty, value);
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty =
            RegisterProperty<string>(o => o.LastUpdatedBy);

        public virtual string LastUpdatedBy
        {
            get => GetProperty(LastUpdatedByProperty);
            private set => LoadProperty(LastUpdatedByProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty =
            RegisterProperty<SmartDate>(o => o.LastUpdatedDate);

        public virtual SmartDate LastUpdatedDate
        {
            get => GetProperty(LastUpdatedDateProperty);
            private set => LoadProperty(LastUpdatedDateProperty, value);
        }

        public static readonly PropertyInfo<string> CodeProperty = RegisterProperty<string>(o => o.Code);

        public virtual string Code
        {
            get => GetProperty(CodeProperty);
            private set => LoadProperty(CodeProperty, value);
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(o => o.Notes);

        public virtual string Notes
        {
            get => GetProperty(NotesProperty);
            private set => LoadProperty(NotesProperty, value);
        }


        public static readonly PropertyInfo<EMailROC> EMailProperty = RegisterProperty<EMailROC>(o => o.EMail);

        public EMailROC EMail
        {
            get => GetProperty(EMailProperty);

            private set => LoadProperty(EMailProperty, value);
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);

        public virtual byte[] RowVersion
        {
            get => GetProperty(RowVersionProperty);
            private set => LoadProperty(RowVersionProperty, value);
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
        private async Task Fetch(int id, [Inject] IPersonDal dal)
        {
            var data = await dal.Fetch(id);

            Id = data.Id;
            Title = (data.Title != null ? await TitleROC.GetTitleROC(data.Title) : null);
            LastName = data.LastName;
            MiddleName = data.MiddleName;
            FirstName = data.FirstName;
            DateOfFirstContact = data.DateOfFirstContact;
            BirthDate = data.BirthDate;
            LastUpdatedBy = data.LastUpdatedBy;
            LastUpdatedDate = data.LastUpdatedDate;
            Code = data.Code;
            Notes = data.Notes;
            EMail = (data.EMail != null ? await EMailROC.GetEMailROC(data.EMail) : null);
            RowVersion = data.RowVersion;
        }

        #endregion
    }
}