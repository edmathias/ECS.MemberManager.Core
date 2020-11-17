using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using Microsoft.SqlServer.Server;

namespace ECS.MemberManager.Core.BusinessObjects.Titles
{
    public partial class TitleRO : Csla.ReadOnlyBase<TitleRO>
    {
        
        private void DataPortal_Fetch(int id)
        {
            using (var dalManager = DalFactory.GetManager())
            {
                var dal = dalManager.GetProvider<ITitleDal>();
                var data = dal.Fetch(id);
                
            }
        }
    }
}