﻿using System.Collections;
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

        public async Task<List<Address>> Fetch()
        {
            List<Address> list;

            using (var context = new MembershipManagerDataContext())
            {
                list = await context.Addresses.ToListAsync();
            }
            
            return list;
        }

        public async Task<Address> Fetch(int id)
        {
            List<Address> list = null;
            
            Address address = null;
            
            using (var context = new MembershipManagerDataContext())
            {
                address = await context.Addresses.Where(a => a.Id == id).FirstAsync();
            }

            return address;
        }

        public async Task<Address> Insert(Address addressToInsert)
        {
            using (var context = new MembershipManagerDataContext())
            {
                await context.Addresses.AddAsync(addressToInsert);
                await context.SaveChangesAsync();
            };
            
            return addressToInsert;
        }

        public async Task<Address> Update(Address addressToUpdate)
        {
            using (var context = new MembershipManagerDataContext())
            {
                 context.Update(addressToUpdate);
                 await context.SaveChangesAsync();
            }

            return addressToUpdate;
        }

        public async Task Delete(int id)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Remove(await context.Addresses.SingleAsync(a => a.Id == id));
                await context.SaveChangesAsync();
            }
        }
        
        public void Dispose()
        {
        }
    }
}