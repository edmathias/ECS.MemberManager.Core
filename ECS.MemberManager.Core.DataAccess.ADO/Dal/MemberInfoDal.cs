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
    public class MemberInfoDal : IMemberInfoDal
    {
        private static IConfigurationRoot _config;
        private SqlConnection _db = null;

        public MemberInfoDal()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var cnxnString = _config.GetConnectionString("LocalDbConnection");

            _db = new SqlConnection(cnxnString);
        }

        public MemberInfoDal(SqlConnection cnxn)
        {
            _db = cnxn;
        }

        public async Task<List<MemberInfo>> Fetch()
        {
            var sql = "select * from MemberInfo m " +
                      "left join Persons p on m.PersonId = p.Id " +
                      "left join PrivacyLevels pl on m.PrivacyLevelId = pl.Id " +
                      "left join MemberStatuses stat on m.MemberStatusId = stat.Id " +
                      "left join MembershipTypes typ on m.MembershipTypeId = typ.Id";
            var result =
                await _db.QueryAsync<MemberInfo, Person, PrivacyLevel, MemberStatus, MembershipType, MemberInfo>(sql,
                    (member, person, privacy, memberStatus, memberType) =>
                    {
                        member.Person = person;
                        member.PrivacyLevel = privacy;
                        member.MemberStatus = memberStatus;
                        member.MembershipType = memberType;

                        return member;
                    }
                );

            return result.ToList();
        }

        public async Task<MemberInfo> Fetch(int id)
        {
            var sql = "select * from MemberInfo m " +
                      "left join Persons p on m.PersonId = p.Id " +
                      "left join PrivacyLevels pl on m.PrivacyLevelId = pl.Id " +
                      "left join MemberStatuses stat on m.MemberStatusId = stat.Id " +
                      "left join MembershipTypes typ on m.MembershipTypeId = typ.Id " +
                      $"where m.Id = {id}";

            var result =
                await _db.QueryAsync<MemberInfo, Person, PrivacyLevel, MemberStatus, MembershipType, MemberInfo>(sql,
                    (member, person, privacy, memberStatus, memberType) =>
                    {
                        member.Person = person;
                        member.PrivacyLevel = privacy;
                        member.MemberStatus = memberStatus;
                        member.MembershipType = memberType;

                        return member;
                    }
                );

            return result.First();
        }

        public async Task<MemberInfo> Insert(MemberInfo memberInfoToInsert)
        {
            var sql =
                "INSERT INTO [dbo].[MemberInfo] ([PersonId], [MemberNumber], [DateFirstJoined], [PrivacyLevelId], [MemberStatusId], [MembershipTypeId], [LastUpdatedBy], [LastUpdatedDate], [Notes]) " +
                "SELECT @PersonId, @MemberNumber, @DateFirstJoined, @PrivacyLevelId, @MemberStatusId, @MembershipTypeId, @LastUpdatedBy, @LastUpdatedDate, @Notes " +
                "SELECT SCOPE_IDENTITY()";
            memberInfoToInsert.Id = await _db.ExecuteScalarAsync<int>(sql, new
            {
                Id = memberInfoToInsert.Id,
                PersonId = memberInfoToInsert.Person.Id,
                MemberNumber = memberInfoToInsert.MemberNumber,
                DateFirstJoined = memberInfoToInsert.DateFirstJoined,
                PrivacyLevelId = memberInfoToInsert.PrivacyLevel.Id,
                MemberStatusId = memberInfoToInsert.MemberStatus.Id,
                MembershipTypeId = memberInfoToInsert.MembershipType.Id,
                LastUpdatedBy = memberInfoToInsert.LastUpdatedBy,
                LastUpdatedDate = memberInfoToInsert.LastUpdatedDate,
                Notes = memberInfoToInsert.Notes,
                RowVersion = memberInfoToInsert.RowVersion
            });

            //reretrieve MemberInfo to get rowversion
            var insertedMemberInfo = await _db.GetAsync<MemberInfo>(memberInfoToInsert.Id);
            memberInfoToInsert.RowVersion = insertedMemberInfo.RowVersion;

            return memberInfoToInsert;
        }

        public async Task<MemberInfo> Update(MemberInfo memberInfoToUpdate)
        {
            var sql = "UPDATE [dbo].[MemberInfo] " +
                      "SET [PersonId] = @PersonId, " +
                      "[MemberNumber] = @MemberNumber, " +
                      "[DateFirstJoined] = @DateFirstJoined, " +
                      "[PrivacyLevelId] = @PrivacyLevelId, " +
                      "[MemberStatusId] = @MemberStatusId, " +
                      "[MembershipTypeId] = @MembershipTypeId, " +
                      "[LastUpdatedBy] = @LastUpdatedBy, " +
                      "[LastUpdatedDate] = @LastUpdatedDate, " +
                      "[Notes] = @Notes " +
                      "OUTPUT inserted.RowVersion " +
                      "WHERE Id = @Id AND RowVersion = @RowVersion ";

            var rowVersion = await _db.ExecuteScalarAsync<byte[]>(sql, new
            {
                Id = memberInfoToUpdate.Id,
                PersonId = memberInfoToUpdate.Person.Id,
                MemberNumber = memberInfoToUpdate.MemberNumber,
                DateFirstJoined = memberInfoToUpdate.DateFirstJoined,
                PrivacyLevelId = memberInfoToUpdate.PrivacyLevel.Id,
                MemberStatusId = memberInfoToUpdate.MemberStatus.Id,
                MembershipTypeId = memberInfoToUpdate.MembershipType.Id,
                LastUpdatedBy = memberInfoToUpdate.LastUpdatedBy,
                LastUpdatedDate = memberInfoToUpdate.LastUpdatedDate,
                Notes = memberInfoToUpdate.Notes,
                RowVersion = memberInfoToUpdate.RowVersion
            });

            if (rowVersion == null)
                throw new DBConcurrencyException("Entity has been updated since last read. Try again!");
            memberInfoToUpdate.RowVersion = rowVersion;

            return memberInfoToUpdate;
        }

        public async Task Delete(int id)
        {
            await _db.DeleteAsync<MemberInfo>(new() {Id = id});
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}