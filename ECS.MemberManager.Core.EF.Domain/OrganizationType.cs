using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class OrganizationType : EntityBase
    {
        [Required] 
        public int CategoryOfOrganizationId { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(255)] public string Notes { get; set; }
    }
}