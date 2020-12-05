using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class EMail : EntityBase
    {
        public EMailType EMailType { get; set; }
        public string EMailAddress { get; set; }
        [MaxLength(255)] public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string Notes { get; set; }

        public IList<Organization> Organizations { get; set; }
        public IList<Person> Persons { get; set; }
    }
}