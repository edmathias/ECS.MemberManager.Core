using System;
using System.IO;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EMailTypeER_Tests : CslaBaseTest
    {
        [Fact]
        public async Task EMailTypeER_TestGetEMailType()
        {
            var eMailType = await EMailTypeER.GetEMailTypeER(1);

            Assert.NotNull(eMailType);
            Assert.IsType<EMailTypeER>(eMailType);
        }

        [Fact]
        public async Task EMailTypeER_TestGetNewEMailTypeER()
        {
            var eMailType = await EMailTypeER.NewEMailTypeER();

            Assert.NotNull(eMailType);
            Assert.False(eMailType.IsValid);
        }

        [Fact]
        public async Task EMailTypeER_TestUpdateExistingEMailTypeER()
        {
            var eMailType = await EMailTypeER.GetEMailTypeER(1);
            eMailType.Notes = "These are updated Notes";

            var result = await eMailType.SaveAsync();

            Assert.NotNull(result);
            Assert.Equal("These are updated Notes", result.Notes);
        }

        [Fact]
        public async Task EMailTypeER_TestInsertNewEMailTypeER()
        {
            var eMailType = await EMailTypeER.NewEMailTypeER();
            eMailType.Description = "Standby";
            eMailType.Notes = "This person is on standby";

            var savedEMailType = await eMailType.SaveAsync();

            Assert.NotNull(savedEMailType);
            Assert.IsType<EMailTypeER>(savedEMailType);
            Assert.True(savedEMailType.Id > 0);
        }

        [Fact]
        public async Task EMailTypeER_TestDeleteObjectFromDatabase()
        {
            const int ID_TO_DELETE = 99;

            await EMailTypeER.DeleteEMailTypeER(ID_TO_DELETE);

            await Assert.ThrowsAsync<DataPortalException>(() => EMailTypeER.GetEMailTypeER(ID_TO_DELETE));
        }

        // test invalid state 
        [Fact]
        public async Task EMailTypeER_TestDescriptionRequired()
        {
            var eMailType = await EMailTypeER.NewEMailTypeER();
            eMailType.Description = "make valid";
            var isObjectValidInit = eMailType.IsValid;
            eMailType.Description = string.Empty;

            Assert.NotNull(eMailType);
            Assert.True(isObjectValidInit);
            Assert.False(eMailType.IsValid);
            Assert.Equal("Description", eMailType.BrokenRulesCollection[0].Property);
            Assert.Equal("Description required", eMailType.BrokenRulesCollection[0].Description);
        }

        [Fact]
        public async Task EMailTypeER_TestDescriptionExceedsMaxLengthOf50()
        {
            var eMailType = await EMailTypeER.NewEMailTypeER();
            eMailType.Description = "valid length";
            Assert.True(eMailType.IsValid);

            eMailType.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                    "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                    "nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                                    "Duis aute irure dolor in reprehenderit";

            Assert.NotNull(eMailType);
            Assert.False(eMailType.IsValid);
            Assert.Equal("Description", eMailType.BrokenRulesCollection[0].Property);
            Assert.Equal("Description can not exceed 50 characters", eMailType.BrokenRulesCollection[0].Description);
        }
        // test exception if attempt to save in invalid state

        [Fact]
        public async Task EMailTypeER_TestInvalidSaveEMailTypeER()
        {
            var eMailType = await EMailTypeER.NewEMailTypeER();
            eMailType.Description = String.Empty;
            EMailTypeER savedEMailType = null;

            Assert.False(eMailType.IsValid);
            Assert.Throws<Csla.Rules.ValidationException>(() => savedEMailType = eMailType.Save());
        }
    }
}