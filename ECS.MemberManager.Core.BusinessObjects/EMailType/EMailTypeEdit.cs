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
    public class EMailTypeEdit : BusinessBase<EMailTypeEdit>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);

        public int Id
        {
            get => GetProperty(IdProperty);
            set => SetProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(p => p.Description);

        [Required, MaxLength(50)]
        public string Description
        {
            get => GetProperty(DescriptionProperty);
            set => SetProperty(DescriptionProperty, value);
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

        public static async Task<EMailTypeEdit> NewEMailTypeEdit()
        {
            return await DataPortal.CreateAsync<EMailTypeEdit>();
        }

        public static async Task<EMailTypeEdit> GetEMailTypeEdit(EMailType childData)
        {
            return await DataPortal.FetchChildAsync<EMailTypeEdit>(childData);
        }

        public static async Task<EMailTypeEdit> GetEMailTypeEdit(int id)
        {
            return await DataPortal.FetchAsync<EMailTypeEdit>(id);
        }

        public static async Task DeleteEMailTypeEdit(int id)
        {
            await DataPortal.DeleteAsync<EMailTypeEdit>(id);
        }

        #endregion

        #region Data Access Methods
        
        [FetchChild]
        private void Fetch(EMailType childData)
        {
            using (BypassPropertyChecks)
            {
                Id = childData.Id;
                Description = childData.Description;
                Notes = childData.Notes;
                RowVersion = childData.RowVersion;
            }
        }

        [Fetch]
        private void Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailTypeDal>();
            var data = dal.Fetch(id);

            Fetch(data);
        }

        [Insert]
        private void Insert()
        {
            InsertChild();
        }

        [InsertChild]
        private void InsertChild()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailTypeDal>();
            var data = new EMailType()
            {
                Notes = Notes,
                Description = Description
            };

            var insertedEMailType = dal.Insert(data);
            Id = insertedEMailType.Id;
            RowVersion = insertedEMailType.RowVersion;
        }

        [Update]
        private void Update()
        {
            ChildUpdate();
        }

        [UpdateChild]
        private void ChildUpdate()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailTypeDal>();

            var emailTypeToUpdate = new EMailType()
            {
                Id = Id,
                Description = Description,
                Notes = Notes,
                RowVersion = RowVersion
            };

            var updatedEmail = dal.Update(emailTypeToUpdate);
            RowVersion = updatedEmail.RowVersion;
        }
        
        [DeleteSelfChild]
        private void DeleteSelf()
        {
            Delete(Id);
        }

        [Delete]
        private void Delete(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailTypeDal>();
           
            dal.Delete(id);
        }

        #endregion
    }
}