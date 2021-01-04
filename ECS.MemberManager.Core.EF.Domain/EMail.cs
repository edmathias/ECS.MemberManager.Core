using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class EMail : EntityBase
    {
        public int EMailTypeId { get; set; }
        public string EMailAddress { get; set; }
        [MaxLength(255)] public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string Notes { get; set; }

        [Write(false)]
        public IList<Organization> Organizations { get; set; }
        [Write(false)]
        public IList<Person> Persons { get; set; }
    }
}