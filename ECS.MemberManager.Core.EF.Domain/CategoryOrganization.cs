namespace ECS.MemberManager.Core.EF.Domain
{
    public class CategoryOrganization
    {
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }

        public int CategoryOfOrganizationId { get; set; }
        public CategoryOfOrganization CategoryOfOrganization { get; set; }
    }
}