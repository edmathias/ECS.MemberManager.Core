using Csla.Rules;
using Csla.Rules.CommonRules;

namespace ECS.MemberManager.Core.BusinessObjects
{
    public partial class TitleEC
    {
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            
            BusinessRules.AddRule(new Required(AbbreviationProperty));
            BusinessRules.AddRule(new MaxLength(AbbreviationProperty,10));
            BusinessRules.AddRule(new MaxLength(DescriptionProperty,50));
        }
    }
}