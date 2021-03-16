using System.ComponentModel.DataAnnotations;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class Image : EntityBase
    {
        public string ImagePath { get; set; }

        public byte[] ImageFile { get; set; }
    }
}