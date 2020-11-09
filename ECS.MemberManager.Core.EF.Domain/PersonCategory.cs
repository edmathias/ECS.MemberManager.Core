using System.ComponentModel.DataAnnotations;

namespace ECS.BizBricks.CRM.Core.EF.Domain
{
    public class PersonCategory
    {
        [Key] public int Id { get; private set; }
        [Required,MaxLength(50)] public string Category { get; set; }
        public int DisplayOrder { get; set; }
    }
}