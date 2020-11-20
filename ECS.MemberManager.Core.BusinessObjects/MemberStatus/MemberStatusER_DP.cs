using System;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects.MemberStatus
{
    public partial class MemberStatusER
    {

        private void DataPortal_Fetch(int id)
        {
            using (var dalManager = DalFactory.GetManager())
            {
                var dal = dalManager.GetProvider<IMemberStatusDal>();
                var data = dal.Fetch(id);
                using (BypassPropertyChecks)
                {
                    Id = data.Id;
                    Description = data.Description;
                    Notes = data.Notes;
                }
            }
        }
        

    }
}