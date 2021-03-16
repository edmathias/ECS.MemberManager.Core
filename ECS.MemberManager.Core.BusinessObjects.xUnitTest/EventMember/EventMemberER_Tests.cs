using System;
using System.IO;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EventMemberER_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public EventMemberER_Tests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var testLibrary = _config.GetValue<string>("TestLibrary");

            if (testLibrary == "Mock")
                MockDb.ResetMockDb();
            else
            {
                if (!IsDatabaseBuilt)
                {
                    var adoDb = new ADODb();
                    adoDb.BuildMemberManagerADODb();
                    IsDatabaseBuilt = true;
                }
            }
        }

        [Fact]
        public async Task TestEventMemberER_NewEventMemberER()
        {
            var eventDocumentObj = await EventMemberER.NewEventMemberER();

            Assert.NotNull(eventDocumentObj);
            Assert.IsType<EventMemberER>(eventDocumentObj);
            Assert.False(eventDocumentObj.IsValid);
        }
        
        [Fact]
        public async Task TestEventMemberER_GetEventMemberER()
        {
            var eventDocumentObj = await EventMemberER.GetEventMemberER(1);

            Assert.NotNull(eventDocumentObj);
            Assert.IsType<EventMemberER>(eventDocumentObj);
            Assert.Equal(1,eventDocumentObj.Id);
            Assert.True(eventDocumentObj.IsValid);
        }

        [Fact]
        public async Task TestEventMemberER_EventRequired()
        {
            var eventDocumentObj = await BuildEventMember(); 
            var isObjectValidInit = eventDocumentObj.IsValid;
            eventDocumentObj.Event = null; 
           
            Assert.NotNull(eventDocumentObj);
            Assert.True(isObjectValidInit);
            Assert.False(eventDocumentObj.IsValid);
            Assert.Equal("Event",eventDocumentObj.BrokenRulesCollection[0].Property);
            Assert.Equal("Event required",eventDocumentObj.BrokenRulesCollection[0].Description);
        }
        
        [Fact]
        public async Task TestEventMemberER_MemberInfoRequired()
        {
            var eventDocumentObj = await BuildEventMember();
            var isObjectValidInit = eventDocumentObj.IsValid;
            eventDocumentObj.MemberInfo = null;
            
            Assert.NotNull(eventDocumentObj);
            Assert.True(isObjectValidInit);
            Assert.False(eventDocumentObj.IsValid);
            Assert.Equal("MemberInfo",eventDocumentObj.BrokenRulesCollection[0].Property);
            Assert.Equal("MemberInfo required",eventDocumentObj.BrokenRulesCollection[0].Description);
        }
        
        [Fact]
        public async Task TestEventMemberER_RoleCannotExceed50Chars()
        {
            var eventDocumentObj = await BuildEventMember();
            var isObjectValidInit = eventDocumentObj.IsValid;
            eventDocumentObj.Role = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor " +
                                    "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                    "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis " +
                                    "incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis ";
           
            Assert.NotNull(eventDocumentObj);
            Assert.True(isObjectValidInit);
            Assert.False(eventDocumentObj.IsValid);
            Assert.Equal("Role",eventDocumentObj.BrokenRulesCollection[0].Property);
            Assert.Equal("Role can not exceed 50 characters",eventDocumentObj.BrokenRulesCollection[0].Description);
        }

        
        [Fact]
        public async Task TestEventMemberER_LastUpdatedByRequired()
        {
            var eventDocumentObj = await BuildEventMember();
            var isObjectValidInit = eventDocumentObj.IsValid;
            eventDocumentObj.LastUpdatedBy = string.Empty;

            Assert.NotNull(eventDocumentObj);
            Assert.True(isObjectValidInit);
            Assert.False(eventDocumentObj.IsValid);
            Assert.Equal("LastUpdatedBy",eventDocumentObj.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedBy required",eventDocumentObj.BrokenRulesCollection[0].Description);
            
        }
        
        [Fact]
        public async Task TestEventMemberER_LastUpdatedDateRequired()
        {
            var eventDocumentObj = await BuildEventMember();
            var isObjectValidInit = eventDocumentObj.IsValid;
            eventDocumentObj.LastUpdatedDate = DateTime.MinValue;

            Assert.NotNull(eventDocumentObj);
            Assert.True(isObjectValidInit);
            Assert.False(eventDocumentObj.IsValid);
            Assert.Equal("LastUpdatedDate",eventDocumentObj.BrokenRulesCollection[0].Property);
            Assert.Equal("LastUpdatedDate required",eventDocumentObj.BrokenRulesCollection[0].Description);
        }

         
        private async Task<EventMemberER> BuildEventMember()
        {
            var eventDocumentObj = await EventMemberER.NewEventMemberER();
            eventDocumentObj.Event = await EventEC.GetEventEC(new Event() {Id = 1});
            eventDocumentObj.MemberInfo = await MemberInfoEC.GetMemberInfoEC(new MemberInfo() {Id = 1});
            eventDocumentObj.Role = "Organizer";
            eventDocumentObj.LastUpdatedBy = "edm";
            eventDocumentObj.LastUpdatedDate = DateTime.Now;
            eventDocumentObj.Notes = "notes for doctype";

            return eventDocumentObj;
        }        
    }
}