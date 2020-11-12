using System;
using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class DocumentType
    {
        public int Id { get; private set; }
        [Required,MaxLength(50)]
        public string TypeDescription { get; set; }
        public DateTime LastUpdatedBy { get; set; }
        public string Notes { get; set; }
    }
}