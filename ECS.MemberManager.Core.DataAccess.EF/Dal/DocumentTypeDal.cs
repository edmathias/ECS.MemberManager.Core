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
        private MembershipManagerDataContext _context;

        public DocumentTypeDal()
        {
            _context = new MembershipManagerDataContext();
        }

        public DocumentTypeDal(MembershipManagerDataContext context)
        {
            _context = context;
        }

        public async Task<List<DocumentType>> Fetch()
        {
            return await _context.DocumentTypes.ToListAsync();
        }

        public async Task<DocumentType> Fetch(int id)
        {
            return await _context.DocumentTypes.Where(a => a.Id == id).FirstAsync();
        }

        public async Task<DocumentType> Insert(DocumentType documentTypeToInsert)
        {
            await _context.DocumentTypes.AddAsync(documentTypeToInsert);
            await _context.SaveChangesAsync();

            return documentTypeToInsert;
        }

        public async Task<DocumentType> Update(DocumentType documentTypeToUpdate)
        {
            _context.Update(documentTypeToUpdate);
            await _context.SaveChangesAsync();

            return documentTypeToUpdate;
        }

        public async Task Delete(int id)
        {
            _context.Remove(await _context.DocumentTypes.SingleAsync(a => a.Id == id));
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
        }
    }
}