using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class CategoryOfPerson : EntityBase
    {
        [Required,MaxLength(50)] public string Category { get; set; }
        public int DisplayOrder { get; set; }
        public IList<Person> Persons { get; set; }
    }
}