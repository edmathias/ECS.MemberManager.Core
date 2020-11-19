using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects.Title
{
    public partial class TitleRO 
    {
        private void DataPortal_Fetch(int id)
        {
            using (var dalManager = DalFactory.GetManager())
            {
                var dal = dalManager.GetProvider<ITitleDal>();
                var data = dal.Fetch(id);
                
                InitializeTitleROFromTitleDto(data);

            }
        }

         private void InitializeTitleROFromTitleDto(ECS.MemberManager.Core.EF.Domain.Title title)
         {
             this.Id = title.Id;
             this.Abbreviation = title.Abbreviation;
             this.Description = title.Description;
             this.DisplayOrder = title.DisplayOrder;

         }
    }
}