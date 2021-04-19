using Csla.Rules.CommonRules;

namespace ECS.MemberManager.Core.BusinessObjects
{
    public partial class PersonalNoteER
    {
        protected override void AddBusinessRules()
        {            
            base.AddBusinessRules();

            BusinessRules.AddRule(new Required(DescriptionProperty));
            BusinessRules.AddRule(new MaxLength(DescriptionProperty, 50));
            BusinessRules.AddRule(new Required(LastUpdatedByProperty));
            BusinessRules.AddRule(new MaxLength(LastUpdatedByProperty, 255));
        }
    }
}