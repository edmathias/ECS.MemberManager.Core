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
    public class MembershipTypeDal : IDal<MembershipType>
    {
        private static IConfigurationRoot _config;
        private SqlConnection _db = null;

        public MembershipTypeDal()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var cnxnString = _config.GetConnectionString("LocalDbConnection");

            _db = new SqlConnection(cnxnString);
        }

        public MembershipTypeDal(SqlConnection cnxn)
        {
            _db = cnxn;
        }

        public async Task<List<MembershipType>> Fetch()
        {
            var membershipTypeTypes = await _db.GetAllAsync<MembershipType>();
            return membershipTypeTypes.ToList();
        }

        public async Task<MembershipType> Fetch(int id)
        {
            return await _db.GetAsync<MembershipType>(id);
        }

        public async Task<MembershipType> Insert(MembershipType membershipTypeToInsert)
        {
            var sql =
                "INSERT INTO [dbo].[MembershipTypes] ([Description], [Level], [LastUpdatedBy], [LastUpdatedDate], [Notes]) " +
                "SELECT @Description, @Level, @LastUpdatedBy, @LastUpdatedDate, @Notes " +
                "SELECT SCOPE_IDENTITY()";

            membershipTypeToInsert.Id = await _db.ExecuteScalarAsync<int>(sql, membershipTypeToInsert);

            var insertedEmail = await _db.GetAsync<MembershipType>(membershipTypeToInsert.Id);
            membershipTypeToInsert.RowVersion = insertedEmail.RowVersion;

            return membershipTypeToInsert;
        }

        public async Task<MembershipType> Update(MembershipType membershipTypeToUpdate)
        {
            var sql = "UPDATE [dbo].[MembershipTypes] " +
                      "SET [Description] = @Description, " +
                      "[Level] = @Level, " +
                      "[LastUpdatedBy] = @LastUpdatedBy, " +
                      "[LastUpdatedDate] = @LastUpdatedDate, " +
                      "[Notes] = @Notes " +
                      "OUTPUT inserted.RowVersion " +
                      "WHERE Id = @Id AND RowVersion = @RowVersion ";

            var rowVersion = await _db.ExecuteScalarAsync<byte[]>(sql, membershipTypeToUpdate);
            if (rowVersion == null)
                throw new DBConcurrencyException("Entity has been updated since last read. Try again!");
            membershipTypeToUpdate.RowVersion = rowVersion;

            return membershipTypeToUpdate;
        }

        public async Task Delete(int id)
        {
            await _db.DeleteAsync<MembershipType>(new MembershipType() {Id = id});
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}