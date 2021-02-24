


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
    public partial class MemberInfoROR : BusinessBase<MemberInfoROR>
    {
        #region Business Methods 
         public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public virtual int Id 
        {
            get => GetProperty(IdProperty); //1-2
            private set => LoadProperty(IdProperty, value); //2-3   
        }


        public static readonly PropertyInfo<PersonROC> PersonProperty = RegisterProperty<PersonROC>(o => o.Person);
        public PersonROC Person  
        {
            get => GetProperty(PersonProperty); //1-1
        
            private set => LoadProperty(PersonProperty, value); //2-1
        }    
 
        public static readonly PropertyInfo<string> MemberNumberProperty = RegisterProperty<string>(o => o.MemberNumber);
        public virtual string MemberNumber 
        {
            get => GetProperty(MemberNumberProperty); //1-2
            private set => LoadProperty(MemberNumberProperty, value); //2-3   
        }

        public static readonly PropertyInfo<SmartDate> DateFirstJoinedProperty = RegisterProperty<SmartDate>(o => o.DateFirstJoined);
        public virtual SmartDate DateFirstJoined 
        {
            get => GetProperty(DateFirstJoinedProperty); //1-2
            private set => LoadProperty(DateFirstJoinedProperty, value); //2-3   
        }


        public static readonly PropertyInfo<PrivacyLevelROC> PrivacyLevelProperty = RegisterProperty<PrivacyLevelROC>(o => o.PrivacyLevel);
        public PrivacyLevelROC PrivacyLevel  
        {
            get => GetProperty(PrivacyLevelProperty); //1-1
        
            private set => LoadProperty(PrivacyLevelProperty, value); //2-1
        }    
 

        public static readonly PropertyInfo<MemberStatusROC> MemberStatusProperty = RegisterProperty<MemberStatusROC>(o => o.MemberStatus);
        public MemberStatusROC MemberStatus  
        {
            get => GetProperty(MemberStatusProperty); //1-1
        
            private set => LoadProperty(MemberStatusProperty, value); //2-1
        }    
 

        public static readonly PropertyInfo<MembershipTypeROC> MembershipTypeProperty = RegisterProperty<MembershipTypeROC>(o => o.MembershipType);
        public MembershipTypeROC MembershipType  
        {
            get => GetProperty(MembershipTypeProperty); //1-1
        
            private set => LoadProperty(MembershipTypeProperty, value); //2-1
        }    
 
        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(o => o.LastUpdatedBy);
        public virtual string LastUpdatedBy 
        {
            get => GetProperty(LastUpdatedByProperty); //1-2
            private set => LoadProperty(LastUpdatedByProperty, value); //2-3   
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty = RegisterProperty<SmartDate>(o => o.LastUpdatedDate);
        public virtual SmartDate LastUpdatedDate 
        {
            get => GetProperty(LastUpdatedDateProperty); //1-2
            private set => LoadProperty(LastUpdatedDateProperty, value); //2-3   
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(o => o.Notes);
        public virtual string Notes 
        {
            get => GetProperty(NotesProperty); //1-2
            private set => LoadProperty(NotesProperty, value); //2-3   
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); //1-2
            private set => LoadProperty(RowVersionProperty, value); //2-3   
        }

        #endregion 

        #region Factory Methods
        public static async Task<MemberInfoROR> GetMemberInfoROR(int id)
        {
            return await DataPortal.FetchAsync<MemberInfoROR>(id);
        }  


        #endregion

        #region Data Access Methods

        [Fetch]
        private async Task Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMemberInfoDal>();
            var data = await dal.Fetch(id);
                Id = data.Id;
                Person = (data.Person != null ? await PersonROC.GetPersonROC(data.Person) : null);
                MemberNumber = data.MemberNumber;
                DateFirstJoined = data.DateFirstJoined;
                PrivacyLevel = (data.PrivacyLevel != null ? await PrivacyLevelROC.GetPrivacyLevelROC(data.PrivacyLevel) : null);
                MemberStatus = (data.MemberStatus != null ? await MemberStatusROC.GetMemberStatusROC(data.MemberStatus) : null);
                MembershipType = (data.MembershipType != null ? await MembershipTypeROC.GetMembershipTypeROC(data.MembershipType) : null);
                LastUpdatedBy = data.LastUpdatedBy;
                LastUpdatedDate = data.LastUpdatedDate;
                Notes = data.Notes;
                RowVersion = data.RowVersion;
        }

        #endregion
    }
}
