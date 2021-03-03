using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class TermInOfficeERL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public TermInOfficeERL_Tests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var testLibrary = _config.GetValue<string>("TestLibrary");
            
            if(testLibrary == "Mock")
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
        private async void TermInOfficeERL_TestNewTermInOfficeERL()
        {
            var eventEdit = await TermInOfficeERL.NewTermInOfficeERL();

            Assert.NotNull(eventEdit);
            Assert.IsType<TermInOfficeERL>(eventEdit);
        }
        
        [Fact]
        private async void TermInOfficeERL_TestGetTermInOfficeERL()
        {
            var eventEdit = 
                await TermInOfficeERL.GetTermInOfficeERL();

            Assert.NotNull(eventEdit);
            Assert.Equal(3, eventEdit.Count);
        }
        
        [Fact]
        private async void TermInOfficeERL_TestDeleteTermInOfficeERL()
        {
            const int ID_TO_DELETE = 99;
            var eventList = 
                await TermInOfficeERL.GetTermInOfficeERL();
            var listCount = eventList.Count;
            var eventToDelete = eventList.First(cl => cl.Id == ID_TO_DELETE);
            // remove is deferred delete
            var isDeleted = eventList.Remove(eventToDelete); 

            var eventListAfterDelete = await eventList.SaveAsync();

            Assert.NotNull(eventListAfterDelete);
            Assert.IsType<TermInOfficeERL>(eventListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount,eventListAfterDelete.Count);
        }

        [Fact]
        private async void TermInOfficeERL_TestUpdateTermInOfficeERL()
        {
            const int ID_TO_UPDATE = 1;
            
            var eventList = 
                await TermInOfficeERL.GetTermInOfficeERL();
            var countBeforeUpdate = eventList.Count;
            var eventToUpdate = eventList.First(cl => cl.Id == ID_TO_UPDATE);
            eventToUpdate.Notes = "Updated Notes";
            
            var updatedList = await eventList.SaveAsync();
            
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void TermInOfficeERL_TestAddTermInOfficeERL()
        {
            var eventList = 
                await TermInOfficeERL.GetTermInOfficeERL();
            var countBeforeAdd = eventList.Count;
            
            var eventToAdd = eventList.AddNew();
            await BuildTermInOffice(eventToAdd);

            var updatedTermInOfficeList = await eventList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, updatedTermInOfficeList.Count);
        }

        private async Task BuildTermInOffice(TermInOfficeEC termObj)
        {
            termObj.Office = await OfficeEC.GetOfficeEC( new Office() {Id = 1});
            termObj.Person = await PersonEC.GetPersonEC(new Person() {Id = 1});
            termObj.StartDate = DateTime.Now;
            termObj.LastUpdatedBy = "edm";
            termObj.LastUpdatedDate = DateTime.Now;
            termObj.Notes = "notes for doctype";

        }         

    }
}