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
    public class MemberStatusEdit : BusinessBase<MemberStatusEdit>
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

        public static async Task<MemberStatusEdit> NewMemberStatusEdit()
        {
            return await DataPortal.CreateAsync<MemberStatusEdit>();
        }

        public static async Task<MemberStatusEdit> GetMemberStatusEdit(MemberStatus childData)
        {
            return await DataPortal.FetchChildAsync<MemberStatusEdit>(childData);
        }

        public static async Task<MemberStatusEdit> GetMemberStatusEdit(int id)
        {
            return await DataPortal.FetchAsync<MemberStatusEdit>(id);
        }

        public static async Task DeleteMemberStatusEdit(int id)
        {
            await DataPortal.DeleteAsync<MemberStatusEdit>(id);
        }

        #endregion

        #region Data Access Methods
        
        [Fetch]
        private async Task Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMemberStatusDal>();
            var data = await dal.Fetch(id);

            Fetch(data);
        }

        [FetchChild]
        private void Fetch(MemberStatus childData)
        {
            using (BypassPropertyChecks)
            {
                Id = childData.Id;
                Description = childData.Description;
                Notes = childData.Notes;
                RowVersion = childData.RowVersion;
            }
        }

        [Insert]
        private async Task Insert()
        {
            await InsertChild();
        }

        [InsertChild]
        private async Task InsertChild()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMemberStatusDal>();
            var data = new MemberStatus()
            {
                Notes = Notes,
                Description = Description
            };

            var insertedMemberStatus = await dal.Insert(data);
            Id = insertedMemberStatus.Id;
            RowVersion = insertedMemberStatus.RowVersion;
        }

        [Update]
        private async Task Update()
        {
            await ChildUpdate();
        }

        [UpdateChild]
        private async Task ChildUpdate()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMemberStatusDal>();

            var emailTypeToUpdate = new MemberStatus()
            {
                Id = Id,
                Description = Description,
                Notes = Notes,
                RowVersion = RowVersion
            };

            var updatedEmail = await dal.Update(emailTypeToUpdate);
            RowVersion = updatedEmail.RowVersion;
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
            var dal = dalManager.GetProvider<IMemberStatusDal>();
           
            await dal.Delete(id);
        }

        #endregion
    }
}