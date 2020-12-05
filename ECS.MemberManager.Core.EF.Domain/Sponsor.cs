using System;
using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class Sponsor : EntityBase
    {
        public Person Person { get; set; }
        public Organization Organization { get; set; }
        [MaxLength(50)]
        public string Status { get; set; }
        public DateTime DateOfFirstContact { get; set; }
        [MaxLength(255)]
        public string ReferredBy { get; set; }
        public DateTime DateSponsorAccepted { get; set; }
        public string TypeName { get; set; }
        public string Details { get; set; }
        public DateTime SponsorUntilDate { get; set; }
        public string Notes { get; set; }
        [MaxLength(255)] public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}