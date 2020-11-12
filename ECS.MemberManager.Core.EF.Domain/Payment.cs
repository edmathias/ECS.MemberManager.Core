using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class Payment
    {
        public int Id { get; private set; }
        [Required]
        public MemberInfo MemberInfo { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }
        [Required]
        public DateTime PaymentExpirationDate { get; set; }
        [Required]
        public PaymentSource PaymentSource { get; set; }
        [Required]
        public PaymentType PaymentType { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string Notes { get; set; }
    }
}