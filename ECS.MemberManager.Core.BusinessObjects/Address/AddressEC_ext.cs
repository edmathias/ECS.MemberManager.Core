using Csla.Rules.CommonRules;

namespace ECS.MemberManager.Core.BusinessObjects
{
    public partial class AddressEC
    {
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            
            BusinessRules.AddRule(new Required(Address1Property));
            BusinessRules.AddRule(new Required(CityProperty));
            BusinessRules.AddRule(new Required(StateProperty));
            BusinessRules.AddRule(new Required(PostCodeProperty));
            BusinessRules.AddRule(new Required(LastUpdatedByProperty));
            BusinessRules.AddRule(new Required(LastUpdatedDateProperty));
            BusinessRules.AddRule(new MaxLength(Address1Property,35));
            BusinessRules.AddRule(new MaxLength(Address2Property,35));
            BusinessRules.AddRule(new MaxLength(CityProperty,50));
            BusinessRules.AddRule(new MaxLength(StateProperty,2));
            BusinessRules.AddRule(new MaxLength(PostCodeProperty,9));
            BusinessRules.AddRule(new MaxLength(NotesProperty,255));
            BusinessRules.AddRule(new MaxLength(LastUpdatedByProperty,255));
        }        
    }
}