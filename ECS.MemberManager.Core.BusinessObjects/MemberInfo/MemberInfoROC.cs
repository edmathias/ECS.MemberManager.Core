


using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class MemberInfoROC : ReadOnlyBase<MemberInfoROC>
    {
        #region Business Methods 

 
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public int Id 
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

        public static readonly PropertyInfo<string> MemberNumberProperty = RegisterProperty<string>(o => o.MemberNumber);
        public string MemberNumber 
        {
            get => GetProperty(MemberNumberProperty); 
            private set => LoadProperty(MemberNumberProperty, value); 
   
        }        

        public static readonly PropertyInfo<SmartDate> DateFirstJoinedProperty = RegisterProperty<SmartDate>(o => o.DateFirstJoined);
        public SmartDate DateFirstJoined 
        {
            get => GetProperty(DateFirstJoinedProperty); 
            private set => LoadProperty(DateFirstJoinedProperty, value); 
   
        }        
        public static readonly PropertyInfo<PrivacyLevelROC> PrivacyLevelProperty = RegisterProperty<PrivacyLevelROC>(o => o.PrivacyLevel);
        public PrivacyLevelROC PrivacyLevel 
        {
            get => GetProperty(PrivacyLevelProperty); 
            private set => LoadProperty(PrivacyLevelProperty, value); 
        }        
        public static readonly PropertyInfo<MemberStatusROC> MemberStatusProperty = RegisterProperty<MemberStatusROC>(o => o.MemberStatus);
        public MemberStatusROC MemberStatus 
        {
            get => GetProperty(MemberStatusProperty); 
            private set => LoadProperty(MemberStatusProperty, value); 
        }        
        public static readonly PropertyInfo<MembershipTypeROC> MembershipTypeProperty = RegisterProperty<MembershipTypeROC>(o => o.MembershipType);
        public MembershipTypeROC MembershipType 
        {
            get => GetProperty(MembershipTypeProperty); 
            private set => LoadProperty(MembershipTypeProperty, value); 
        }        

        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(o => o.LastUpdatedBy);
        public string LastUpdatedBy 
        {
            get => GetProperty(LastUpdatedByProperty); 
            private set => LoadProperty(LastUpdatedByProperty, value); 
   
        }        

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty = RegisterProperty<SmartDate>(o => o.LastUpdatedDate);
        public SmartDate LastUpdatedDate 
        {
            get => GetProperty(LastUpdatedDateProperty); 
            private set => LoadProperty(LastUpdatedDateProperty, value); 
   
        }        

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(o => o.Notes);
        public string Notes 
        {
            get => GetProperty(NotesProperty); 
            private set => LoadProperty(NotesProperty, value); 
   
        }        

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); 
            private set => LoadProperty(RowVersionProperty, value); 
   
        }        
        #endregion 

        #region Factory Methods

        public static async Task<MemberInfoROC> GetMemberInfoROC(MemberInfo childData)
        {
            return await DataPortal.FetchChildAsync<MemberInfoROC>(childData);
        }

        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(MemberInfo childData)
        {
            Id = childData.Id;
            Person = (childData.Person != null ? await PersonROC.GetPersonROC( new Person() { Id = childData.Person.Id }) : null);
            MemberNumber = childData.MemberNumber;
            DateFirstJoined = childData.DateFirstJoined;
            PrivacyLevel = (childData.PrivacyLevel != null ? await PrivacyLevelROC.GetPrivacyLevelROC( new PrivacyLevel() { Id = childData.PrivacyLevel.Id }) : null);
            MemberStatus = (childData.MemberStatus != null ? await MemberStatusROC.GetMemberStatusROC( new MemberStatus() { Id = childData.MemberStatus.Id }) : null);
            MembershipType = (childData.MembershipType != null ? await MembershipTypeROC.GetMembershipTypeROC( new MembershipType() { Id = childData.MembershipType.Id }) : null);
            LastUpdatedBy = childData.LastUpdatedBy;
            LastUpdatedDate = childData.LastUpdatedDate;
            Notes = childData.Notes;
            RowVersion = childData.RowVersion;
        }

        #endregion
    }
}
