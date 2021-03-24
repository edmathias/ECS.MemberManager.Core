using Csla.Rules.CommonRules;

namespace ECS.MemberManager.Core.BusinessObjects
{
    public partial class EventDocumentER
    {
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();

            BusinessRules.AddRule(new Required(EventProperty));
            BusinessRules.AddRule(new Required(DocumentNameProperty));
            BusinessRules.AddRule(new MaxLength(DocumentNameProperty, 50));
            BusinessRules.AddRule(new Required(PathAndFileNameProperty));
            BusinessRules.AddRule(new MaxLength(PathAndFileNameProperty, 255));
            BusinessRules.AddRule(new Required(LastUpdatedByProperty));
            BusinessRules.AddRule(new MaxLength(LastUpdatedByProperty, 255));
            BusinessRules.AddRule(new Required(LastUpdatedDateProperty));
        }
    }
}