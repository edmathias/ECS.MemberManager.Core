using System;
using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class Office : EntityBase
    {
        [Required, MaxLength(50)] public string Name { get; set; }
        public int Term { get; set; }
        [MaxLength(25)] public string CalendarPeriod { get; set; }
        public int ChosenHow { get; set; }
        [MaxLength(50)] public string Appointer { get; set; }
        [Required, MaxLength(255)] public string LastUpdatedBy { get; set; }
        [Required] public DateTime LastUpdatedDate { get; set; }
        public string Notes { get; set; }
    }
}