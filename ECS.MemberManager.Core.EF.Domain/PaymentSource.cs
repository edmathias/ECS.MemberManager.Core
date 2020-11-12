using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class PaymentSource
    {
        public int Id { get; private set; }
        [Required,MaxLength(50)]
        public string Description { get; set; }
        public string Notes { get; set; }
    }
}