using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class MembershipType
    {
        public int Id { get; set; }
        [Required, MaxLength(255)]
        public string Description { get; set; }
        public int Level { get; set; }
        public string Notes { get; set; }
    }
}