using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using ECS.BizBricks.CRM.Core.EF.Domain;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class Organization
    {
        public int Id { get; private set; }
        [Required,MaxLength(255)]
        public string Name { get; set; }
        [Required]
        public OrganizationType OrganizationType { get; set; }
        [Required]
        public DateTime DateOfFirstContact { get; set; }
        [MaxLength(255)] public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        [MaxLength(255)]
        public string Notes { get; set; }
        
        public IList<Address> Addresses { get; set;  }
        public IList<CategoryOfOrganization> CategoryOfOrganizations { get; set; }
        public IList<Phone> Phones { get; set; }
        public IList<EMail> EMails { get; set; }
    }
}