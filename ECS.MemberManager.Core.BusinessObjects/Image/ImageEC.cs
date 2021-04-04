

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
    public partial class ImageEC : BusinessBase<ImageEC>
    {
        #region Business Methods
 
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public virtual int Id 
        {
            get => GetProperty(IdProperty); 
            private set => LoadProperty(IdProperty, value);    
        }

        public static readonly PropertyInfo<string> ImagePathProperty = RegisterProperty<string>(o => o.ImagePath);
        public virtual string ImagePath 
        {
            get => GetProperty(ImagePathProperty); 
            set => SetProperty(ImagePathProperty, value); 
   
        }

        public static readonly PropertyInfo<byte[]> ImageFileProperty = RegisterProperty<byte[]>(o => o.ImageFile);
        public virtual byte[] ImageFile 
        {
            get => GetProperty(ImageFileProperty); 
            set => SetProperty(ImageFileProperty, value); 
   
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); 
            set => SetProperty(RowVersionProperty, value); 
   
        }

        #endregion 

        #region Factory Methods
        internal static async Task<ImageEC> NewImageEC()
        {
            return await DataPortal.CreateChildAsync<ImageEC>();
        }

        internal static async Task<ImageEC> GetImageEC(Image childData)
        {
            return await DataPortal.FetchChildAsync<ImageEC>(childData);
        }  


        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(Image data)
        {
            using(BypassPropertyChecks)
            {
            Id = data.Id;
            ImagePath = data.ImagePath;
            ImageFile = data.ImageFile;
            RowVersion = data.RowVersion;
            }            
        }
        [InsertChild]
        private async Task Insert([Inject] IDal<Image> dal)
        {
            FieldManager.UpdateChildren();

            var data = new Image()
            {

                Id = Id,
                ImagePath = ImagePath,
                ImageFile = ImageFile,
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Insert(data);
            Id = insertedObj.Id;
            RowVersion = insertedObj.RowVersion;
        }

       [UpdateChild]
        private async Task Update([Inject] IDal<Image> dal)
        {
            FieldManager.UpdateChildren();

            var data = new Image()
            {

                Id = Id,
                ImagePath = ImagePath,
                ImageFile = ImageFile,
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Update(data);
            RowVersion = insertedObj.RowVersion;
        }

       
        [DeleteSelfChild]
        private async Task DeleteSelf([Inject] IDal<Image> dal)
        {
            await Delete(Id,dal);
        }
       
        [Delete]
        private async Task Delete(int id, [Inject] IDal<Image> dal)
        {
            await dal.Delete(id);
        }

        #endregion
    }
}
