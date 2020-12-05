using System;
using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class ContactForSponsor : EntityBase
    {
        [Required]
        public Sponsor Sponsor { get; set; }
        public DateTime DateWhenContacted { get; set; }
        [MaxLength(255)]
        public string Purpose { get; set; }
        public string RecordOfDiscussion { get; set; }
        [Required]
        public Person Person { get; set; }
        public string Notes { get; set; }
        [MaxLength(255)]
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}