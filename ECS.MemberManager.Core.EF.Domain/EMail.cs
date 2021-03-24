using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;

namespace ECS.MemberManager.Core.EF.Domain
{
    [Serializable]
    [Table("EMails")]
    public class EMail : EntityBase
    {
        [Required, MaxLength(255)] public string EMailAddress { get; set; }
        [Required, MaxLength(255)] public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string Notes { get; set; }

        public EMailType EMailType { get; set; }

        [Write(false)] public IList<Organization> Organizations { get; set; }
        [Write(false)] public IList<Person> Persons { get; set; }
    }
}