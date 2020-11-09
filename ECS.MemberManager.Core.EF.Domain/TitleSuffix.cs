﻿using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class TitleSuffix
    {
        [Key] public int Id { get; private set; }
        [Required,MaxLength(10)] public string Abbreviation { get; set; }
        [MaxLength(255)] public string Description { get; set; }
        public int DisplayOrder { get; set; }
    }
}