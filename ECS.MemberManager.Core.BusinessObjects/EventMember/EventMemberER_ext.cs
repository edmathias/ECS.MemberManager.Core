using Csla.Rules;
using Csla.Rules.CommonRules;

namespace ECS.MemberManager.Core.BusinessObjects
{
    public partial class EventMemberER 
    {
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            
            BusinessRules.AddRule(new Required(MemberInfoProperty));
            BusinessRules.AddRule(new Required(EventProperty));
            BusinessRules.AddRule(new Required(RoleProperty));
            BusinessRules.AddRule(new MaxLength(RoleProperty,50));            
            BusinessRules.AddRule(new Required(LastUpdatedByProperty));
            BusinessRules.AddRule(new MaxLength(LastUpdatedByProperty,255));
            BusinessRules.AddRule(new Required(LastUpdatedDateProperty));
        }
    }
}