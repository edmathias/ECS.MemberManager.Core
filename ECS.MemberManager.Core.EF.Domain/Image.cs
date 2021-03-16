using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class Image : EntityBase
    {
        public string ImagePath;

        public byte[] ImageFile;
    }
}