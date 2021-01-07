using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ECS.MemberManager.Core.DataAccess.ADO
{
    public class AddressDal : IAddressDal
    {
        private static IConfigurationRoot _config;
        private SqlConnection _db = null;

        public AddressDal()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var cnxnString = _config.GetConnectionString("LocalDbConnection");
            
            _db = new SqlConnection(cnxnString);
        }

        public AddressDal(SqlConnection cnxn)
        {
            _db = cnxn;
        }
        
        public List<Address> Fetch()
        {
            return _db.GetAll<Address>().ToList();
        }

        public Address Fetch(int id)
        {
            return _db.Get<Address>(id);
        }

        public Address Insert(Address addressToInsert)
        
        {
            var sql = "INSERT INTO Addresses (Address1, Address2, City,State,PostCode,Notes,LastUpdatedBy,LastUpdatedDate) " +
                      "VALUES(@Address1,@Address2,@City,@State,@PostCode,@Notes,@LastUpdatedBy,@LastUpdatedDate);" +
                      "SELECT SCOPE_IDENTITY()";
           
            addressToInsert.Id = _db.ExecuteScalar<int>(sql, addressToInsert);
            addressToInsert.RowVersion = _db.Get<Address>(addressToInsert.Id).RowVersion;

            return addressToInsert;
        }

        public Address Update(Address addressToUpdate)
        {
            var sql = "UPDATE Addresses " +
                      "SET Address1 = @Address1," +
                      "Address2 = @Address2," +
                      "City = @City," +
                      "State = @State," +
                      "PostCode = @PostCode," +
                      "Notes = @Notes," +
                      "LastUpdatedBy = @LastUpdatedBy," +
                      "LastUpdatedDate = @LastUpdatedDate " +
                      "OUTPUT inserted.RowVersion " +
                      "WHERE Id = @Id AND RowVersion = @RowVersion ";

            var rowVersion = _db.ExecuteScalar<byte[]>(sql, addressToUpdate);

            if (rowVersion == null)
                throw new DBConcurrencyException("Entity has been updated since last read. Try again!");
            addressToUpdate.RowVersion = rowVersion;
            
            return addressToUpdate;
        }

        public void Delete(int id)
        {
            _db.Delete<Address>(new Address() {Id = id});
        }
        
        public void Dispose()
        {
            _db.Dispose(); 
        }
    }
}