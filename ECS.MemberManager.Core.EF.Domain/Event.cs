using System;
using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class Event
    {
        public int Id { get; private set; }
        [Required,MaxLength(255)]
        public string EventName { get; set; }
        public string Description { get; set; }
        public bool IsOneTime { get; set; }
        public DateTime NextDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string Notes { get; set; }
    }
}