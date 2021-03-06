﻿using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class EntityBase
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }

        [Timestamp] public byte[] RowVersion { get; set; }
    }
}