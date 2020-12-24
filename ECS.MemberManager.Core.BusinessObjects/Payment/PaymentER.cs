using System;
using System.Collections.Generic;
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
    public class PaymentER : BusinessBase<PaymentER>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);

        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<double> AmountProperty = RegisterProperty<double>(p => p.Amount);

        public double Amount
        {
            get => GetProperty(AmountProperty);
            set => SetProperty(AmountProperty, Math.Round(value, 2));
        }

        public static readonly PropertyInfo<SmartDate> PaymentDateProperty =
            RegisterProperty<SmartDate>(p => p.PaymentDate);

        [Required]
        public SmartDate PaymentDate
        {
            get => GetProperty(PaymentDateProperty);
            set => SetProperty(PaymentDateProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> PaymentExpirationDateProperty =
            RegisterProperty<SmartDate>(p => p.PaymentExpirationDate);

        public SmartDate PaymentExpirationDate
        {
            get => GetProperty(PaymentExpirationDateProperty);
            set => SetProperty(PaymentExpirationDateProperty, value);
        }

        public static readonly PropertyInfo<PaymentSourceEC> PaymentSourceProperty =
            RegisterProperty<PaymentSourceEC>(p => p.PaymentSource);

        [Required]
        public PaymentSourceEC PaymentSource
        {
            get => GetProperty(PaymentSourceProperty);
            set => SetProperty(PaymentSourceProperty, value);
        }

        public static readonly PropertyInfo<PaymentTypeEC> PaymentTypeProperty =
            RegisterProperty<PaymentTypeEC>(p => p.PaymentType);

        [Required]
        public PaymentTypeEC PaymentType
        {
            get => GetProperty(PaymentTypeProperty);
            set => SetProperty(PaymentTypeProperty, value);
        }

        public static readonly PropertyInfo<PersonEC> PersonProperty = RegisterProperty<PersonEC>(p => p.Person);

        public PersonEC Person
        {
            get => GetProperty(PersonProperty);
            set => SetProperty(PersonProperty, value);
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty =
            RegisterProperty<string>(p => p.LastUpdatedBy);

        [Required]
        public string LastUpdatedBy
        {
            get => GetProperty(LastUpdatedByProperty);
            set => SetProperty(LastUpdatedByProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty =
            RegisterProperty<SmartDate>(p => p.LastUpdatedDate);

        [Required]
        public SmartDate LastUpdatedDate
        {
            get => GetProperty(LastUpdatedDateProperty);
            set => SetProperty(LastUpdatedDateProperty, value);
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(p => p.Notes);

        public string Notes
        {
            get => GetProperty(NotesProperty);
            set => SetProperty(NotesProperty, value);
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

        public static async Task<PaymentER> NewPayment()
        {
            return await DataPortal.CreateAsync<PaymentER>();
        }

        public static async Task<PaymentER> GetPayment(int id)
        {
            return await DataPortal.FetchAsync<PaymentER>(id);
        }

        public static async Task DeletePayment(int id)
        {
            await DataPortal.DeleteAsync<PaymentER>(id);
        }

        #endregion

        #region Data Access

        [Fetch]
        private async void Fetch(int id)
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPaymentDal>();
            using (BypassPropertyChecks)
            {
                var data = dal.Fetch(id);
                Id = data.Id;
                Amount = Convert.ToDouble(data.Amount);
                Notes = data.Notes;
                PaymentDate = data.PaymentDate;
                LastUpdatedBy = data.LastUpdatedBy;
                PaymentExpirationDate = data.PaymentExpirationDate;

                PaymentSource = await PaymentSourceEC.GetPaymentSource(data.PaymentSource);
                PaymentType = await PaymentTypeEC.GetPaymentType(data.PaymentType);
                Person = await PersonEC.GetPerson(data.Person);
            }
        }

        [Insert]
        private void Insert()
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPaymentDal>();
            var paymentToInsert = new Payment();

            paymentToInsert.Amount = Amount;
            paymentToInsert.Notes = Notes;
            paymentToInsert.PaymentDate = PaymentDate;
            paymentToInsert.LastUpdatedBy = LastUpdatedBy;
            paymentToInsert.PaymentExpirationDate = PaymentExpirationDate;

            Id = dal.Insert(paymentToInsert);
        }

        [Update]
        private void Update()
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPaymentDal>();
            using (BypassPropertyChecks)
            {
                var paymentToUpdate = dal.Fetch(Id);

                paymentToUpdate.Id = Id;
                paymentToUpdate.Amount = Amount;
                paymentToUpdate.Notes = Notes;
                paymentToUpdate.PaymentDate = PaymentDate;
                paymentToUpdate.LastUpdatedBy = LastUpdatedBy;
                paymentToUpdate.PaymentExpirationDate = PaymentExpirationDate;

                dal.Update(paymentToUpdate);
            }
        }

        [DeleteSelf]
        private void DeleteSelf()
        {
            Delete(this.Id);
        }

        [Delete]
        private void Delete(int id)
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPaymentDal>();

            dal.Delete(id);
        }

        #endregion
    }
}