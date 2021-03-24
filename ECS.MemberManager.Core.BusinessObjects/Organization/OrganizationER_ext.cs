using Csla.Rules.CommonRules;

namespace ECS.MemberManager.Core.BusinessObjects
{
    public partial class OrganizationER
    {
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();

            BusinessRules.AddRule(new Required(NameProperty));
            BusinessRules.AddRule(new MaxLength(NameProperty, 50));
            BusinessRules.AddRule(new Required(LastUpdatedByProperty));
            BusinessRules.AddRule(new MaxLength(LastUpdatedByProperty, 255));
            BusinessRules.AddRule(new Required(OrganizationTypeProperty));
        }
    }
}