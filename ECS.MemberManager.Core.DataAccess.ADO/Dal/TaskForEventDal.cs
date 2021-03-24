using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ECS.MemberManager.Core.DataAccess.ADO
{
    public class TaskForEventDal : ITaskForEventDal
    {
        private static IConfigurationRoot _config;
        private SqlConnection _db = null;

        public TaskForEventDal()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var cnxnString = _config.GetConnectionString("LocalDbConnection");

            _db = new SqlConnection(cnxnString);
        }

        public TaskForEventDal(SqlConnection cnxn)
        {
            _db = cnxn;
        }

        public async Task<List<TaskForEvent>> Fetch()
        {
            var sql = "select * from TaskForEvents inner join Events on TaskForEvents.EventId = Events.Id";
            var result = await _db.QueryAsync<TaskForEvent, Event, TaskForEvent>(sql,
                (taskForEvent, eventObj) =>
                {
                    taskForEvent.Event = eventObj;
                    return taskForEvent;
                }
            );

            return result.ToList();
        }

        public async Task<TaskForEvent> Fetch(int id)
        {
            var sql = "select * from TaskForEvents inner join Events on TaskForEvents.EventId = Events.Id " +
                      $"where TaskForEvents.Id = {id}";
            var result = await _db.QueryAsync<TaskForEvent, Event, TaskForEvent>(sql,
                (taskForEvent, eventObj) =>
                {
                    taskForEvent.Event = eventObj;
                    return taskForEvent;
                }
            );

            return result.First();
        }

        public async Task<TaskForEvent> Insert(TaskForEvent eventToInsert)
        {
            var sql =
                "INSERT INTO [dbo].[TaskForEvents] ([EventId], [TaskName], [PlannedDate], [ActualDate], [Information], [LastUpdatedBy], [LastUpdatedDate], [Notes]) " +
                "VALUES (@EventId, @TaskName, @PlannedDate, @ActualDate, @Information, @LastUpdatedBy, @LastUpdatedDate, @Notes) " +
                "SELECT SCOPE_IDENTITY()";
            eventToInsert.Id = await _db.ExecuteScalarAsync<int>(sql, new
            {
                Id = eventToInsert.Id,
                EventId = eventToInsert.Event.Id,
                TaskName = eventToInsert.TaskName,
                PlannedDate = eventToInsert.PlannedDate,
                ActualDate = eventToInsert.ActualDate,
                Information = eventToInsert.Information,
                LastUpdatedBy = eventToInsert.LastUpdatedBy,
                LastUpdatedDate = eventToInsert.LastUpdatedDate,
                Notes = eventToInsert.Notes,
                RowVersion = eventToInsert.RowVersion
            });

            //reretrieve TaskForEvent to get rowversion
            var insertedEmail = await _db.GetAsync<TaskForEvent>(eventToInsert.Id);
            eventToInsert.RowVersion = insertedEmail.RowVersion;

            return eventToInsert;
        }

        public async Task<TaskForEvent> Update(TaskForEvent eventToUpdate)
        {
            var sql = "UPDATE [dbo].[TaskForEvents] " +
                      "SET [EventId] = @EventId, " +
                      "[TaskName] = @TaskName, " +
                      "[PlannedDate] = @PlannedDate, " +
                      "[ActualDate] = @ActualDate, " +
                      "[Information] = @Information, " +
                      "[LastUpdatedBy] = @LastUpdatedBy, " +
                      "[LastUpdatedDate] = @LastUpdatedDate, " +
                      "[Notes] = @Notes " +
                      "OUTPUT inserted.RowVersion " +
                      "WHERE Id = @Id AND RowVersion = @RowVersion ";

            var rowVersion = await _db.ExecuteScalarAsync<byte[]>(sql, new
            {
                Id = eventToUpdate.Id,
                EventId = eventToUpdate.Event.Id,
                TaskName = eventToUpdate.TaskName,
                PlannedDate = eventToUpdate.PlannedDate,
                ActualDate = eventToUpdate.ActualDate,
                Information = eventToUpdate.Information,
                LastUpdatedBy = eventToUpdate.LastUpdatedBy,
                LastUpdatedDate = eventToUpdate.LastUpdatedDate,
                Notes = eventToUpdate.Notes,
                RowVersion = eventToUpdate.RowVersion
            });

            if (rowVersion == null)
                throw new DBConcurrencyException("Entity has been updated since last read. Try again!");
            eventToUpdate.RowVersion = rowVersion;

            return eventToUpdate;
        }

        public async Task Delete(int id)
        {
            await _db.DeleteAsync<TaskForEvent>(new TaskForEvent() {Id = id});
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}