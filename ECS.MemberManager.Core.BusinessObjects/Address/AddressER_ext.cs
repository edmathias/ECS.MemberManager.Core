using Csla.Rules.CommonRules;

namespace ECS.MemberManager.Core.BusinessObjects
{
    public partial class AddressER
    {
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            
            BusinessRules.AddRule(new Required(Address1Property));
            BusinessRules.AddRule(new Required(CityProperty));
            BusinessRules.AddRule(new Required(StateProperty));
            BusinessRules.AddRule(new Required(PostCodeProperty));
            BusinessRules.AddRule(new MaxLength(Address1Property,35));
            BusinessRules.AddRule(new MaxLength(Address2Property,35));
            BusinessRules.AddRule(new MaxLength(CityProperty,50));
            BusinessRules.AddRule(new MaxLength(StateProperty,2));
            BusinessRules.AddRule(new MaxLength(PostCodeProperty,9));
        }        
    }
}