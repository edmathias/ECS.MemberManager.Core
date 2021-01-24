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
    public class PhoneDal : IPhoneDal
    {
        private static IConfigurationRoot _config;
        private SqlConnection _db = null;

        public PhoneDal()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var cnxnString = _config.GetConnectionString("LocalDbConnection");
            
            _db = new SqlConnection(cnxnString);
        }

        public PhoneDal(SqlConnection cnxn)
        {
            _db = cnxn;
        }
        public async Task<List<Phone>> Fetch()
        {
            var phones =await _db.GetAllAsync<Phone>();
            return phones.ToList();
        }

        public async Task<Phone> Fetch(int id)
        {
            return await _db.GetAsync<Phone>(id);
        }

        public async Task<Phone> Insert(Phone phoneToInsert)
        {
            var sql = "INSERT INTO [dbo].[Phones] ([PhoneType], [AreaCode], [Number], [Extension], [DisplayOrder], [LastUpdatedBy], [LastUpdatedDate], [Notes]) "+
                            "SELECT @PhoneType, @AreaCode, @Number, @Extension, @DisplayOrder, @LastUpdatedBy, @LastUpdatedDate, @Notes; " +
                            "SELECT SCOPE_IDENTITY()";
            phoneToInsert.Id = await _db.ExecuteScalarAsync<int>(sql,phoneToInsert);

            //reretrieve Phone to get rowversion
            var insertedEmail = await _db.GetAsync<Phone>(phoneToInsert.Id);
            phoneToInsert.RowVersion = insertedEmail.RowVersion;
            
            return phoneToInsert;
        }

        public async Task<Phone> Update(Phone phoneToUpdate)
        {
            var sql = "UPDATE [dbo].[Phones] "+
                            "SET    [PhoneType] = @PhoneType, "+
                            "[AreaCode] = @AreaCode, "+
                            "[Number] = @Number, "+
                            "[Extension] = @Extension, "+
                            "[DisplayOrder] = @DisplayOrder, "+
                            "[LastUpdatedBy] = @LastUpdatedBy, "+
                            "[LastUpdatedDate] = @LastUpdatedDate, "+
                            "[Notes] = @Notes "+
                            "OUTPUT inserted.RowVersion " +
                            "WHERE Id = @Id AND RowVersion = @RowVersion ";

            var rowVersion = await _db.ExecuteScalarAsync<byte[]>(sql,phoneToUpdate);
            if (rowVersion == null)
                throw new DBConcurrencyException("Entity has been updated since last read. Try again!");
            phoneToUpdate.RowVersion = rowVersion;
            
            return phoneToUpdate;
        }

        public async Task Delete(int id)
        {
            await _db.DeleteAsync<Phone>( new () {Id = id});
        }
        
        public void Dispose()
        {
            _db.Dispose(); 
        }
    }
}