using System;
using System.ComponentModel.DataAnnotations;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class MemberStatusER : BusinessBase<MemberStatusER>
    {
        #region Business Methods
        
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            private set { LoadProperty(IdProperty, value); }
        }

        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(p => p.Description);
        [Required,MaxLength(255)]
        public string Description
        {
            get { return GetProperty(DescriptionProperty); }
            set { SetProperty(DescriptionProperty, value); }
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(p => p.Notes);
        public string Notes
        {
            get { return GetProperty(NotesProperty); }
            set { SetProperty(NotesProperty, value); }
        }
        
        #endregion
        
        #region Factory Methods

        public static MemberStatusER NewMemberStatusER()
        {
            return DataPortal.Create<MemberStatusER>();
        }

        public static MemberStatusER GetMemberStatusER(int id)
        {
            return DataPortal.Fetch<MemberStatusER>(id);
        }

        public static void DeleteMemberStatusER(int id)
        {
            DataPortal.Delete<MemberStatusER>(id);
        }
        
        #endregion
        
        #region Data Access 
        
        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Fetch(int id)
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMemberStatusDal>();
            var data = dal.Fetch(id);
            using (BypassPropertyChecks)
            {
                Id = data.Id;
                Description = data.Description;
                Notes = data.Notes;
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMemberStatusDal>();
            using (BypassPropertyChecks)
            {
                var memberStatus = new ECS.MemberManager.Core.EF.Domain.MemberStatus 
                    { Description = this.Description, Notes = this.Notes };
                Id = dal.Insert(memberStatus);
            }
        }
        
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMemberStatusDal>();
            using (BypassPropertyChecks)
            {
                var memberStatus = new EF.Domain.MemberStatus
                    {Id = this.Id, Description = this.Description, Notes = this.Notes};
                dal.Update(memberStatus);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(this.Id);
        }
        
        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(int id)
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMemberStatusDal>();
 
            dal.Delete(id);
        }
        
    #endregion
        
    }
}