

//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/25/2021 11:07:37
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
    public partial class EMailEC : BusinessBase<EMailEC>
    {
        #region Business Methods
 
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public virtual int Id 
        {
            get => GetProperty(IdProperty); 
            private set => LoadProperty(IdProperty, value);    
        }

        public static readonly PropertyInfo<string> EMailAddressProperty = RegisterProperty<string>(o => o.EMailAddress);
        public virtual string EMailAddress 
        {
            get => GetProperty(EMailAddressProperty); 
            set => SetProperty(EMailAddressProperty, value); 
   
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


        public static readonly PropertyInfo<EMailTypeEC> EMailTypeProperty = RegisterProperty<EMailTypeEC>(o => o.EMailType);
        public EMailTypeEC EMailType  
        {
            get => GetProperty(EMailTypeProperty); 
            set => SetProperty(EMailTypeProperty, value); 
        }    
 
        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); 
            set => SetProperty(RowVersionProperty, value); 
   
        }

        #endregion 

        #region Factory Methods
        internal static async Task<EMailEC> NewEMailEC()
        {
            return await DataPortal.CreateChildAsync<EMailEC>();
        }

        internal static async Task<EMailEC> GetEMailEC(EMail childData)
        {
            return await DataPortal.FetchChildAsync<EMailEC>(childData);
        }  


        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(EMail data)
        {
            using(BypassPropertyChecks)
            {
            Id = data.Id;
            EMailAddress = data.EMailAddress;
            LastUpdatedBy = data.LastUpdatedBy;
            LastUpdatedDate = data.LastUpdatedDate;
            Notes = data.Notes;
            EMailType = (data.EMailType != null ? await EMailTypeEC.GetEMailTypeEC(data.EMailType) : null);
            RowVersion = data.RowVersion;
            }            
        }
        [InsertChild]
        private async Task Insert([Inject] IEMailDal dal)
        {
            FieldManager.UpdateChildren();

            var data = new EMail()
            {

                Id = Id,
                EMailAddress = EMailAddress,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
                EMailType = (EMailType != null ? new EMailType() { Id = EMailType.Id } : null),
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Insert(data);
            Id = insertedObj.Id;
            RowVersion = insertedObj.RowVersion;
        }

       [UpdateChild]
        private async Task Update([Inject] IEMailDal dal)
        {
            FieldManager.UpdateChildren();

            var data = new EMail()
            {

                Id = Id,
                EMailAddress = EMailAddress,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
                EMailType = (EMailType != null ? new EMailType() { Id = EMailType.Id } : null),
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Update(data);
            RowVersion = insertedObj.RowVersion;
        }

       
        [DeleteSelfChild]
        private async Task DeleteSelf([Inject] IEMailDal dal)
        {
            await Delete(Id,dal);
        }
       
        [Delete]
        private async Task Delete(int id, [Inject] IEMailDal dal)
        {
            await dal.Delete(id);
        }

        #endregion
    }
}
