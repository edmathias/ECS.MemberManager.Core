


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
    public partial class EventDocumentROR : BusinessBase<EventDocumentROR>
    {
        #region Business Methods 
         public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public virtual int Id 
        {
            get => GetProperty(IdProperty); //1-2
            private set => LoadProperty(IdProperty, value); //2-3   
        }


        public static readonly PropertyInfo<EventROC> EventProperty = RegisterProperty<EventROC>(o => o.Event);
        public EventROC Event  
        {
            get => GetProperty(EventProperty); //1-1
        
            private set => LoadProperty(EventProperty, value); //2-1
        }    
 
        public static readonly PropertyInfo<string> DocumentNameProperty = RegisterProperty<string>(o => o.DocumentName);
        public virtual string DocumentName 
        {
            get => GetProperty(DocumentNameProperty); //1-2
            private set => LoadProperty(DocumentNameProperty, value); //2-3   
        }


        public static readonly PropertyInfo<DocumentTypeROC> DocumentTypeProperty = RegisterProperty<DocumentTypeROC>(o => o.DocumentType);
        public DocumentTypeROC DocumentType  
        {
            get => GetProperty(DocumentTypeProperty); //1-1
        
            private set => LoadProperty(DocumentTypeProperty, value); //2-1
        }    
 
        public static readonly PropertyInfo<string> PathAndFileNameProperty = RegisterProperty<string>(o => o.PathAndFileName);
        public virtual string PathAndFileName 
        {
            get => GetProperty(PathAndFileNameProperty); //1-2
            private set => LoadProperty(PathAndFileNameProperty, value); //2-3   
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
        public static async Task<EventDocumentROR> GetEventDocumentROR(int id)
        {
            return await DataPortal.FetchAsync<EventDocumentROR>(id);
        }  


        #endregion

        #region Data Access Methods

        [Fetch]
        private async Task Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDocumentDal>();
            var data = await dal.Fetch(id);
                Id = data.Id;
                Event = (data.Event != null ? await EventROC.GetEventROC(data.Event) : null);
                DocumentName = data.DocumentName;
                DocumentType = (data.DocumentType != null ? await DocumentTypeROC.GetDocumentTypeROC(data.DocumentType) : null);
                PathAndFileName = data.PathAndFileName;
                LastUpdatedBy = data.LastUpdatedBy;
                LastUpdatedDate = data.LastUpdatedDate;
                Notes = data.Notes;
                RowVersion = data.RowVersion;
        }

        #endregion
    }
}
