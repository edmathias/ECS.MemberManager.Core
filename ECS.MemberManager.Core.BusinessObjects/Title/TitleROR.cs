


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
    public partial class TitleROR : BusinessBase<TitleROR>
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
            private set => LoadProperty(AbbreviationProperty, value);    
        }

        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(o => o.Description);
        public virtual string Description 
        {
            get => GetProperty(DescriptionProperty); 
            private set => LoadProperty(DescriptionProperty, value);    
        }

        public static readonly PropertyInfo<int> DisplayOrderProperty = RegisterProperty<int>(o => o.DisplayOrder);
        public virtual int DisplayOrder 
        {
            get => GetProperty(DisplayOrderProperty); 
            private set => LoadProperty(DisplayOrderProperty, value);    
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); 
            private set => LoadProperty(RowVersionProperty, value);    
        }

        #endregion 

        #region Factory Methods
        public static async Task<TitleROR> GetTitleROR(int id)
        {
            return await DataPortal.FetchAsync<TitleROR>(id);
        }  


        #endregion

        #region Data Access Methods

        [Fetch]
        private async Task Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITitleDal>();
            var data = await dal.Fetch(id);
                Id = data.Id;
                Abbreviation = data.Abbreviation;
                Description = data.Description;
                DisplayOrder = data.DisplayOrder;
                RowVersion = data.RowVersion;
        }

        #endregion
    }
}
