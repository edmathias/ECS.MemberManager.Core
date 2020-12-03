using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class PaymentSourceER : BusinessBase<PaymentSourceER>
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
        
        #endregion
        
        #region Factory Methods

        public static async Task<PaymentSourceER> NewPaymentSource()
        {
            return await DataPortal.CreateAsync<PaymentSourceER>();
        }

        public static async Task<PaymentSourceER> GetPaymentSource(int id)
        {
            return await DataPortal.FetchAsync<PaymentSourceER>(id);
        }

        public static async Task  DeletePaymentSource(int id)
        {
            await DataPortal.DeleteAsync<PaymentSourceER>(id);
        }
        
        #endregion
        
        #region Data Access Methods
        [Fetch]       
        private void Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPaymentSourceDal>();
            var data = dal.Fetch(id);
            using (BypassPropertyChecks)
            {
                Id = data.Id;
                Description = data.Description;
                Notes = data.Notes;
            }
        }

        [Insert]
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
        
        [Update]
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