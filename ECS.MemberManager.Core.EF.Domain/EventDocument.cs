using System;
using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class EventDocument
    {
        public int Id { get; private set; }
        [Required]
        public Event Event { get; set; }
        [Required, MaxLength(50)]
        public string DocumentName { get; set; }
        public DocumentType DocumentType { get; set; }
        [MaxLength(255)]
        public string PathAndFileName { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string Notes { get; set; }
    }
}