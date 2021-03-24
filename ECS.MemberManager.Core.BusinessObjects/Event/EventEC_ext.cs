using Csla.Rules.CommonRules;

namespace ECS.MemberManager.Core.BusinessObjects
{
    public partial class EventEC
    {
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();

            BusinessRules.AddRule(new Required(EventNameProperty));
            BusinessRules.AddRule(new MaxLength(EventNameProperty, 255));
            BusinessRules.AddRule(new Required(LastUpdatedByProperty));
            BusinessRules.AddRule(new MaxLength(LastUpdatedByProperty, 255));
            BusinessRules.AddRule(new Required(LastUpdatedDateProperty));
        }
    }
}