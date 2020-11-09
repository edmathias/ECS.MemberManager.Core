using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class MaritalStatus
    {
        [Key]
        public int Id { get; private set; }

        [Required,MaxLength(50)]
        public string Status { get; set; }

        public int DisplayOrder { get; set; }

    }
}