using Csla.Rules.CommonRules;

namespace ECS.MemberManager.Core.BusinessObjects
{
    public partial class EMailER
    {
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            
            BusinessRules.AddRule(new Required(EMailAddressProperty));
            BusinessRules.AddRule(new MaxLength	(EMailAddressProperty,255));            
            BusinessRules.AddRule(new Required(LastUpdatedByProperty));
            BusinessRules.AddRule(new MaxLength	(LastUpdatedByProperty,255));
        }        
    }
}