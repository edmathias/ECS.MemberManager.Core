using Csla.Rules;
using Csla.Rules.CommonRules;

namespace ECS.MemberManager.Core.BusinessObjects
{
    public partial class TaskForEventER 
    {
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            
            BusinessRules.AddRule(new Required(LastUpdatedByProperty));
            BusinessRules.AddRule(new MaxLength(LastUpdatedByProperty,255));
            BusinessRules.AddRule(new Required(TaskNameProperty));
            BusinessRules.AddRule(new MaxLength(TaskNameProperty,50));
        }
    }
}