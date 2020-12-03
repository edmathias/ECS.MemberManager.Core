using System;
using System.ComponentModel.DataAnnotations;
using ECS.BizBricks.CRM.Core.EF.Domain;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class Payment
    {
        public int Id { get; set; }
        [Required]
        public Person Person { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime PaymentExpirationDate { get; set; }
        [Required]
        public PaymentSource PaymentSource { get; set; }
        [Required]
        public PaymentType PaymentType { get; set; }
        [MaxLength(255)]
        public int LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string Notes { get; set; }
    }
}