


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/02/2021 21:50:29
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
    public partial class PaymentROR : BusinessBase<PaymentROR>
    {
        #region Business Methods 
         public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public virtual int Id 
        {
            get => GetProperty(IdProperty); 
            private set => LoadProperty(IdProperty, value);    
        }


        public static readonly PropertyInfo<PersonROC> PersonProperty = RegisterProperty<PersonROC>(o => o.Person);
        public PersonROC Person  
        {
            get => GetProperty(PersonProperty); 
        
            private set => LoadProperty(PersonProperty, value); 
        }    
 
        public static readonly PropertyInfo<double> AmountProperty = RegisterProperty<double>(o => o.Amount);
        public virtual double Amount 
        {
            get => GetProperty(AmountProperty); 
            private set => LoadProperty(AmountProperty, value);    
        }

        public static readonly PropertyInfo<SmartDate> PaymentDateProperty = RegisterProperty<SmartDate>(o => o.PaymentDate);
        public virtual SmartDate PaymentDate 
        {
            get => GetProperty(PaymentDateProperty); 
            private set => LoadProperty(PaymentDateProperty, value);    
        }

        public static readonly PropertyInfo<SmartDate> PaymentExpirationDateProperty = RegisterProperty<SmartDate>(o => o.PaymentExpirationDate);
        public virtual SmartDate PaymentExpirationDate 
        {
            get => GetProperty(PaymentExpirationDateProperty); 
            private set => LoadProperty(PaymentExpirationDateProperty, value);    
        }


        public static readonly PropertyInfo<PaymentSourceROC> PaymentSourceProperty = RegisterProperty<PaymentSourceROC>(o => o.PaymentSource);
        public PaymentSourceROC PaymentSource  
        {
            get => GetProperty(PaymentSourceProperty); 
        
            private set => LoadProperty(PaymentSourceProperty, value); 
        }    
 

        public static readonly PropertyInfo<PaymentTypeROC> PaymentTypeProperty = RegisterProperty<PaymentTypeROC>(o => o.PaymentType);
        public PaymentTypeROC PaymentType  
        {
            get => GetProperty(PaymentTypeProperty); 
        
            private set => LoadProperty(PaymentTypeProperty, value); 
        }    
 
        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(o => o.LastUpdatedBy);
        public virtual string LastUpdatedBy 
        {
            get => GetProperty(LastUpdatedByProperty); 
            private set => LoadProperty(LastUpdatedByProperty, value);    
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty = RegisterProperty<SmartDate>(o => o.LastUpdatedDate);
        public virtual SmartDate LastUpdatedDate 
        {
            get => GetProperty(LastUpdatedDateProperty); 
            private set => LoadProperty(LastUpdatedDateProperty, value);    
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(o => o.Notes);
        public virtual string Notes 
        {
            get => GetProperty(NotesProperty); 
            private set => LoadProperty(NotesProperty, value);    
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); 
            private set => LoadProperty(RowVersionProperty, value);    
        }

        #endregion 

        #region Factory Methods
        public static async Task<PaymentROR> GetPaymentROR(int id)
        {
            return await DataPortal.FetchAsync<PaymentROR>(id);
        }  


        #endregion

        #region Data Access Methods

        [Fetch]
        private async Task Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPaymentDal>();
            var data = await dal.Fetch(id);
                Id = data.Id;
                Person = (data.Person != null ? await PersonROC.GetPersonROC(data.Person) : null);
                Amount = data.Amount;
                PaymentDate = data.PaymentDate;
                PaymentExpirationDate = data.PaymentExpirationDate;
                PaymentSource = (data.PaymentSource != null ? await PaymentSourceROC.GetPaymentSourceROC(data.PaymentSource) : null);
                PaymentType = (data.PaymentType != null ? await PaymentTypeROC.GetPaymentTypeROC(data.PaymentType) : null);
                LastUpdatedBy = data.LastUpdatedBy;
                LastUpdatedDate = data.LastUpdatedDate;
                Notes = data.Notes;
                RowVersion = data.RowVersion;
        }

        #endregion
    }
}
