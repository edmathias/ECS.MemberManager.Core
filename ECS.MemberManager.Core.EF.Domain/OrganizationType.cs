using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class OrganizationType
    {
        public int Id { get; private set; }
        [Required]
        public CategoryOfOrganization OrganizationCategory { get; set; }
        [Required,MaxLength(50)]
        public string TypeName { get; set; }
        public int DisplayOrder { get; set; }
    }
}