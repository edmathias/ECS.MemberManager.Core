using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    [Serializable]
    public class Address : EntityBase
    {
        [Required, MaxLength(35)]
        public string Address1 { get; set; }
        [MaxLength(35)]
        public string Address2 { get; set; }
        [Required,MaxLength(50)]
        public string City { get; set; }
        [Required,MaxLength(2)]
        public string State { get; set; }
        [Required,MaxLength(9)]
        public string PostCode { get; set; }
        public string Notes { get; set; }
        [MaxLength(255)]
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public IList<Organization> Organizations { get; set; } = new List<Organization>();
        public IList<Person> Persons { get; set; } = new List<Person>();
    }

}