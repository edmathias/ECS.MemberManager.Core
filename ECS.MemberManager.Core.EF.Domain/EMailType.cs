using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class EMailType : EntityBase
    {
        [Required, MaxLength(50)]
        public string Description { get; set; }
        public string Notes { get; set; } 
    }
}