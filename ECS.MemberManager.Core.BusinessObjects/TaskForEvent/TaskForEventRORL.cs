


using System; 
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class TaskForEventRORL : ReadOnlyListBase<TaskForEventRORL,TaskForEventROC>
    {
        #region Factory Methods

        public static async Task<TaskForEventRORL> NewTaskForEventRORL()
        {
            return await DataPortal.CreateAsync<TaskForEventRORL>();
        }

        public static async Task<TaskForEventRORL> GetTaskForEventRORL( )
        {
            return await DataPortal.FetchAsync<TaskForEventRORL>();
        }

        #endregion

        #region Data Access
 
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITaskForEventDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await TaskForEventROC.GetTaskForEventROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion

     }
}
