using System;
using System.Collections.Generic;
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
    public class EMailEC : BusinessBase<EMailEC>
    {
        #region Business Methods
        
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }
       
        public static readonly PropertyInfo<string> EMailAddressProperty = RegisterProperty<string>(p => p.EMailAddress);
        [Required,MaxLength(255),EmailAddress()]
        public string EMailAddress
        {
            get => GetProperty(EMailAddressProperty);
            set => SetProperty(EMailAddressProperty, value);
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(p => p.LastUpdatedBy);
        [Required]
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

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(p => p.Notes);
        public string Notes
        {
            get => GetProperty(NotesProperty);
            set => SetProperty(NotesProperty, value);
        }
        
        public static readonly PropertyInfo<EMailTypeEdit> EMailTypeProperty = RegisterProperty<EMailTypeEdit>(p => p.EMailType);
        [Required]
        public EMailTypeEdit EMailType
        {
            get => GetProperty(EMailTypeProperty);
            set => SetProperty(EMailTypeProperty, value);
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

        public static async Task<EMailEC> NewEMail()
        {
            return await DataPortal.CreateAsync<EMailEC>();
        }

        public static async Task<EMailEC> GetEMail(EMail childData)
        {
            return await DataPortal.FetchAsync<EMailEC>(childData);
        }

        public static async Task DeleteEMail(int id)
        {
            await DataPortal.DeleteAsync<EMailEC>(id);
        }
        
        #endregion

        #region Data Access

        [Fetch]
        private async void Fetch(EMailType childData)
        {
            using (BypassPropertyChecks)
            {
                Id = childData.Id;
                EMailType = await EMailTypeEdit.GetEMailTypeEdit(childData);
            }
        }

        [Insert]
        private void Insert()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailDal>();
            using (BypassPropertyChecks)
            {
                var eMailToInsert = new EMail()
                {

                };

                var eMail = dal.Insert(eMailToInsert);
                this.Id = eMail.Id;
            }
        }

        [Update]
        private void Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailDal>();
            using (BypassPropertyChecks)
            {
                var eMailToUpdate = new EMail()
                {
                    Id = this.Id,
                    EMailAddress = this.EMailAddress,
                    EMailTypeId = this.EMailType.Id,
                    LastUpdatedBy = this.LastUpdatedBy,
                    LastUpdatedDate = this.LastUpdatedDate,
                    Notes = this.Notes,
                    // TODO: many-to-many
                    Organizations = new List<Organization>(),
                    Persons = new List<Person>()
                };
                
                dal.Update(eMailToUpdate);
            }
        }

        [DeleteSelf]
        private void DeleteSelf()
        {
            Delete(this.Id);
        }

        [Delete]
        private void Delete(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailDal>();

            dal.Delete(id);
        }


        #endregion
       
    }
}