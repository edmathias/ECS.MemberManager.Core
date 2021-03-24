using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class Title : EntityBase
    {
        [Required, MaxLength(10)] public string Abbreviation { get; set; }
        [MaxLength(50)] public string Description { get; set; }
        public int DisplayOrder { get; set; }
    }
}