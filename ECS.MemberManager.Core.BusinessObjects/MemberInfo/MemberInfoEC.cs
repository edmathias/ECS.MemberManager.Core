


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
    public partial class MemberInfoEC : BusinessBase<MemberInfoEC>
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

        public static readonly PropertyInfo<string> MemberNumberProperty = RegisterProperty<string>(o => o.MemberNumber);
        public virtual string MemberNumber 
        {
            get => GetProperty(MemberNumberProperty); 
            set => SetProperty(MemberNumberProperty, value); 
   
        } 
        public static readonly PropertyInfo<SmartDate> DateFirstJoinedProperty = RegisterProperty<SmartDate>(o => o.DateFirstJoined);
        public virtual SmartDate DateFirstJoined 
        {
            get => GetProperty(DateFirstJoinedProperty); 
            set => SetProperty(DateFirstJoinedProperty, value); 
   
        } 
        public static readonly PropertyInfo<PrivacyLevelEC> PrivacyLevelProperty = RegisterProperty<PrivacyLevelEC>(o => o.PrivacyLevel);
        public PrivacyLevelEC PrivacyLevel  
        {
            get => GetProperty(PrivacyLevelProperty); 
            set => SetProperty(PrivacyLevelProperty, value); 
        }        

        public static readonly PropertyInfo<MemberStatusEC> MemberStatusProperty = RegisterProperty<MemberStatusEC>(o => o.MemberStatus);
        public MemberStatusEC MemberStatus  
        {
            get => GetProperty(MemberStatusProperty); 
            set => SetProperty(MemberStatusProperty, value); 
        }        

        public static readonly PropertyInfo<MembershipTypeEC> MembershipTypeProperty = RegisterProperty<MembershipTypeEC>(o => o.MembershipType);
        public MembershipTypeEC MembershipType  
        {
            get => GetProperty(MembershipTypeProperty); 
            set => SetProperty(MembershipTypeProperty, value); 
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
        internal static async Task<MemberInfoEC> NewMemberInfoEC()
        {
            return await DataPortal.CreateChildAsync<MemberInfoEC>();
        }

        internal static async Task<MemberInfoEC> GetMemberInfoEC(MemberInfo childData)
        {
            return await DataPortal.FetchChildAsync<MemberInfoEC>(childData);
        }  


        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(MemberInfo data)
        {
            using (BypassPropertyChecks)
            {
                Id = data.Id;
                Person = (data.Person != null ? await PersonEC.GetPersonEC(data.Person) : null);
                MemberNumber = data.MemberNumber;
                DateFirstJoined = data.DateFirstJoined;
                PrivacyLevel = (data.PrivacyLevel != null ? await PrivacyLevelEC.GetPrivacyLevelEC(data.PrivacyLevel) : null);
                MemberStatus = (data.MemberStatus != null ? await MemberStatusEC.GetMemberStatusEC(data.MemberStatus) : null);
                MembershipType = (data.MembershipType != null ? await MembershipTypeEC.GetMembershipTypeEC(data.MembershipType) : null);
                LastUpdatedBy = data.LastUpdatedBy;
                LastUpdatedDate = data.LastUpdatedDate;
                Notes = data.Notes;
                RowVersion = data.RowVersion;
            }
        }

        [InsertChild]
        private async Task Insert()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMemberInfoDal>();
            var data = new MemberInfo()
            {

                Id = Id,
                Person = (Person != null ? new Person() { Id = Person.Id } : null),
                MemberNumber = MemberNumber,
                DateFirstJoined = DateFirstJoined,
                PrivacyLevel = (PrivacyLevel != null ? new PrivacyLevel() { Id = PrivacyLevel.Id } : null),
                MemberStatus = (MemberStatus != null ? new MemberStatus() { Id = MemberStatus.Id } : null),
                MembershipType = (MembershipType != null ? new MembershipType() { Id = MembershipType.Id } : null),
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
        private async Task Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMemberInfoDal>();
            var data = new MemberInfo()
            {

                Id = Id,
                Person = (Person != null ? new Person() { Id = Person.Id } : null),
                MemberNumber = MemberNumber,
                DateFirstJoined = DateFirstJoined,
                PrivacyLevel = (PrivacyLevel != null ? new PrivacyLevel() { Id = PrivacyLevel.Id } : null),
                MemberStatus = (MemberStatus != null ? new MemberStatus() { Id = MemberStatus.Id } : null),
                MembershipType = (MembershipType != null ? new MembershipType() { Id = MembershipType.Id } : null),
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Update(data);
            Id = insertedObj.Id;
            RowVersion = insertedObj.RowVersion;
        }

       
        [DeleteSelfChild]
        private async Task DeleteSelf()
        {
            await Delete(Id);
        }
       
        [Delete]
        private async Task Delete(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IMemberInfoDal>();
           
            await dal.Delete(id);
        }

        #endregion
    }
}
