using ECS.BizBricks.CRM.Core.EF.Domain;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class AddressPerson
    {
        public int AddressId { get; set; }
        public Address Address { get; set; }
        
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}