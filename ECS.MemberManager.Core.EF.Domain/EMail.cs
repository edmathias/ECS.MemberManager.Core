using System;

namespace ECS.MemberManager.Core.EF.Domain
{
    public class EMail
    {
        public int Id { get; private set; }
        public EMailType EMailType { get; set; }
        public string EMailAddress { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string Notes { get; set; }
    }
}