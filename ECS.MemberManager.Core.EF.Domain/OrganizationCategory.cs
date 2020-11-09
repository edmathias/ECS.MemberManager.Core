using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class OrganizationCategory
    {
        [Key] public int Id { get; private set; }
        [Required,MaxLength(35)] public string TypeName { get; set; }
        public int DisplayOrder { get; set; }
    }
}