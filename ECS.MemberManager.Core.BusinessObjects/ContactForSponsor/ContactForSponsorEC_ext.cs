using System.Buffers.Text;
using Csla.Rules.CommonRules;

namespace ECS.MemberManager.Core.BusinessObjects
{
    public partial class ContactForSponsorEC
    {
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            
            BusinessRules.AddRule(new Required(PurposeProperty));
            BusinessRules.AddRule(new MaxLength(PurposeProperty,255));
            BusinessRules.AddRule(new Required(LastUpdatedByProperty));
            BusinessRules.AddRule(new MaxLength(LastUpdatedByProperty,255));
            BusinessRules.AddRule(new Required(LastUpdatedDateProperty));
        }                
        
    }
}