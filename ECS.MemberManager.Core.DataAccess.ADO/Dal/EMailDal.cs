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
    public class EMailDal : IDal<EMail>
    {
        private static IConfigurationRoot _config;
        private SqlConnection _db = null;

        public EMailDal()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var cnxnString = _config.GetConnectionString("LocalDbConnection");

            _db = new SqlConnection(cnxnString);
        }

        public EMailDal(SqlConnection cnxn)
        {
            _db = cnxn;
        }

        public async Task<List<EMail>> Fetch()
        {
            var sql = "select * from EMails inner join EMailTypes on EMails.EMailTypeId = EMailTypes.Id";
            var result = await _db.QueryAsync<EMail, EMailType, EMail>(sql,
                (eMail, eMailType) =>
                {
                    eMail.EMailType = eMailType;
                    return eMail;
                }
            );

            return result.ToList();
        }

        public async Task<EMail> Fetch(int id)
        {
            var sql = "select * from EMails inner join EMailTypes on EMails.EMailTypeId = EMailTypes.Id " +
                      $"where EMails.Id = {id}";
            var result = await _db.QueryAsync<EMail, EMailType, EMail>(sql,
                (eMail, eMailType) =>
                {
                    eMail.EMailType = eMailType;
                    return eMail;
                }
            );

            return result.First();
        }

        public async Task<EMail> Insert(EMail eMailToInsert)
        {
            var sql = "INSERT INTO EMails (EMailTypeId,EMailAddress,LastUpdatedBy,LastUpdatedDate,Notes) " +
                      "VALUES(@EMailTypeId,@EMailAddress,@LastUpdatedBy,@LastUpdatedDate,@Notes);" +
                      "SELECT SCOPE_IDENTITY()";

            eMailToInsert.Id = await _db.ExecuteScalarAsync<int>(sql,
                new
                {
                    EMailTypeId = eMailToInsert.EMailType.Id,
                    EMailAddress = eMailToInsert.EMailAddress,
                    LastUpdatedBy = eMailToInsert.LastUpdatedBy,
                    LastUpdatedDate = eMailToInsert.LastUpdatedDate,
                    Notes = eMailToInsert.Notes
                });

            var insertedEmail = await _db.GetAsync<EMail>(eMailToInsert.Id);
            eMailToInsert.RowVersion = insertedEmail.RowVersion;

            return eMailToInsert;
        }

        public async Task<EMail> Update(EMail eMailToUpdate)
        {
            var sql = "UPDATE EMails " +
                      "SET EMailTypeId=@EMailTypeId," +
                      "EMailAddress=@EMailAddress," +
                      "LastUpdatedBy=@LastUpdatedBy," +
                      "LastUpdatedDate=@LastUpdatedDate," +
                      "Notes=@Notes " +
                      "OUTPUT inserted.RowVersion " +
                      "WHERE Id = @Id AND RowVersion = @RowVersion ";

            var rowVersion = await _db.ExecuteScalarAsync<byte[]>(sql,
                new
                {
                    Id = eMailToUpdate.Id,
                    EMailTypeId = eMailToUpdate.EMailType.Id,
                    EMailAddress = eMailToUpdate.EMailAddress,
                    LastUpdatedBy = eMailToUpdate.LastUpdatedBy,
                    LastUpdatedDate = eMailToUpdate.LastUpdatedDate,
                    Notes = eMailToUpdate.Notes,
                    RowVersion = eMailToUpdate.RowVersion
                });

            if (rowVersion == null)
                throw new DBConcurrencyException("Entity has been updated since last read. Try again!");
            eMailToUpdate.RowVersion = rowVersion;

            return eMailToUpdate;
        }

        public async Task Delete(int id)
        {
            await _db.DeleteAsync<EMail>(new EMail() {Id = id});
        }


        public void Dispose()
        {
            _db.Dispose();
        }
    }
}