using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class Person : EntityBase
    {
        [Required]
        public Title Title { get; set; }
        [Required,MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string MiddleName { get; set; }
        [Required,MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        public DateTime DateOfFirstContact { get; set; }
        public DateTime BirthDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        [MaxLength(5)]
        public string Code { get; set; }
        public string Notes { get; set; }
        
        public IList<Address> Addresses { get; set; }
        public IList<CategoryOfPerson> CategoryOfPersons { get; set; }
        public IList<Phone> Phones { get; set; }
        public IList<Event> Events { get; set; }
    }
}