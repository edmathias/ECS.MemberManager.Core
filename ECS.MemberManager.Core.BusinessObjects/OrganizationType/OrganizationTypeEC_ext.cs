using Csla.Rules.CommonRules;

namespace ECS.MemberManager.Core.BusinessObjects
{
    public partial class OrganizationTypeEC
    {
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            
            BusinessRules.AddRule(new Required(NameProperty));
            BusinessRules.AddRule(new MaxLength(NameProperty,50));
            BusinessRules.AddRule(new MaxLength(NotesProperty,255));
        }
    }
}