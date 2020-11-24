using System;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    public partial class EMailTypeER : BusinessBase<EMailTypeER>
    {
        #region Data Access Methods
        
        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Fetch(int id)
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailTypeDal>();
            var data = dal.Fetch(id);
            using (BypassPropertyChecks)
            {
                Id = data.Id;
                Description = data.TypeDescription;
                Notes = data.Notes;
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailTypeDal>();
            using (BypassPropertyChecks)
            {
                var eMailType = new ECS.MemberManager.Core.EF.Domain.EMailType 
                    { 
                        TypeDescription = this.Description, 
                        Notes = this.Notes 
                    };
                Id = dal.Insert(eMailType);
            }
        }
        
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailTypeDal>();
            using (BypassPropertyChecks)
            {
                var eMailType = new EF.Domain.EMailType
                {
                    Id = this.Id, 
                    TypeDescription = Description = this.Description, 
                    Notes = this.Notes
                };
                
                dal.Update(eMailType);
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
            var dal = dalManager.GetProvider<IEMailTypeDal>();
 
            dal.Delete(id);
        }
        
        #endregion 
    } // end class
} // end namespace