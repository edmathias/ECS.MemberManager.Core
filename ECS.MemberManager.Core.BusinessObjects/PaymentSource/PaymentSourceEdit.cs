using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class PaymentSourceEdit : BusinessBase<PaymentSourceEdit>
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
            set => SetProperty(RowVersionProperty, value);
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

        public static async Task<PaymentSourceEdit> NewPaymentSourceEdit()
        {
            return await DataPortal.CreateAsync<PaymentSourceEdit>();
        }

        public static async Task<PaymentSourceEdit> GetPaymentSourceEdit(int id)
        {
            return await DataPortal.FetchAsync<PaymentSourceEdit>(id);
        }

        public static async Task<PaymentSourceEdit> GetPaymentSourceEdit(PaymentSource childData)
        {
            return await DataPortal.FetchChildAsync<PaymentSourceEdit>(childData);
        }

        public static async Task DeletePaymentSourceEdit(int id)
        {
            await DataPortal.DeleteAsync<PaymentSourceEdit>(id);
        }

        #endregion

        #region Data Access Methods

        [Fetch]
        private async Task Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPaymentSourceDal>();
            var data = await dal.Fetch(id);

            Fetch(data);
        }

        [FetchChild]
        private void Fetch(PaymentSource childData)
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
            var dal = dalManager.GetProvider<IPaymentSourceDal>();
            using (BypassPropertyChecks)
            {
                var paymentSource = new EF.Domain.PaymentSource
                {
                    Description = this.Description,
                    Notes = this.Notes
                };
                var insertedPaymentSource = await dal.Insert(paymentSource);
                Id = insertedPaymentSource.Id;
                RowVersion = insertedPaymentSource.RowVersion;
            }
        }

        [Update]
        private async Task Update()
        {
            await UpdateChild();
        }
        
        [UpdateChild]
        private async Task UpdateChild()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPaymentSourceDal>();
            using (BypassPropertyChecks)
            {
                var paymentSource = new EF.Domain.PaymentSource
                {
                    Id = Id,
                    Description = Description,
                    Notes = Notes,
                    RowVersion = RowVersion
                };

                var updatedPaymentSource = await dal.Update(paymentSource);
                RowVersion = updatedPaymentSource.RowVersion;
            }
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
            var dal = dalManager.GetProvider<IPaymentSourceDal>();

            await dal.Delete(id);
        }

        #endregion
    }
}