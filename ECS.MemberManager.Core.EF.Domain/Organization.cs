using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ECS.BizBricks.CRM.Core.EF.Domain;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class Organization
    {
        public int Id { get; private set; }
        [Required,MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public DateTime DateOfFirstContact { get; set; }
        [Required]
        public int LastUpdatedBy { get; set; }
        [Required]
        public DateTime LastUpdatedDate { get; set; }
        public string Notes { get; set; }
        
        public IList<Address> Addresses { get; set;  }
        public IList<CategoryOfOrganization> CategoryOfOrganizations { get; set; }
        public IList<Phone> Phones { get; set; }
    }
}