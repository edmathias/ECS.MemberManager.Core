using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface ITermInOfficeDal : IDisposable
    {
        Task<TermInOffice> Fetch(int id);
        Task<List<TermInOffice>> Fetch();
        Task<TermInOffice> Insert(TermInOffice person);
        Task<TermInOffice> Update(TermInOffice personToUpdate );
        Task Delete(int id);
    }
}