using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class MemberStatus
    {
        public int Id { get; set; }
        [Required, MaxLength(255)] public string Description { get; set; }
        public string Notes { get; set; }
    }
}