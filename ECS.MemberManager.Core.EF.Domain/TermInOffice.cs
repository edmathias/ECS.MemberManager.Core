using System;
using System.ComponentModel.DataAnnotations;
using ECS.BizBricks.CRM.Core.EF.Domain;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class TermInOffice
    {
        public int Id { get; set; }
        public Person Person { get; set; }
        public Office Office { get; set; }
        public DateTime StartDate { get; set; }
        [MaxLength(255)] public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string Notes { get; set; }
    }
}