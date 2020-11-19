using System;
using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class Office
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public int Term { get; set; }
        [MaxLength(25)]
        public string CalendarPeriod { get; set; }
        public int ChosenHow { get; set; }
        [MaxLength(50)]
        public string Appointer { get; set; }
        [MaxLength(255)] public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string Notes { get; set; }
    }
}