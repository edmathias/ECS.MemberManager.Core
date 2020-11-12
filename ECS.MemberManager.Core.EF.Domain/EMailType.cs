using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class EMailType
    {
        public int Id { get; private set; }
        [Required, MaxLength(255)]
        public string TypeDescription { get; set; }
        public string Notes { get; set; } 
    }
}