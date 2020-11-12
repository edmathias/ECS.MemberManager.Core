﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.BizBricks.CRM.Core.EF.Domain
{
    public class Person
    {
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
        public int LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string Notes { get; set; }
        
        public IList<Address> Addresses { get; set; }
        public IList<CategoryOfPerson> CategoryOfPersons { get; set; }
        public IList<Phone> Phones { get; set; }
        public IList<EMail> Type { get; set; }
    }
}