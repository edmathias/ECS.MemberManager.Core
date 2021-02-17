using Csla.Rules.CommonRules;

namespace ECS.MemberManager.Core.BusinessObjects
{
    public partial class MemberInfoEC
    {
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            
            BusinessRules.AddRule(new Required(MemberNumberProperty));
            BusinessRules.AddRule(new Required(LastUpdatedByProperty));
            BusinessRules.AddRule(new MaxLength(MemberNumberProperty,35));
            BusinessRules.AddRule(new MaxLength(LastUpdatedByProperty,255));
        }
    }
}