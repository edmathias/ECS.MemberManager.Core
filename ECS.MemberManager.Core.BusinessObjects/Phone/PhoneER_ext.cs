using Csla.Rules.CommonRules;

namespace ECS.MemberManager.Core.BusinessObjects
{
    public partial class PhoneER
    {
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();

            BusinessRules.AddRule(new Required(PhoneTypeProperty));
            BusinessRules.AddRule(new Required(AreaCodeProperty));
            BusinessRules.AddRule(new Required(NumberProperty));
            BusinessRules.AddRule(new Required(LastUpdatedByProperty));
            BusinessRules.AddRule(new MaxLength(LastUpdatedByProperty, 255));
        }
    }
}