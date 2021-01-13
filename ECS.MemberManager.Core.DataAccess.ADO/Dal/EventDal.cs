using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
    public class EventDal : IEventDal
    {
        private static IConfigurationRoot _config;
        private SqlConnection _db = null;

        public EventDal()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var cnxnString = _config.GetConnectionString("LocalDbConnection");

            _db = new SqlConnection(cnxnString);
        }

        public EventDal(SqlConnection cnxn)
        {
            _db = cnxn;
        }

        public async Task<List<Event>> Fetch()
        {
            var events = await _db.GetAllAsync<Event>();
            return events.ToList();
        }

        public async Task<Event> Fetch(int id)
        {
            return await _db.GetAsync<Event>(id);
        }

        public async Task<Event> Insert(Event eventToInsert)
        {
            var sql =
                "INSERT INTO [dbo].[Events] ([EventName], [Description], [IsOneTime], [NextDate], [LastUpdatedBy], [LastUpdatedDate], [Notes]) " +
                "VALUES (@EventName, @Description, @IsOneTime, @NextDate, @LastUpdatedBy, @LastUpdatedDate, @Notes) " +
                "SELECT SCOPE_IDENTITY()";
            eventToInsert.Id = await _db.ExecuteScalarAsync<int>(sql, eventToInsert);

            //reretrieve Event to get rowversion
            var insertedEmail = await _db.GetAsync<Event>(eventToInsert.Id);
            eventToInsert.RowVersion = insertedEmail.RowVersion;

            return eventToInsert;
        }

        public async Task<Event> Update(Event eventToUpdate)
        {
            var sql = "UPDATE Events " +
                      "SET [EventName] = @EventName, " +
                      "[Description] = @Description, " +
                      "[IsOneTime] = @IsOneTime, " +
                      "[NextDate] = @NextDate, " +
                      "[LastUpdatedBy] = @LastUpdatedBy, " +
                      "[LastUpdatedDate] = @LastUpdatedDate, " +
                      "[Notes] = @Notes " +
                      "OUTPUT inserted.RowVersion " +
                      "WHERE Id = @Id AND RowVersion = @RowVersion ";

            var rowVersion = await _db.ExecuteScalarAsync<byte[]>(sql, eventToUpdate);
            if (rowVersion == null)
                throw new DBConcurrencyException("Entity has been updated since last read. Try again!");
            eventToUpdate.RowVersion = rowVersion;

            return eventToUpdate;
        }

        public async Task Delete(int id)
        {
            await _db.DeleteAsync<Event>(new Event() {Id = id});
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}