using System;
using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class EventMember : EntityBase
    {
        [Required] public MemberInfo MemberInfo { get; set; }
        [Required] public Event Event { get; set; }

        [MaxLength(50)] public string Role { get; set; }
        [Required, MaxLength(255)] public string LastUpdatedBy { get; set; }
        [Required] public DateTime LastUpdatedDate { get; set; }
        public string Notes { get; set; }
    }
}