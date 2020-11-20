using System;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects.MemberStatus
{
    public partial class MemberStatusER
    {

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
            var memberStatus = dal.Fetch(id);
 
            dal.Delete(id);
        }
        

    } // end class
} // end namespace