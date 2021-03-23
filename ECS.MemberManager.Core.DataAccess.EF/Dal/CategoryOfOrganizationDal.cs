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
    public class CategoryOfOrganizationDal : ICategoryOfOrganizationDal
    {

        public async Task<List<CategoryOfOrganization>> Fetch()
        {
            List<CategoryOfOrganization> list;

            using (var context = new MembershipManagerDataContext())
            {
                list = await context.CategoryOfOrganizations.ToListAsync();
            }
            
            return list;
        }

        public async Task<CategoryOfOrganization> Fetch(int id)
        {
            List<CategoryOfOrganization> list = null;
            
            CategoryOfOrganization categoryOfOrganization = null;
            
            using (var context = new MembershipManagerDataContext())
            {
                categoryOfOrganization = await context.CategoryOfOrganizations.Where(a => a.Id == id).FirstAsync();
            }

            return categoryOfOrganization;
        }

        public async Task<CategoryOfOrganization> Insert(CategoryOfOrganization categoryOfOrganizationToInsert)
        {
            using (var context = new MembershipManagerDataContext())
            {
                await context.CategoryOfOrganizations.AddAsync(categoryOfOrganizationToInsert);
                await context.SaveChangesAsync();
            };
            
            return categoryOfOrganizationToInsert;
        }

        public async Task<CategoryOfOrganization> Update(CategoryOfOrganization categoryOfOrganizationToUpdate)
        {
            using (var context = new MembershipManagerDataContext())
            {
                 context.Update(categoryOfOrganizationToUpdate);
                 await context.SaveChangesAsync();
            }

            return categoryOfOrganizationToUpdate;
        }

        public async Task Delete(int id)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Remove(await context.CategoryOfOrganizations.SingleAsync(a => a.Id == id));
                await context.SaveChangesAsync();
            }
        }
        
        public void Dispose()
        {
        }
    }
}