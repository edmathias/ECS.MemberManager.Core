

//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/18/2021 16:28:29
//******************************************************************************    

using System;
using System.Collections.Generic; 
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class PaymentEC : BusinessBase<PaymentEC>
    {
        #region Business Methods
 
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public virtual int Id 
        {
            get => GetProperty(IdProperty); 
            private set => LoadProperty(IdProperty, value);    
        }


        public static readonly PropertyInfo<PersonEC> PersonProperty = RegisterProperty<PersonEC>(o => o.Person);
        public PersonEC Person  
        {
            get => GetProperty(PersonProperty); 
            set => SetProperty(PersonProperty, value); 
        }    
 
        public static readonly PropertyInfo<double> AmountProperty = RegisterProperty<double>(o => o.Amount);
        public virtual double Amount 
        {
            get => GetProperty(AmountProperty); 
            set => SetProperty(AmountProperty, value); 
   
        }

        public static readonly PropertyInfo<SmartDate> PaymentDateProperty = RegisterProperty<SmartDate>(o => o.PaymentDate);
        public virtual SmartDate PaymentDate 
        {
            get => GetProperty(PaymentDateProperty); 
            set => SetProperty(PaymentDateProperty, value); 
   
        }

        public static readonly PropertyInfo<SmartDate> PaymentExpirationDateProperty = RegisterProperty<SmartDate>(o => o.PaymentExpirationDate);
        public virtual SmartDate PaymentExpirationDate 
        {
            get => GetProperty(PaymentExpirationDateProperty); 
            set => SetProperty(PaymentExpirationDateProperty, value); 
   
        }


        public static readonly PropertyInfo<PaymentSourceEC> PaymentSourceProperty = RegisterProperty<PaymentSourceEC>(o => o.PaymentSource);
        public PaymentSourceEC PaymentSource  
        {
            get => GetProperty(PaymentSourceProperty); 
            set => SetProperty(PaymentSourceProperty, value); 
        }    
 

        public static readonly PropertyInfo<PaymentTypeEC> PaymentTypeProperty = RegisterProperty<PaymentTypeEC>(o => o.PaymentType);
        public PaymentTypeEC PaymentType  
        {
            get => GetProperty(PaymentTypeProperty); 
            set => SetProperty(PaymentTypeProperty, value); 
        }    
 
        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(o => o.LastUpdatedBy);
        public virtual string LastUpdatedBy 
        {
            get => GetProperty(LastUpdatedByProperty); 
            set => SetProperty(LastUpdatedByProperty, value); 
   
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty = RegisterProperty<SmartDate>(o => o.LastUpdatedDate);
        public virtual SmartDate LastUpdatedDate 
        {
            get => GetProperty(LastUpdatedDateProperty); 
            set => SetProperty(LastUpdatedDateProperty, value); 
   
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(o => o.Notes);
        public virtual string Notes 
        {
            get => GetProperty(NotesProperty); 
            set => SetProperty(NotesProperty, value); 
   
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); 
            set => SetProperty(RowVersionProperty, value); 
   
        }

        #endregion 

        #region Factory Methods
        internal static async Task<PaymentEC> NewPaymentEC()
        {
            return await DataPortal.CreateChildAsync<PaymentEC>();
        }

        internal static async Task<PaymentEC> GetPaymentEC(Payment childData)
        {
            return await DataPortal.FetchChildAsync<PaymentEC>(childData);
        }  


        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(Payment data)
        {
            using(BypassPropertyChecks)
            {
            Id = data.Id;
            Person = (data.Person != null ? await PersonEC.GetPersonEC(data.Person) : null);
            Amount = data.Amount;
            PaymentDate = data.PaymentDate;
            PaymentExpirationDate = data.PaymentExpirationDate;
            PaymentSource = (data.PaymentSource != null ? await PaymentSourceEC.GetPaymentSourceEC(data.PaymentSource) : null);
            PaymentType = (data.PaymentType != null ? await PaymentTypeEC.GetPaymentTypeEC(data.PaymentType) : null);
            LastUpdatedBy = data.LastUpdatedBy;
            LastUpdatedDate = data.LastUpdatedDate;
            Notes = data.Notes;
            RowVersion = data.RowVersion;
            }            
        }
        [InsertChild]
        private async Task Insert([Inject] IPaymentDal dal)
        {
            var data = new Payment()
            {

                Id = Id,
                Person = (Person != null ? new Person() { Id = Person.Id } : null),
                Amount = Amount,
                PaymentDate = PaymentDate,
                PaymentExpirationDate = PaymentExpirationDate,
                PaymentSource = (PaymentSource != null ? new PaymentSource() { Id = PaymentSource.Id } : null),
                PaymentType = (PaymentType != null ? new PaymentType() { Id = PaymentType.Id } : null),
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Insert(data);
            Id = insertedObj.Id;
            RowVersion = insertedObj.RowVersion;
        }

       [UpdateChild]
        private async Task Update([Inject] IPaymentDal dal)
        {
            var data = new Payment()
            {

                Id = Id,
                Person = (Person != null ? new Person() { Id = Person.Id } : null),
                Amount = Amount,
                PaymentDate = PaymentDate,
                PaymentExpirationDate = PaymentExpirationDate,
                PaymentSource = (PaymentSource != null ? new PaymentSource() { Id = PaymentSource.Id } : null),
                PaymentType = (PaymentType != null ? new PaymentType() { Id = PaymentType.Id } : null),
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Update(data);
            RowVersion = insertedObj.RowVersion;
        }

       
        [DeleteSelfChild]
        private async Task DeleteSelf([Inject] IPaymentDal dal)
        {
            await Delete(Id,dal);
        }
       
        [Delete]
        private async Task Delete(int id, [Inject] IPaymentDal dal)
        {
            await dal.Delete(id);
        }

        #endregion
    }
}
