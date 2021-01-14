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
        
        public async Task<List<Address>> Fetch()
        {
            var list = await _db.GetAllAsync<Address>();
            
            return list.ToList();
        }

        public async Task<Address> Fetch(int id)
        {
            return await _db.GetAsync<Address>(id);
        }

        public async Task<Address> Insert(Address addressToInsert)
        
        {
            var sql = "INSERT INTO Addresses (Address1, Address2, City,State,PostCode,Notes,LastUpdatedBy,LastUpdatedDate) " +
                      "VALUES(@Address1,@Address2,@City,@State,@PostCode,@Notes,@LastUpdatedBy,@LastUpdatedDate);" +
                      "SELECT SCOPE_IDENTITY()";
           
            addressToInsert.Id =  await _db.ExecuteScalarAsync<int>(sql, addressToInsert);
            var refetchedAddress = await _db.GetAsync<Address>(addressToInsert.Id);
            addressToInsert.RowVersion = refetchedAddress.RowVersion;

            return addressToInsert;
        }

        public async Task<Address> Update(Address addressToUpdate)
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

            var rowVersion = await _db.ExecuteScalarAsync<byte[]>(sql, addressToUpdate);

            if (rowVersion == null)
                throw new DBConcurrencyException("Entity has been updated since last read. Try again!");
            addressToUpdate.RowVersion = rowVersion;
            
            return addressToUpdate;
        }

        public async Task Delete(int id)
        {
            await _db.DeleteAsync<Address>(new Address() {Id = id});
        }
        
        public void Dispose()
        {
            _db.Dispose(); 
        }
    }
}