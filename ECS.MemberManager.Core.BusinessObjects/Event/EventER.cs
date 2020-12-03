using System;
using System.ComponentModel;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class EventER : BusinessBase<EventER>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        //TODO: rest of properties for Event
        
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

        public static EventER NewEventER()
        {
            return DataPortal.Create<EventER>();
        }

        public static EventER GetEventER(int id)
        {
            return DataPortal.Fetch<EventER>(id);
        }

        public static void DeleteEventER(int id)
        {
            DataPortal.Delete<EventER>(id);
        }

        #endregion

        #region Data Access

        [RunLocal]
        [Create]
        private void Create()
        {
            // omit if no defaults to set

            base.DataPortal_Create();
        }

        [Fetch] 
        private void Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDal>();
            var data = dal.Fetch(id);
            using (BypassPropertyChecks)
            {
                Id = data.Id;

            }
        }

        [Insert]
        private void Insert()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDal>();
            using (BypassPropertyChecks)
            {
                // format and store dto 

            }
        }

        [Update]
        private void Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDal>();
            using (BypassPropertyChecks)
            {
                // format dto and update dal 
            }
        }

        [DeleteSelf]
        private void DeleteSelf()
        {
            Delete(this.Id);
        }

        [Delete]
        private void Delete(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEventDal>();

            dal.Delete(id);
        }



        #endregion


    }
}