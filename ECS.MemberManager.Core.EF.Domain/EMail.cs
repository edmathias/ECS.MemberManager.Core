using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ECS.BizBricks.CRM.Core.EF.Domain;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class EMail
    {
        public int Id { get; private set; }
        public EMailType EMailType { get; set; }
        public string EMailAddress { get; set; }
        [MaxLength(255)] public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string Notes { get; set; }

        public IList<Organization> Organizations { get; set; }
        public IList<Person> Persons { get; set; }
    }
}