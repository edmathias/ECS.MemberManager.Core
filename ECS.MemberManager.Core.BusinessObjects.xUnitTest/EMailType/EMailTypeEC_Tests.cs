﻿using System.IO;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EMailTypeEC_Tests : CslaBaseTest
    {
        [Fact]
        public async Task TestEMailTypeEC_NewEMailTypeEC()
        {
            var eMailType = await EMailTypeEC.NewEMailTypeEC();

            Assert.NotNull(eMailType);
            Assert.IsType<EMailTypeEC>(eMailType);
            Assert.False(eMailType.IsValid);
        }

        [Fact]
        public async Task TestEMailTypeEC_GetEMailTypeEC()
        {
            var eMailTypeToLoad = BuildEMailType();
            var eMailType = await EMailTypeEC.GetEMailTypeEC(eMailTypeToLoad);

            Assert.NotNull(eMailType);
            Assert.IsType<EMailTypeEC>(eMailType);
            Assert.Equal(eMailTypeToLoad.Id, eMailType.Id);
            Assert.Equal(eMailTypeToLoad.Description, eMailType.Description);
            Assert.Equal(eMailTypeToLoad.Notes, eMailType.Notes);
            Assert.Equal(eMailTypeToLoad.RowVersion, eMailType.RowVersion);
            Assert.True(eMailType.IsValid);
        }

        [Fact]
        public async Task TestEMailTypeEC_DescriptionRequired()
        {
            var eMailTypeToTest = BuildEMailType();
            var eMailType = await EMailTypeEC.GetEMailTypeEC(eMailTypeToTest);
            var isObjectValidInit = eMailType.IsValid;
            eMailType.Description = string.Empty;

            Assert.NotNull(eMailType);
            Assert.True(isObjectValidInit);
            Assert.False(eMailType.IsValid);
            Assert.Equal("Description", eMailType.BrokenRulesCollection[0].Property);
            Assert.Equal("Description required", eMailType.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task TestEMailTypeEC_DescriptionGreaterThan50Chars()
        {
            var eMailTypeToTest = BuildEMailType();
            var eMailType = await EMailTypeEC.GetEMailTypeEC(eMailTypeToTest);
            var isObjectValidInit = eMailType.IsValid;
            eMailType.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                    "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                    "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                    "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";

            Assert.NotNull(eMailType);
            Assert.True(isObjectValidInit);
            Assert.False(eMailType.IsValid);
            Assert.Equal("Description", eMailType.BrokenRulesCollection[0].Property);
            Assert.Equal("Description can not exceed 50 characters", eMailType.BrokenRulesCollection[0].Description);
        }

        private EMailType BuildEMailType()
        {
            var eMailType = new EMailType();
            eMailType.Id = 1;
            eMailType.Description = "test description";
            eMailType.Notes = "notes for doctype";

            return eMailType;
        }
    }
}