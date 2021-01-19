using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECS.MemberManager.Core.EF.Domain
{
    [Table("MemberStatuses")]
    public class MemberStatus : EntityBase
    {
        [Required, MaxLength(50)] public string Description { get; set; }
        public string Notes { get; set; }
    }
}