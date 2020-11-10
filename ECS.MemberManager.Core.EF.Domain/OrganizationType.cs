namespace ECS.MemberManager.Core.EF.Domain
{
    public class OrganizationType
    {
        public int Id { get; set; }
        public CategoryOfOrganization CategoryOfOrganization { get; set; }
        public string TypeName { get; set; }
    }
}