using System;
using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class Payment : EntityBase
    {
        [Required] public Person Person { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime PaymentExpirationDate { get; set; }
        [Required] public PaymentSource PaymentSource { get; set; }
        [Required] public PaymentType PaymentType { get; set; }
        [MaxLength(255)] public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string Notes { get; set; }
    }
}