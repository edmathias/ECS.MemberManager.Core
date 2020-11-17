using System.Collections.Generic;
using System.Data;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface ITitleDal
    {
        IList<Title> Fetch();
        Title Fetch(int id);
        void Insert(Title dto);
        void Update(Title dto);
        void Delete(Title dto);
        
    }
}