using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class ImageROC : ReadOnlyBase<ImageROC>
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
            private set => LoadProperty(ImagePathProperty, value);
        }

        public static readonly PropertyInfo<byte[]> ImageFileProperty = RegisterProperty<byte[]>(o => o.ImageFile);

        public virtual byte[] ImageFile
        {
            get => GetProperty(ImageFileProperty);
            private set => LoadProperty(ImageFileProperty, value);
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);

        public virtual byte[] RowVersion
        {
            get => GetProperty(RowVersionProperty);
            private set => LoadProperty(RowVersionProperty, value);
        }

        #endregion

        #region Factory Methods

        internal static async Task<ImageROC> GetImageROC(Image childData)
        {
            return await DataPortal.FetchChildAsync<ImageROC>(childData);
        }

        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(Image data)
        {
            Id = data.Id;
            ImagePath = data.ImagePath;
            ImageFile = data.ImageFile;
            RowVersion = data.RowVersion;
        }

        #endregion
    }
}