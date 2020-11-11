using System;
using System.ComponentModel.DataAnnotations;
using ECS.BizBricks.CRM.Core.EF.Domain;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class Sponsor
    {
        public int Id { get; private set; }
        public Person Person { get; set; }
        public Organization Organization { get; set; }
        public string Status { get; set; }
        public DateTime DateOfFirstContact { get; set; }
        public string ReferredBy { get; set; }
        public DateTime DateSponsorAccepted { get; set; }
        public string TypeName { get; set; }
        public string Details { get; set; }
        public DateTime SponsorUntilDate { get; set; }
        public string Notes { get; set; }
    }
}