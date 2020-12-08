using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects.Test
{
    [TestClass]
    public class EventER_Tests 
    {
        [TestMethod]
        public async Task TestEventER_Get()
        {
            var getEvent = await EventER.GetEvent(1);
            

            Assert.AreEqual(getEvent.Id, 1);
            Assert.IsTrue(getEvent.IsValid);
        }

        [TestMethod]
        public async Task TestEventER_New()
        {
            var newEvent = await EventER.NewEvent();

            Assert.IsNotNull(newEvent);
            Assert.IsFalse(newEvent.IsValid);
        }

        [TestMethod]
        public async Task TestEventER_Update()
        {
            var eventToUpdate = await EventER.GetEvent(1);
            eventToUpdate.Notes = "These are updated Notes";
            
            var result = await eventToUpdate.SaveAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Notes, "These are updated Notes");
        }

        [TestMethod]
        public async Task TestEventER_Insert()
        {
            var newEvent = await EventER.NewEvent();
            CreateValidEvent(newEvent);

            var savedEvent = await newEvent.SaveAsync();
           
            Assert.IsNotNull(savedEvent);
            Assert.IsInstanceOfType(savedEvent, typeof(EventER));
            Assert.IsTrue( savedEvent.Id > 0 );
        }

 
        [TestMethod]
        public async Task TestEventER_Delete()
        {
            int beforeCount = MockDb.Events.Count();
            
            await EventER.DeleteEvent(1);
            
            Assert.AreNotEqual(beforeCount,MockDb.Events.Count());
        }
        
        // test invalid state 
        [TestMethod]
        public async Task TestEventER_EventNameRequired()
        {
            var validEvent = await EventER.NewEvent();
            CreateValidEvent(validEvent);
            var isObjectValidInit = validEvent.IsValid;
            validEvent.EventName = string.Empty;
            
            Assert.IsTrue(isObjectValidInit);
            Assert.IsFalse(validEvent.IsValid);
        }
    
         
        // test exception if attempt to save in invalid state

        [TestMethod]
        public async Task TestEventER_TestInvalidSave()
        {
            var eventToSave = await EventER.NewEvent();
            
            Assert.ThrowsException<ValidationException>(() => eventToSave.Save() );
        } 
        
        private static void CreateValidEvent(EventER newEvent)
        {
            newEvent.EventName = "This awesome event";
            newEvent.Description = "description of awesome event";
            newEvent.IsOneTime = true;
            newEvent.NextDate = DateTime.Now.AddDays(7);
            newEvent.LastUpdatedBy = "edm";
            newEvent.LastUpdatedDate = DateTime.Now;
            newEvent.Notes = "Notes for this event";
        }
        
    } 
}
