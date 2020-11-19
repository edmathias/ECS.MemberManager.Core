using System;
using System.ComponentModel.DataAnnotations;
using ECS.BizBricks.CRM.Core.EF.Domain;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class PersonalNote
    {
        public int Id { get; set; }
        [Required]
        public Person Person { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DateEnd { get; set; }
        [MaxLength(255)] public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string Note { get; set; }
    }
}