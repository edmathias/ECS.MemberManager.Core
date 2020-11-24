using System;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    public partial class DocumentTypeER : BusinessBase<DocumentTypeER>
    {
 
        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Fetch(int id)
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IDocumentTypeDal>();
            var data = dal.Fetch(id);
            using (BypassPropertyChecks)
            {
                Id = data.Id;
                Notes = data.Notes;
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IDocumentTypeDal>();
            var addressToInsert = new ECS.MemberManager.Core.EF.Domain.DocumentType();
            using (BypassPropertyChecks)
            {
                addressToInsert.LastUpdatedDate = this.LastUpdatedDate;
                addressToInsert.LastUpdatedBy = this.LastUpdatedBy;
                addressToInsert.Notes = this.Notes; 
            }
            Id = dal.Insert(addressToInsert);
        }
        
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IDocumentTypeDal>();
            var addressToUpdate = new EF.Domain.DocumentType();
            using (BypassPropertyChecks)
            {
                addressToUpdate.LastUpdatedDate = this.LastUpdatedDate;
                addressToUpdate.LastUpdatedBy = this.LastUpdatedBy;
                addressToUpdate.Notes = this.Notes; 
            }

            dal.Update(addressToUpdate);
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
            var dal = dalManager.GetProvider<IDocumentTypeDal>();
 
            dal.Delete(id);
        }
        

    } // end class
} // end namespace