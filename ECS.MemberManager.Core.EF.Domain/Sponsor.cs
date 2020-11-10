using System;
using System.ComponentModel.DataAnnotations;
using ECS.BizBricks.CRM.Core.EF.Domain;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class Sponsor
    {
        public int Id { get; private set; }
        [Required]
        public Person Person { get; set; }
        [Required]
        public Organization Organization { get; set; }
        public string Status { get; set; }
        public DateTime DateOfFirstContact { get; set; }
        public string ReferredBy { get; set; }
        public DateTime DateSponsorAccepted { get; set; }
        public string Type { get; set; }
        public string Details { get; set; }
        public DateTime SponsorUntilDate { get; set; }
        [Timestamp] public byte[] RowVersion { get; private set; }
    }
}