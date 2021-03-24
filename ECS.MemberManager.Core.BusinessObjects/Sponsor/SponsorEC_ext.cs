using Csla.Rules.CommonRules;

namespace ECS.MemberManager.Core.BusinessObjects
{
    public partial class SponsorEC
    {
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();

            BusinessRules.AddRule(new Required(StatusProperty));
            BusinessRules.AddRule(new MaxLength(StatusProperty, 255));
        }
    }
}