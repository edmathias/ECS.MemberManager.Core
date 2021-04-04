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
    public class PaymentSourceDal : IDal<PaymentSource>
    {
        private static IConfigurationRoot _config;
        private SqlConnection _db = null;

        public PaymentSourceDal()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var cnxnString = _config.GetConnectionString("LocalDbConnection");

            _db = new SqlConnection(cnxnString);
        }

        public PaymentSourceDal(SqlConnection cnxn)
        {
            _db = cnxn;
        }

        public async Task<List<PaymentSource>> Fetch()
        {
            var paymentSources = await _db.GetAllAsync<PaymentSource>();
            return paymentSources.ToList();
        }

        public async Task<PaymentSource> Fetch(int id)
        {
            return await _db.GetAsync<PaymentSource>(id);
        }

        public async Task<PaymentSource> Insert(PaymentSource paymentSourceToInsert)
        {
            var sql = "INSERT INTO PaymentSources (Description, Notes) " +
                      "VALUES(@Description, @Notes);" +
                      "SELECT SCOPE_IDENTITY()";
            paymentSourceToInsert.Id = await _db.ExecuteScalarAsync<int>(sql, paymentSourceToInsert);

            //reretrieve PaymentSource to get rowversion
            var insertedEmail = await _db.GetAsync<PaymentSource>(paymentSourceToInsert.Id);
            paymentSourceToInsert.RowVersion = insertedEmail.RowVersion;

            return paymentSourceToInsert;
        }

        public async Task<PaymentSource> Update(PaymentSource paymentSourceToUpdate)
        {
            var sql = "UPDATE PaymentSources " +
                      "SET Description = @Description, " +
                      "Notes = @Notes " +
                      "OUTPUT inserted.RowVersion " +
                      "WHERE Id = @Id AND RowVersion = @RowVersion ";

            var rowVersion = await _db.ExecuteScalarAsync<byte[]>(sql, paymentSourceToUpdate);
            if (rowVersion == null)
                throw new DBConcurrencyException("Entity has been updated since last read. Try again!");
            paymentSourceToUpdate.RowVersion = rowVersion;

            return paymentSourceToUpdate;
        }

        public async Task Delete(int id)
        {
            await _db.DeleteAsync<PaymentSource>(new PaymentSource() {Id = id});
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}