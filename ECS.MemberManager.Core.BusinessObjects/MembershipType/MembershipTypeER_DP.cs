using System;
using System.Threading.Tasks.Dataflow;
using Csla;
using Csla.Data;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    public partial class MembershipTypeER : BusinessBase<MembershipTypeER>
    {
 
        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Fetch(int id)
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMembershipTypeDal>();
            var data = dal.Fetch(id);
            using (BypassPropertyChecks)
            {
                Id = data.Id;
                Description = data.Description;
                Level = data.Level;
                LastUpdatedBy = data.LastUpdatedBy;
                LastUpdatedDate = data.LastUpdatedDate;
                Notes = data.Notes;
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMembershipTypeDal>();
            var documentTypeToInsert = new ECS.MemberManager.Core.EF.Domain.MembershipType();
            using (BypassPropertyChecks)
            {
                documentTypeToInsert.Description = this.Description;
                documentTypeToInsert.LastUpdatedDate = this.LastUpdatedDate;
                documentTypeToInsert.LastUpdatedBy = this.LastUpdatedBy;
                documentTypeToInsert.Notes = this.Notes;
                documentTypeToInsert.Level = this.Level;
            }
            Id = dal.Insert(documentTypeToInsert);
        }
        
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMembershipTypeDal>();
            var documentTypeToUpdate = dal.Fetch(Id);
            using (BypassPropertyChecks)
            {
                documentTypeToUpdate.Description = this.Description;
                documentTypeToUpdate.Level = this.Level;
                documentTypeToUpdate.LastUpdatedDate = this.LastUpdatedDate;
                documentTypeToUpdate.LastUpdatedBy = this.LastUpdatedBy;
                documentTypeToUpdate.Notes = this.Notes; 
            }

            dal.Update(documentTypeToUpdate);
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
            var dal = dalManager.GetProvider<IMembershipTypeDal>();
 
            dal.Delete(id);
        }
    } // end class
} // end namespace