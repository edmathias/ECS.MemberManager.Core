using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Timestamp] public byte[] RowVersion { get; set; }

        public IList<AddressOrganization> AddressOrganizations { get;} = new List<AddressOrganization>();
        public IList<AddressPerson> AddressPersons { get; } = new List<AddressPerson>();
    }

}