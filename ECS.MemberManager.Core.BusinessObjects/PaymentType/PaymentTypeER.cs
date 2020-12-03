using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class PaymentTypeER : BusinessBase<PaymentTypeER>
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

        public static async Task<PaymentTypeER> NewPaymentType()
        {
            return await DataPortal.CreateAsync<PaymentTypeER>();
        }

        public static async Task<PaymentTypeER> GetPaymentType(int id)
        {
            return await DataPortal.FetchAsync<PaymentTypeER>(id);
        }

        public static async Task  DeletePaymentType(int id)
        {
            await DataPortal.DeleteAsync<PaymentTypeER>(id);
        }
        
        #endregion
        
        #region Data Access Methods
        [Fetch]       
        private void Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPaymentTypeDal>();
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
            var dal = dalManager.GetProvider<IPaymentTypeDal>();
            using (BypassPropertyChecks)
            {
                var paymentSource = new EF.Domain.PaymentType 
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
            var dal = dalManager.GetProvider<IPaymentTypeDal>();
            using (BypassPropertyChecks)
            {
                var paymentSource = new EF.Domain.PaymentType
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
            var dal = dalManager.GetProvider<IPaymentTypeDal>();
 
            dal.Delete(id);
        }
        
        #endregion
       
    }
}