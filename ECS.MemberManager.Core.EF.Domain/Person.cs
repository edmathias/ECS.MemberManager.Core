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
            Addresses = new List<Address>();
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
        public PersonCategory PersonCategory { get; set; }
        public int LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string Notes { get; set; }
        [Timestamp] public byte[] RowVersion { get; set; }
        public IList<Address> Addresses { get; set; }

    }
}