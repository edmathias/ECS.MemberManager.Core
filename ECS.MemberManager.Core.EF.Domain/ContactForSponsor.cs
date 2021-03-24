using System;
using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class ContactForSponsor : EntityBase
    {
        public DateTime DateWhenContacted { get; set; }
        [Required, MaxLength(255)] public string Purpose { get; set; }
        public string RecordOfDiscussion { get; set; }
        public string Notes { get; set; }
        [MaxLength(255)] public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        public Sponsor Sponsor { get; set; }

        public Person Person { get; set; }
    }
}