using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class Event : EntityBase
    {
        [Required,MaxLength(255)]
        public string EventName { get; set; }
        public string Description { get; set; }
        public bool IsOneTime { get; set; }
        public DateTime NextDate { get; set; }
        [Required,MaxLength(255)] public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string Notes { get; set; }

        public IList<Person> Persons { get; set; }
    }
}