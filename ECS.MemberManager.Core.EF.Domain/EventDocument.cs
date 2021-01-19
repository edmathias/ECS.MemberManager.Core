using System;
using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class EventDocument : EntityBase
    {
        [Required]
        public int EventId { get; set; }
        [MaxLength(50)]
        public string DocumentName { get; set; }
        public int DocumentTypeId { get; set; }
        [MaxLength(255)]
        public string PathAndFileName { get; set; }
        [MaxLength(255)] public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string Notes { get; set; }
    }
}