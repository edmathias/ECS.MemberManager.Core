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
    public class PaymentDal : IPaymentDal
    {
        private static IConfigurationRoot _config;
        private SqlConnection _db = null;

        public PaymentDal()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var cnxnString = _config.GetConnectionString("LocalDbConnection");
            
            _db = new SqlConnection(cnxnString);
        }

        public PaymentDal(SqlConnection cnxn)
        {
            _db = cnxn;
        }
        
        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task<List<Payment>> Fetch()
        {
            var titles =await _db.GetAllAsync<Payment>();
            return titles.ToList();
        }

        public async Task<Payment> Fetch(int id)
        {
            return await _db.GetAsync<Payment>(id);
        }

        public async Task<Payment> Insert(Payment paymentToInsert)
        {
            var sql = "INSERT INTO [dbo].[Payments] ([PersonId], [Amount], [PaymentDate], [PaymentExpirationDate], "+
                        "[PaymentSourceId], [PaymentTypeId], [LastUpdatedBy], [LastUpdatedDate], [Notes]) "+
                        "SELECT @PersonId, @Amount, @PaymentDate, @PaymentExpirationDate, @PaymentSourceId, "+
                        "@PaymentTypeId, @LastUpdatedBy, @LastUpdatedDate, @Notes " +
                        "SELECT SCOPE_IDENTITY()";
            paymentToInsert.Id = await _db.ExecuteScalarAsync<int>(sql,paymentToInsert);

            //reretrieve payment to get rowversion
            var insertedOffice = await _db.GetAsync<Payment>(paymentToInsert.Id);
            paymentToInsert.RowVersion = insertedOffice.RowVersion;
            
            return paymentToInsert;            
        }

        public async Task<Payment> Update(Payment paymentToUpdate)
        {
            var sql = "UPDATE [dbo].[Payments] " +
                            "SET [PersonId] = @PersonId, " +
                        "[Amount] = @Amount, "+
                        "[PaymentDate] = @PaymentDate, "+
                        "[PaymentExpirationDate] = @PaymentExpirationDate, "+
                        "[PaymentSourceId] = @PaymentSourceId, "+
                        "[PaymentTypeId] = @PaymentTypeId, "+
                        "[LastUpdatedBy] = @LastUpdatedBy, "+
                        "[LastUpdatedDate] = @LastUpdatedDate, "+
                        "[Notes] = @Notes "+
                        "OUTPUT inserted.RowVersion " +
                        "WHERE  Id = @Id AND RowVersion = @RowVersion ";

            var rowVersion = await _db.ExecuteScalarAsync<byte[]>(sql,paymentToUpdate);
            if (rowVersion == null)
                throw new DBConcurrencyException("Entity has been updated since last read. Try again!");
            paymentToUpdate.RowVersion = rowVersion;
            
            return paymentToUpdate;
        }

        public async Task Delete(int id)
        {
            await _db.DeleteAsync<Payment>(new () {Id = id});
        }
    }
}