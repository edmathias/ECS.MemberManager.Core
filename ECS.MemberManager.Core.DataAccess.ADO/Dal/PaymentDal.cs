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
            var sql = "select * from Payments pmt " +
                      "inner join Persons per on pmt.PersonId = per.Id " +
                      "inner join PaymentSources ps on pmt.PaymentSourceId = ps.Id " +
                      "inner join PaymentTypes pt on pmt.PaymentTypeId = pt.Id ";
            
            var payments = await _db.QueryAsync<Payment, Person, PaymentSource,PaymentType, Payment>(sql,
                (payment, person, paymentSource, paymentType ) =>
                {
                    payment.Person = person;
                    payment.PaymentSource = paymentSource;
                    payment.PaymentType = paymentType;
                    
                    return payment;
                }
            );

            return payments.ToList();
        }

        public async Task<Payment> Fetch(int id)
        {
            var sql = "select * from Payments pmt " +
                      "inner join Persons per on pmt.PersonId = per.Id " +
                      "inner join PaymentSources ps on pmt.PaymentSourceId = ps.Id " +
                      "inner join PaymentTypes pt on pmt.PaymentTypeId = pt.Id " +
                      $"WHERE pmt.Id = {id}";
            
            var payments = await _db.QueryAsync<Payment, Person, PaymentSource,PaymentType, Payment>(sql,
                (payment, person, paymentSource, paymentType ) =>
                {
                    payment.Person = person;
                    payment.PaymentSource = paymentSource;
                    payment.PaymentType = paymentType;
                    
                    return payment;
                }
            );

            return payments.FirstOrDefault();
        }

        public async Task<Payment> Insert(Payment paymentToInsert)
        {
            var sql = "INSERT INTO [dbo].[Payments] ([PersonId], [Amount], [PaymentDate], [PaymentExpirationDate], "+
                        "[PaymentSourceId], [PaymentTypeId], [LastUpdatedBy], [LastUpdatedDate], [Notes]) "+
                        "SELECT @PersonId, @Amount, @PaymentDate, @PaymentExpirationDate, @PaymentSourceId, "+
                        "@PaymentTypeId, @LastUpdatedBy, @LastUpdatedDate, @Notes " +
                        "SELECT SCOPE_IDENTITY()";
            paymentToInsert.Id = await _db.ExecuteScalarAsync<int>(sql,new
            {
                PersonId = paymentToInsert.Person.Id,
                Amount = paymentToInsert.Amount,
                PaymentDate = paymentToInsert.PaymentDate,
                PaymentExpirationDate = paymentToInsert.PaymentExpirationDate,
                PaymentSourceId = paymentToInsert.PaymentSource.Id,
                PaymentTypeId = paymentToInsert.PaymentType.Id,
                LastUpdatedBy = paymentToInsert.LastUpdatedBy,
                LastUpdatedDate = paymentToInsert.LastUpdatedDate,
                Notes = paymentToInsert.Notes
            });

            //reretrieve payment to get rowversion
            var insertedOffice = await Fetch(paymentToInsert.Id);
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

            var rowVersion = await _db.ExecuteScalarAsync<byte[]>(sql,new
            {
                Id = paymentToUpdate.Id,
                PersonId = paymentToUpdate.Person.Id,
                Amount = paymentToUpdate.Amount,
                PaymentDate = paymentToUpdate.PaymentDate,
                PaymentExpirationDate = paymentToUpdate.PaymentExpirationDate,
                PaymentSourceId = paymentToUpdate.PaymentSource.Id,
                PaymentTypeId = paymentToUpdate.PaymentType.Id,
                LastUpdatedBy = paymentToUpdate.LastUpdatedBy,
                LastUpdatedDate = paymentToUpdate.LastUpdatedDate,
                Notes = paymentToUpdate.Notes,
                RowVersion = paymentToUpdate.RowVersion
            });
            
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