using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class TaskForEvent
    {
        public int Id { get; private set; }
        [Required] public Event Event { get; set; }
        [Required, MaxLength(50)] public string TaskName { get; set; }
        public DateTime PlannedDate { get; set; }
        public DateTime ActualDate { get; set; }
        public string Information { get; set; }
        [MaxLength(255)]
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string Notes { get; set; }
    }
}