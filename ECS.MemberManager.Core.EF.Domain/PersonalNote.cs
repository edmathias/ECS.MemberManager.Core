using System;
using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class PersonalNote : EntityBase
    {
        [Required] public Person Person { get; set; }
        [MaxLength(50)] public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DateEnd { get; set; }
        [MaxLength(255)] public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string Note { get; set; }
    }
}