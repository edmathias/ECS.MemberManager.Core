using Csla.Rules;
using Csla.Rules.CommonRules;

namespace ECS.MemberManager.Core.BusinessObjects
{
    public partial class TermInOfficeEC
    {
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            
            BusinessRules.AddRule(new Required(LastUpdatedByProperty));
            BusinessRules.AddRule(new MaxLength(LastUpdatedByProperty,255));
        }
    }
}