using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class Organization
    {
        public Organization()
        {
            AddressOrganizations = new List<AddressOrganization>();
        }
        public int Id { get; private set; }
        [Required,MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public OrganizationType OrganizationType { get; set; }
        [Required]
        public DateTime DateOfFirstContact { get; set; }
        [Required]
        public int LastUpdatedBy { get; set; }
        [Required]
        public DateTime LastUpdatedDate { get; set; }
        public string Notes { get; set; }
        [Timestamp] public byte[] RowVersion { get; private set; }

        public IList<AddressOrganization> AddressOrganizations { get; set; }
    }
}