using Csla.Rules.CommonRules;

namespace ECS.MemberManager.Core.BusinessObjects
{
    public partial class MemberInfoER
    {
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();

            BusinessRules.AddRule(new Required(MemberNumberProperty));
            BusinessRules.AddRule(new MaxLength(MemberNumberProperty, 35));
            BusinessRules.AddRule(new Required(DateFirstJoinedProperty));
            BusinessRules.AddRule(new Required(MemberStatusProperty));
            BusinessRules.AddRule(new Required(MembershipTypeProperty));
            BusinessRules.AddRule(new Required(LastUpdatedByProperty));
            BusinessRules.AddRule(new MaxLength(LastUpdatedByProperty, 255));
            BusinessRules.AddRule(new Required(LastUpdatedDateProperty));
        }
    }
}