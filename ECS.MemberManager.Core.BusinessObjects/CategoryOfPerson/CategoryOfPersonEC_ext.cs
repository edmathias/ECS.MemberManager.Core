﻿using Csla.Rules.CommonRules;

namespace ECS.MemberManager.Core.BusinessObjects
{
    public partial class CategoryOfPersonEC
    {
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();

            BusinessRules.AddRule(new Required(CategoryProperty));
            BusinessRules.AddRule(new MaxLength(CategoryProperty, 50));
        }
    }
}