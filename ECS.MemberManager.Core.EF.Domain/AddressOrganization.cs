using System.ComponentModel.DataAnnotations.Schema;
using ECS.BizBricks.CRM.Core.EF.Domain;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class AddressOrganization
    {
        public int AddressId { get; set; }
        public Address Address { get; set; }

        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
    }
}