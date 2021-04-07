using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class DocumentTypeDal : IDal<DocumentType> 
    {
        public async Task<List<DocumentType>> Fetch()
        {
            List<DocumentType> list;

            using (var context = new MembershipManagerDataContext())
            {
                list = await context.DocumentTypes.ToListAsync();
            }

            return list;
        }

        public async Task<DocumentType> Fetch(int id)
        {
            List<DocumentType> list = null;

            DocumentType documentType = null;

            using (var context = new MembershipManagerDataContext())
            {
                documentType = await context.DocumentTypes.Where(a => a.Id == id).FirstAsync();
            }

            return documentType;
        }

        public async Task<DocumentType> Insert(DocumentType documentTypeToInsert)
        {
            using (var context = new MembershipManagerDataContext())
            {
                await context.DocumentTypes.AddAsync(documentTypeToInsert);
                await context.SaveChangesAsync();
            }

            ;

            return documentTypeToInsert;
        }

        public async Task<DocumentType> Update(DocumentType documentTypeToUpdate)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Update(documentTypeToUpdate);
                await context.SaveChangesAsync();
            }

            return documentTypeToUpdate;
        }

        public async Task Delete(int id)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Remove(await context.DocumentTypes.SingleAsync(a => a.Id == id));
                await context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
        }
    }
}