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
    public class EventMemberDal : IDal<EventMember>
    {
        private static IConfigurationRoot _config;
        private SqlConnection _db = null;

        public EventMemberDal()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var cnxnString = _config.GetConnectionString("LocalDbConnection");

            _db = new SqlConnection(cnxnString);
        }

        public EventMemberDal(SqlConnection cnxn)
        {
            _db = cnxn;
        }

        public async Task<List<EventMember>> Fetch()
        {
            var sql = "select * from EventMembers em " +
                      "left join MemberInfo mi on em.MemberInfoId = mi.Id " +
                      "left join Events ev on em.EventId = ev.Id ";

            var result = await _db.QueryAsync<EventMember, MemberInfo, Event, EventMember>(sql,
                (eventMember, memberInfo, eventObj) =>
                {
                    eventMember.MemberInfo = memberInfo;
                    eventMember.Event = eventObj;
                    return eventMember;
                }
            );

            return result.ToList();
        }

        public async Task<EventMember> Fetch(int id)
        {
            var sql = "select * from EventMembers em " +
                      "left join MemberInfo mi on em.MemberInfoId = mi.Id " +
                      "left join Events ev on em.EventId = ev.Id " +
                      $"where em.Id={id}";

            var result = await _db.QueryAsync<EventMember, MemberInfo, Event, EventMember>(sql,
                (eventMember, memberInfo, eventObj) =>
                {
                    eventMember.MemberInfo = memberInfo;
                    eventMember.Event = eventObj;
                    return eventMember;
                }
            );

            return result.SingleOrDefault();
        }

        public async Task<EventMember> Insert(EventMember eventToInsert)
        {
            var sql =
                "INSERT INTO [dbo].[EventMembers] ([MemberInfoId], [EventId], [Role], [LastUpdatedBy], [LastUpdatedDate], [Notes]) " +
                "SELECT @MemberInfoId, @EventId, @Role, @LastUpdatedBy, @LastUpdatedDate, @Notes " +
                "SELECT SCOPE_IDENTITY()";
            eventToInsert.Id = await _db.ExecuteScalarAsync<int>(sql, new
            {
                MemberInfoId = eventToInsert.MemberInfo.Id,
                EventId = eventToInsert.Event.Id,
                Role = eventToInsert.Role,
                LastUpdatedBy = eventToInsert.LastUpdatedBy,
                LastUpdatedDate = eventToInsert.LastUpdatedDate,
                Notes = eventToInsert.Notes
            });

            //reretrieve EventMember to get rowversion
            var insertedEmail = await _db.GetAsync<EventMember>(eventToInsert.Id);
            eventToInsert.RowVersion = insertedEmail.RowVersion;

            return eventToInsert;
        }

        public async Task<EventMember> Update(EventMember eventToUpdate)
        {
            var sql = "UPDATE [dbo].[EventMembers] " +
                      "SET    [MemberInfoId] = @MemberInfoId, " +
                      "[EventId] = @EventId, " +
                      "[Role] = @Role, " +
                      "[LastUpdatedBy] = @LastUpdatedBy, " +
                      "[LastUpdatedDate] = @LastUpdatedDate, " +
                      "[Notes] = @Notes " +
                      "OUTPUT inserted.RowVersion " +
                      "WHERE  [Id] = @Id AND RowVersion = @RowVersion ";

            var rowVersion = await _db.ExecuteScalarAsync<byte[]>(sql, new
            {
                Id = eventToUpdate.Id,
                MemberInfoId = eventToUpdate.MemberInfo.Id,
                EventId = eventToUpdate.Event.Id,
                Role = eventToUpdate.Role,
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
            await _db.DeleteAsync<EventMember>(new EventMember() {Id = id});
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}