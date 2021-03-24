﻿using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class TitleERL : BusinessListBase<TitleERL, TitleEC>
    {
        #region Factory Methods

        public static async Task<TitleERL> NewTitleERL()
        {
            return await DataPortal.CreateAsync<TitleERL>();
        }

        public static async Task<TitleERL> GetTitleERL()
        {
            return await DataPortal.FetchAsync<TitleERL>();
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task Fetch([Inject] ITitleDal dal)
        {
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await TitleEC.GetTitleEC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        [Update]
        private void Update()
        {
            Child_Update();
        }

        #endregion
    }
}