using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class AddressDal : IAddressDal
    {

        private readonly MembershipManagerDataContext context;

        public AddressDal(MembershipManagerDataContext _context)
        {
            context = _context;
        }

        public async Task<List<Address>> Fetch()
        {
            List<Address> list;
            
            list = await context.Addresses.ToListAsync();
            
            return list;
        }

        public async Task<Address> Fetch(int id)
        {
            Address address = null;
            
            address = await context.Addresses.Where(a => a.Id == id).FirstAsync();

            return address;
        }

        public async Task<Address> Insert(Address addressToInsert)
        {
/*            Address result = null;
            
            using (var context = new MembershipManagerDataContext())
            {
                await context.Addresses.AddAsync(addressToInsert);
                await context.SaveChangesAsync();

                result = await context.Addresses.Where(addr => addr.Id == insertedId).FirstAsync();
            };
            
            return result;
*/
            return null;
        }

        public async Task<Address> Update(Address addressToUpdate)
        {
 /*           var sql = "UPDATE Addresses " +
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
*/
            return null;
        }

        public async Task Delete(int id)
        {
 //           await _db.DeleteAsync<Address>(new Address() {Id = id});
        }
        
        public void Dispose()
        {
        }
    }
}