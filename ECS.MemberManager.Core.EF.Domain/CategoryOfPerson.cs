using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECS.BizBricks.CRM.Core.EF.Domain
{
    public class CategoryOfPerson
    {
        [Key] public int Id { get; set; }
        [Required,MaxLength(50)] public string Category { get; set; }
        public int DisplayOrder { get; set; }
        public IList<Person> Persons { get; set; }
    }
}