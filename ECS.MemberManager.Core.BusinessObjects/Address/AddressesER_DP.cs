using System;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    public partial class AddressER : BusinessBase<AddressER>
    {
 
        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Fetch(int id)
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IAddressDal>();
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
            var dal = dalManager.GetProvider<IAddressDal>();
            var addressToInsert = new ECS.MemberManager.Core.EF.Domain.Address();
            using (BypassPropertyChecks)
            {
                addressToInsert.Address1 = this.Address1;
                addressToInsert.Address2 = this.Address2;
                addressToInsert.City = this.City;
                addressToInsert.State = this.State;
                addressToInsert.PostCode = this.PostCode;
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
            var dal = dalManager.GetProvider<IAddressDal>();
            var addressToUpdate = new EF.Domain.Address();
            using (BypassPropertyChecks)
            {
                addressToUpdate.Address1 = this.Address1;
                addressToUpdate.Address2 = this.Address2;
                addressToUpdate.City = this.City;
                addressToUpdate.State = this.State;
                addressToUpdate.PostCode = this.PostCode;
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
            var dal = dalManager.GetProvider<IAddressDal>();
 
            dal.Delete(id);
        }
        

    } // end class
} // end namespace