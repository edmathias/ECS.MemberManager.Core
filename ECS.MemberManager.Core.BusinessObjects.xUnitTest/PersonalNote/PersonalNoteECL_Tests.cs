using System;
using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PersonalNoteECL_Tests : CslaBaseTest
    {
        [Fact]
        private async void PersonalNoteECL_TestPersonalNoteECL()
        {
            var memberStatusEdit = await PersonalNoteECL.NewPersonalNoteECL();

            Assert.NotNull(memberStatusEdit);
            Assert.IsType<PersonalNoteECL>(memberStatusEdit);
        }

        [Fact]
        private async void PersonalNoteECL_TestGetPersonalNoteECL()
        {
            var childData = MockDb.PersonalNotes;

            var listToTest = await PersonalNoteECL.GetPersonalNoteECL(childData);

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }
    }
}