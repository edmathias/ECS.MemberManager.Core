using System;
using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class EventMember
    {
        public int Id { get; set; }
        [Required]
        public MemberInfo MemberInfo { get; set; }
        [Required]
        public Event Event { get; set; }
        [MaxLength(50)]
        public string Role { get; set; }
        [MaxLength(255)] public string LastUpdatedBy { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Notes { get; set; }
    }
}