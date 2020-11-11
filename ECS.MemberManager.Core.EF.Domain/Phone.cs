using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ECS.BizBricks.CRM.Core.EF.Domain;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class Phone
    {
        public int Id { get; set; }
        [Required,MaxLength(35)]
        public string PhoneType { get; set; }
        [Required,MaxLength(3)]
        public string AreaCode { get; set; }
        [Required,MaxLength(7)]
        public string Number { get; set; }
        public string Extension { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        public IList<Organization> Organizations { get; set; }
        public IList<Person> Persons { get; set; }
    }
}