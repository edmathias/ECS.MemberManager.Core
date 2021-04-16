using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PersonalNoteROCL_Tests : CslaBaseTest
    {
        [Fact]
        private async void PersonalNoteROCL_TestGetPersonalNoteInfoList()
        {
            var childData = MockDb.PersonalNotes;

            var personalNoteList = await PersonalNoteROCL.GetPersonalNoteROCL(childData);

            Assert.NotNull(personalNoteList);
            Assert.True(personalNoteList.IsReadOnly);
            Assert.Equal(3, personalNoteList.Count);
        }
    }
}