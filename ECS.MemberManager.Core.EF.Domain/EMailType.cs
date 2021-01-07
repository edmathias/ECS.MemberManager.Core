using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECS.MemberManager.Core.EF.Domain
{
    [Serializable]
    [Table("EMailTypes")]
    public class EMailType : EntityBase
    {
        [Required, MaxLength(50)]
        public string Description { get; set; }
        public string Notes { get; set; } 
    }
}