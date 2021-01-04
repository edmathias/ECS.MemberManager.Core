using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class EMailTypeEC : BusinessBase<EMailTypeEC>
    {
        #region Business Methods
        
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get => GetProperty(IdProperty);
            set => SetProperty(IdProperty, value);
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
        

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();

            // TODO: add business rules
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }
 
        
        #endregion
        
        #region Factory Methods

        internal static async Task<EMailTypeER> NewEMailType()
        {
            return await DataPortal.CreateChildAsync<EMailTypeER>();
        }

        internal static async Task<EMailTypeEC> GetEMailType(EMailType childData)
        {
            return await DataPortal.FetchChildAsync<EMailTypeEC>(childData);
        }

        internal static async Task DeleteEMailType(int id)
        {
            await DataPortal.DeleteAsync<EMailTypeER>(id);
        }
        
        #endregion
        
        #region Data Access Methods
        
        [FetchChild]       
        private void Fetch(EMailType childData)
        {
            using (BypassPropertyChecks)
            {
                Id = childData.Id;
                Description = childData.Description;
                Notes = childData.Notes;
            }
        }

        [InsertChild]
        private void Insert()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailTypeDal>();
            using (BypassPropertyChecks)
            {
                var eMailType = new EMailType 
                    { 
                        Description = this.Description, 
                        Notes = this.Notes 
                    };
                eMailType = dal.Insert(eMailType);
                Id = eMailType.Id;
            }
        }
        
        [UpdateChild]
        private void Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailTypeDal>();
            using (BypassPropertyChecks)
            {
                var eMailType = new EMailType
                {
                    Id = this.Id, 
                    Description = this.Description, 
                    Notes = this.Notes
                };
                
                dal.Update(eMailType);
            }
        }

        [DeleteSelfChild]
        private void DeleteSelf()
        {
            Delete(this.Id);
        }
       
        [Delete]
        private void Delete(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailTypeDal>();
 
            dal.Delete(id);
        }
        
        #endregion
    }
}