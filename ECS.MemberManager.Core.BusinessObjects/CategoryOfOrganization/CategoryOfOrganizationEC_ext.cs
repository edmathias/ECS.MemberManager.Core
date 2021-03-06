﻿using Csla.Rules.CommonRules;

namespace ECS.MemberManager.Core.BusinessObjects
{
    public partial class CategoryOfOrganizationEC
    {
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();

            BusinessRules.AddRule(new Required(CategoryProperty));
            BusinessRules.AddRule(new MaxLength(CategoryProperty, 35));
        }
    }
}