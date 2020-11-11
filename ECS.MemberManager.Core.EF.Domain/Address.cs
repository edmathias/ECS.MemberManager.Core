using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ECS.BizBricks.CRM.Core.EF.Domain;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class Address
    {
        public int Id { get; private set; }
        [Required, MaxLength(35)]
        public string Address1 { get; set; }
        [MaxLength(35)]
        public string Address2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required,MaxLength(2)]
        public string State { get; set; }
        [Required,MaxLength(9)]
        public string PostCode { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public IList<Organization> Organizations { get; set; }
        public IList<Person> Persons { get; set; }
    }

}