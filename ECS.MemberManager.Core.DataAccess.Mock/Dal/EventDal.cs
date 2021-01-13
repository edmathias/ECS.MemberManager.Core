﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class EventDal : IEventDal
    {
        public void Dispose()
        {
        }

        public async Task<Event> Fetch(int id)
        {
            return MockDb.Events.FirstOrDefault(ms => ms.Id == id);
        }

        public async Task<List<Event>> Fetch()
        {
            return MockDb.Events.ToList();
        }

        public async Task<Event> Insert(Event eventToInsert)
        {
            var lastEvent = MockDb.Events.ToList().OrderByDescending(e =>e.Id).First();
            eventToInsert.Id = 1+lastEvent.Id;
            
            MockDb.Events.Add(eventToInsert);
            return eventToInsert;        
        }

        public async Task<Event> Update(Event eventUpdate)
        {
            var eventToUpdate = MockDb.Events.FirstOrDefault(e => e.Id == eventUpdate.Id);

            eventToUpdate.EventName = eventUpdate.EventName;
            eventToUpdate.Description = eventUpdate.Description; 
            eventToUpdate.Notes = eventUpdate.Notes;
            eventToUpdate.IsOneTime = eventUpdate.IsOneTime;
            eventToUpdate.LastUpdatedBy = eventUpdate.LastUpdatedBy;
            eventToUpdate.LastUpdatedDate = DateTime.Now;
            eventToUpdate.NextDate = eventUpdate.NextDate;
            eventToUpdate.Persons = eventUpdate.Persons;

            return eventUpdate;
        }

        public async Task Delete(int id)
        {
            var eventToDelete = MockDb.Events.FirstOrDefault(e => e.Id == id);
            var listIndex = MockDb.Events.IndexOf(eventToDelete);
            if(listIndex > -1)
                MockDb.Events.RemoveAt(listIndex);
        }
    }
}