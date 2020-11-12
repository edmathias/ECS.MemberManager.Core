using System;
using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class PaymentType
    {
        public int Id { get; private set; }
        [Required]
        public string TypeDescription { get; set; }
        public string Notes { get; set; }
    }
}