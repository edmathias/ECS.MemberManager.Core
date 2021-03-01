using System;
using System.ComponentModel;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class ContactForSponsorROR_Tests
    {

        [Fact]
        public async void ContactForSponsorROR_TestGetById()
        {
            var category = await ContactForSponsorROR.GetContactForSponsorROR(1);
            
            Assert.NotNull(category);
            Assert.IsType<ContactForSponsorROR>(category);
            Assert.Equal(1, category.Id);
        }
    }
}