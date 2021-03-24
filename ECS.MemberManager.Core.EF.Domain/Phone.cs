using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    [Serializable]
    public class Phone : EntityBase
    {
        public Phone()
        {
            Organizations = new List<Organization>();
            Persons = new List<Person>();
        }

        [MaxLength(10)] public string PhoneType { get; set; }
        [Required, MaxLength(3)] public string AreaCode { get; set; }
        [Required, MaxLength(25)] public string Number { get; set; }
        [MaxLength(25)] public string Extension { get; set; }
        public int DisplayOrder { get; set; }
        [MaxLength(255)] public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string Notes { get; set; }

        public IList<Organization> Organizations { get; set; }
        public IList<Person> Persons { get; set; }
    }
}