using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.BizBricks.CRM.Core.EF.Domain
{
    public class Person
    {
        public Person()
        {
            AddressPersons = new List<AddressPerson>();
            CategoryPersons = new List<CategoryPerson>();
        }

        public int Id { get; private set; }
        [Required,MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string MiddleName { get; set; }
        [Required,MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        public DateTime DateOfFirstContact { get; set; }
        public DateTime Birthdate { get; set; }
        [Required]
        public Title Title { get; set; }
        public TitleSuffix TitleSuffix { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public CategoryOfPerson CategoryOfPerson { get; set; }
        public int LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string Notes { get; set; }
        [Timestamp] public byte[] RowVersion { get; set; }

        public IList<AddressPerson> AddressPersons { get; set; }
        public IList<CategoryPerson> CategoryPersons { get; set; }

    }
}