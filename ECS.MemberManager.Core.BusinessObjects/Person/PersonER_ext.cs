using Csla.Rules.CommonRules;

namespace ECS.MemberManager.Core.BusinessObjects
{
    public partial class PersonER
    {
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            
            BusinessRules.AddRule(new Required(LastNameProperty));
            BusinessRules.AddRule(new MaxLength(LastNameProperty,50));
            BusinessRules.AddRule(new MaxLength(MiddleNameProperty,50));
            BusinessRules.AddRule(new MaxLength(FirstNameProperty,50));
            BusinessRules.AddRule(new Required(LastUpdatedByProperty));
            BusinessRules.AddRule(new MaxLength(LastUpdatedByProperty,50));
            BusinessRules.AddRule(new MaxLength(CodeProperty,5));

        }
    }
}