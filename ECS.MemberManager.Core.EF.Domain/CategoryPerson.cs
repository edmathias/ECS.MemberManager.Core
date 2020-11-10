using ECS.BizBricks.CRM.Core.EF.Domain;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class CategoryPerson
    {
        public int CategoryOfPersonId { get; set; }
        public CategoryOfPerson CategoryOfPerson { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}