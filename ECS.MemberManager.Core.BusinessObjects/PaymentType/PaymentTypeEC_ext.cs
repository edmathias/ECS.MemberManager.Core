using Csla.Rules;
using Csla.Rules.CommonRules;

namespace ECS.MemberManager.Core.BusinessObjects
{
    public partial class PaymentTypeEC
    {
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();

            BusinessRules.AddRule(new Required(DescriptionProperty));
            BusinessRules.AddRule(new MaxLength(DescriptionProperty,50));

        }
    }
}