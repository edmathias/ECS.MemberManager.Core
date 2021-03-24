using System;
using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class MembershipType : EntityBase
    {
        [Required, MaxLength(50)] public string Description { get; set; }
        public int Level { get; set; }
        [MaxLength(255)] public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string Notes { get; set; }
    }
}