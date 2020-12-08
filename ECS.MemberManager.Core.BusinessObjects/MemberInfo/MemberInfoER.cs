using System;
using System.ComponentModel;
using Csla;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class MemberInfoER : BusinessBase<MemberInfoER>
    {
        #region Business Methods

        // TODO: add public properties and methods


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

        #endregion

        #region Data Access

        #endregion


        
    }
}