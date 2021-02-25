using Csla.Rules;
using Csla.Rules.CommonRules;

namespace ECS.MemberManager.Core.BusinessObjects
{
    public partial class SponsorER
    {
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();

            BusinessRules.AddRule(new Required(StatusProperty));
            BusinessRules.AddRule(new MaxLength(StatusProperty,255));         
        }
    }
}