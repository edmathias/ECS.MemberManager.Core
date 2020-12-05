using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class EntityBase
    {
        [Key] public int Id { get; set; }
        [Timestamp] public byte[] Timestamp { get; set; }
    }
}