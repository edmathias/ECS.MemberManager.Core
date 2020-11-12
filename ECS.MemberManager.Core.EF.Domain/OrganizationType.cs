using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class OrganizationType
    {
        public int Id { get; private set; }
        public CategoryOfOrganization CategoryOfOrganization { get; set; }
        [Required, MaxLength(50)]
        public string TypeName { get; set; }
    }
}