using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class OrganizationType : EntityBase
    {
        [Required, MaxLength(50)] public string Name { get; set; }
        [MaxLength(255)] public string Notes { get; set; }

        public CategoryOfOrganization CategoryOfOrganization { get; set; }
    }
}