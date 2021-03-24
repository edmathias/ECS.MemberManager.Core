using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class CategoryOfOrganization : EntityBase
    {
        [Required, MaxLength(35)] public string Category { get; set; }
        public int DisplayOrder { get; set; }
        public IList<Organization> Organizations { get; set; }
    }
}