using System;
using System.ComponentModel.DataAnnotations;
using ECS.BizBricks.CRM.Core.EF.Domain;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class MemberInfo
    {
        public int Id { get; set; }
        [Required]
        public Person Person { get; set; }
        [MaxLength(35)]
        public string MemberNumber { get; set; }
        [Required]
        public DateTime DateFirstJoined { get; set; }
        public PrivacyLevel Privacy { get; set; }
        [Required]
        public MemberStatus MemberStatus { get; set; }
        [Required]
        public MembershipType MembershipType { get; set; }
        [MaxLength(255)] public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string Notes { get; set; }

    }
}