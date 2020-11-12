using System;
using System.ComponentModel.DataAnnotations;
using ECS.BizBricks.CRM.Core.EF.Domain;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class ContactForSponsor
    {
        public int Id { get; private set; }
        public Sponsor Sponsor { get; set; }
        [Required]
        public DateTime DateWhenContacted { get; set; }
        [Required, MaxLength(255)]
        public string Purpose { get; set; }
        public string RecordOfDiscussion { get; set; }
        [Required]
        public Person Person { get; set; }
        public string Notes { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}