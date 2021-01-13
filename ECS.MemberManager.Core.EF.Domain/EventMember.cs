using System;
using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class EventMember : EntityBase
    {
        [Required]
        public int MemberInfoId { get; set; }
        [Required]
        public int EventId { get; set; }
        [MaxLength(50)]
        public string RoleId { get; set; }
        [MaxLength(255)] public string LastUpdatedBy { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Notes { get; set; }
    }
}