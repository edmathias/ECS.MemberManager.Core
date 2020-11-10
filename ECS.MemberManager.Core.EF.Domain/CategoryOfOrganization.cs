using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class CategoryOfOrganization
    {
        public CategoryOfOrganization()
        {
            CategoryOrganizations = new List<CategoryOrganization>();
        }

        [Key] public int Id { get; private set; }
        [Required,MaxLength(35)] public string TypeName { get; set; }
        public int DisplayOrder { get; set; }

        public IList<CategoryOrganization> CategoryOrganizations { get; set; }
    }
}