using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class Organization : EntityBase
    {
        [Required,MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public DateTime DateOfFirstContact { get; set; }
        [MaxLength(255)] public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string Notes { get; set; }
        
        public IList<Address> Addresses { get; set;  }
        public OrganizationType OrganizationType { get; set; }

        public IList<Phone> Phones { get; set; }
        public IList<EMail> EMails { get; set; }
    }
}