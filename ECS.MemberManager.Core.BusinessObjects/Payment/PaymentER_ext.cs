using Csla;
using Csla.Rules.CommonRules;

namespace ECS.MemberManager.Core.BusinessObjects
{
    public partial class PaymentER
    {
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            
            BusinessRules.AddRule(new MaxLength(NotesProperty,255));
            BusinessRules.AddRule(new MinValue<double>(AmountProperty,0.01));
            BusinessRules.AddRule(new Required(LastUpdatedByProperty));
            BusinessRules.AddRule(new MaxLength(LastUpdatedByProperty,255));
        }
    }
}