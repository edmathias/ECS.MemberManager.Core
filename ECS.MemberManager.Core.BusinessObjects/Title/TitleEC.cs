

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
    public partial class TitleEC : BusinessBase<TitleEC>
    {
        #region Business Methods
 
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public virtual int Id 
        {
            get => GetProperty(IdProperty); 
            private set => LoadProperty(IdProperty, value);    
        }

        public static readonly PropertyInfo<string> AbbreviationProperty = RegisterProperty<string>(o => o.Abbreviation);
        public virtual string Abbreviation 
        {
            get => GetProperty(AbbreviationProperty); 
            set => SetProperty(AbbreviationProperty, value); 
   
        }

        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(o => o.Description);
        public virtual string Description 
        {
            get => GetProperty(DescriptionProperty); 
            set => SetProperty(DescriptionProperty, value); 
   
        }

        public static readonly PropertyInfo<int> DisplayOrderProperty = RegisterProperty<int>(o => o.DisplayOrder);
        public virtual int DisplayOrder 
        {
            get => GetProperty(DisplayOrderProperty); 
            set => SetProperty(DisplayOrderProperty, value); 
   
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); 
            set => SetProperty(RowVersionProperty, value); 
   
        }

        #endregion 

        #region Factory Methods
        internal static async Task<TitleEC> NewTitleEC()
        {
            return await DataPortal.CreateChildAsync<TitleEC>();
        }

        internal static async Task<TitleEC> GetTitleEC(Title childData)
        {
            return await DataPortal.FetchChildAsync<TitleEC>(childData);
        }  


        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(Title data)
        {
            using(BypassPropertyChecks)
            {
                Id = data.Id;
                Abbreviation = data.Abbreviation;
                Description = data.Description;
                DisplayOrder = data.DisplayOrder;
                RowVersion = data.RowVersion;
            }            
        }
        [InsertChild]
        private async Task Insert()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITitleDal>();
            var data = new Title()
            {

                Id = Id,
                Abbreviation = Abbreviation,
                Description = Description,
                DisplayOrder = DisplayOrder,
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
            var dal = dalManager.GetProvider<ITitleDal>();
            var data = new Title()
            {

                Id = Id,
                Abbreviation = Abbreviation,
                Description = Description,
                DisplayOrder = DisplayOrder,
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
            var dal = dalManager.GetProvider<ITitleDal>();
           
            await dal.Delete(id);
        }

        #endregion
    }
}
