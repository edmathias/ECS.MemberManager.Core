using System;
using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class DocumentType
    {
        public int Id { get; private set; }
        [Required,MaxLength(50)]
        public string TypeDescription { get; set; }
        [MaxLength(255)]
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string Notes { get; set; }
    }
}