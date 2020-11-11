using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Timestamp] public byte[] RowVersion { get; set; }
    }
}