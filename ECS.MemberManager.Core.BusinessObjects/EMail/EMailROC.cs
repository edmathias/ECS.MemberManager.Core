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
    public class EMailROC : BusinessBase<EMailROC>
    {
        #region Business Methods
        
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<EMailTypeEC> EMailTypeProperty = RegisterProperty<EMailTypeEC>(p => p.EMailType);
        public EMailTypeEC EMailType
        {
            get => GetProperty(EMailTypeProperty);
            private set => LoadProperty(EMailTypeProperty, value);
        }

        public static readonly PropertyInfo<string> EMailAddressProperty = RegisterProperty<string>(p => p.EMailAddress);
        public string EMailAddress
        {
            get => GetProperty(EMailAddressProperty);
            private set => LoadProperty(EMailAddressProperty, value);
        }
        
        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(p => p.LastUpdatedBy);
        public string LastUpdatedBy
        {
            get => GetProperty(LastUpdatedByProperty);
            private set => LoadProperty(LastUpdatedByProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty = RegisterProperty<SmartDate>(p => p.LastUpdatedDate);
        public SmartDate LastUpdatedDate
        {
            get => GetProperty(LastUpdatedDateProperty);
            private set => LoadProperty(LastUpdatedDateProperty, value);
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(p => p.Notes);

        public string Notes
        {
            get => GetProperty(NotesProperty);
            private set => LoadProperty(NotesProperty, value);
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(p => p.RowVersion);

        public byte[] RowVersion
        {
            get => GetProperty(RowVersionProperty);
            private set => LoadProperty(RowVersionProperty, value);
        }
        
        #endregion

        #region Factory Methods

        internal static async Task<EMailROC> GetEMailROC(EMail data)
        {
            return await DataPortal.FetchChildAsync<EMailROC>(data);
        }

        #endregion

        #region Data Access Methods
 
        [FetchChild]
        private async void FetchChild(EMail childData)
        {
            using (BypassPropertyChecks)
            {
                Id = childData.Id;
                EMailType = await EMailTypeEC.GetEMailTypeEC(childData.EMailType);
                EMailAddress = childData.EMailAddress;
                LastUpdatedBy = childData.LastUpdatedBy;
                LastUpdatedDate = childData.LastUpdatedDate;
                Notes = childData.Notes;
                RowVersion = childData.RowVersion;
            }
        }

        #endregion
    }
}