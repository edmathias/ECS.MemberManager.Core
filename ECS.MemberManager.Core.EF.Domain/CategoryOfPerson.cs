using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.BizBricks.CRM.Core.EF.Domain
{
    public class CategoryOfPerson
    {
        [Key] public int Id { get; private set; }
        [Required,MaxLength(50)] public string Category { get; set; }
        public int DisplayOrder { get; set; }
        public IList<Person> Persons { get; set; }
    }
}