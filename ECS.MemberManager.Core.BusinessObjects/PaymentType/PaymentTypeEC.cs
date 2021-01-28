using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class PaymentTypeEC : BusinessBase<PaymentTypeEC>
    {
        #region Business Methods
        
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
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
        
        #endregion

        #region Factory Methods

        internal static async Task<PaymentTypeEC> NewPaymentTypeEC()
        {
            return await DataPortal.CreateChildAsync<PaymentTypeEC>();
        }        
        
        internal static async Task<PaymentTypeEC> GetPaymentTypeEC(PaymentType data)
        {
            return await DataPortal.FetchChildAsync<PaymentTypeEC>(data);
        }

        #endregion

        #region Data Access Methods
 
        [FetchChild]
        private void FetchChild(PaymentType childData)
        {
            using (BypassPropertyChecks)
            {
                Id = childData.Id;
                Description = childData.Description;
                Notes = childData.Notes;
                RowVersion = childData.RowVersion;
            }
        }

        [InsertChild]
        private async Task InsertChild()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPaymentTypeDal>();
            var data = new PaymentType()
            {
                Description = Description,
                Notes = Notes
            };

            var insertedPaymentType = await dal.Insert(data);
            Id = insertedPaymentType.Id;
            RowVersion = insertedPaymentType.RowVersion;
        }

        [UpdateChild]
        private async Task UpdateChild()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPaymentTypeDal>();

            var paymentTypeToUpdate = new PaymentType()
            {
                Id = Id,
                Description = Description,
                Notes = Notes,
                RowVersion = RowVersion
            };

            var updatedStatus = await dal.Update(paymentTypeToUpdate);
            RowVersion = updatedStatus.RowVersion;
        }

        [DeleteSelfChild]
        private async Task DeleteSelfChild()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPaymentTypeDal>();
           
            await dal.Delete(Id);
        }

        #endregion
    }
}