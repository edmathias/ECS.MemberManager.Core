

//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 04/07/2021 09:32:30
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
    public partial class PaymentSourceEC : BusinessBase<PaymentSourceEC>
    {
        #region Business Methods
 
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public virtual int Id 
        {
            get => GetProperty(IdProperty); 
            private set => LoadProperty(IdProperty, value);    
        }

        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(o => o.Description);
        public virtual string Description 
        {
            get => GetProperty(DescriptionProperty); 
            set => SetProperty(DescriptionProperty, value); 
   
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
        internal static async Task<PaymentSourceEC> NewPaymentSourceEC()
        {
            return await DataPortal.CreateChildAsync<PaymentSourceEC>();
        }

        internal static async Task<PaymentSourceEC> GetPaymentSourceEC(PaymentSource childData)
        {
            return await DataPortal.FetchChildAsync<PaymentSourceEC>(childData);
        }  


        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(PaymentSource data)
        {
            using(BypassPropertyChecks)
            {
            Id = data.Id;
            Description = data.Description;
            Notes = data.Notes;
            RowVersion = data.RowVersion;
            }            
        }
        [InsertChild]
        private async Task Insert([Inject] IDal<PaymentSource> dal)
        {
            FieldManager.UpdateChildren();

            var data = new PaymentSource()
            {

                Id = Id,
                Description = Description,
                Notes = Notes,
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Insert(data);
            Id = insertedObj.Id;
            RowVersion = insertedObj.RowVersion;
        }

       [UpdateChild]
        private async Task Update([Inject] IDal<PaymentSource> dal)
        {
            FieldManager.UpdateChildren();

            var data = new PaymentSource()
            {

                Id = Id,
                Description = Description,
                Notes = Notes,
                RowVersion = RowVersion,
            };
            
            PaymentSource insertedObj = null;

            try
            {
                insertedObj = await dal.Update(data);
            }
            catch (Exception exc)
            {
                System.Console.WriteLine(exc.Message);
            }
            RowVersion = insertedObj.RowVersion;
        }

       
        [DeleteSelfChild]
        private async Task DeleteSelf([Inject] IDal<PaymentSource> dal)
        {
            await Delete(Id,dal);
        }
       
        [Delete]
        private async Task Delete(int id, [Inject] IDal<PaymentSource> dal)
        {
            await dal.Delete(id);
        }

        #endregion
    }
}
