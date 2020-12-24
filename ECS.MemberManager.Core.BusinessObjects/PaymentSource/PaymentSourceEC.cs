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
    public class PaymentSourceEC : BusinessBase<PaymentSourceEC>
    {
        #region Business Methods
        
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(p => p.Description);
        [Required,MaxLength(50)]
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

        public static async Task<PaymentSourceEC> NewPaymentSource()
        {
            return await DataPortal.CreateAsync<PaymentSourceEC>();
        }

        public static async Task<PaymentSourceEC> GetPaymentSource(PaymentSource childData)
        {
            return await DataPortal.FetchAsync<PaymentSourceEC>(childData);
        }

        public static async Task  DeletePaymentSource(int id)
        {
            await DataPortal.DeleteAsync<PaymentSourceER>(id);
        }
        
        #endregion
        
        #region Data Access Methods
        [FetchChild]       
        private void Fetch(PaymentSource childData)
        {
            using (BypassPropertyChecks)
            {
                Id = childData.Id;
                Description = childData.Description;
                Notes = childData.Notes;
            }
        }

        [InsertChild]
        private void Insert()
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
                Id = dal.Insert(paymentSource);
            }
        }
        
        [UpdateChild]
        private void Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPaymentSourceDal>();
            using (BypassPropertyChecks)
            {
                var paymentSource = new EF.Domain.PaymentSource
                {
                    Id = this.Id, 
                    Description = this.Description, 
                    Notes = this.Notes
                };
                
                dal.Update(paymentSource);
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
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPaymentSourceDal>();
 
            dal.Delete(id);
        }
        
        #endregion
       
    }
}