using System.Collections.Generic;
using System.Linq;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Csla.Data.EntityFrameworkCore;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.DataAccess.SqlEF
{
    public class TitleDal : ITitleDal
    {
        public IList<Title> Fetch()
        {
            List<Title> listOfTitles;
            
            using(var dbContext = DbContextManager<MembershipManagerDataContext>.GetManager())
            {
                listOfTitles = dbContext.DbContext.Titles.ToList();
            }

            return listOfTitles;
        }
        
        
        public Title Fetch(int id)
        {
            Title title;
            using (var dbContext = DbContextManager<MembershipManagerDataContext>.GetManager())
            {
                title = dbContext.DbContext.Titles.Find(id);
            }

            return title;
        }

        public void Insert(Title dto)
        {
            using (var dbContext = DbContextManager<MembershipManagerDataContext>.GetManager("MembershipDb"))
            {
                dbContext.DbContext.Titles.Add(dto);
            }

        }

        public void Update(Title dto)
        {
            using (var dbContext = DbContextManager<MembershipManagerDataContext>.GetManager("MembershipDb"))
            {
                dbContext.DbContext.Titles.Update(dto);
            }
        }

        public void Delete(Title dto)
        {
            using (var dbContext = DbContextManager<MembershipManagerDataContext>.GetManager("MembershipDb"))
            {
                dbContext.DbContext.Titles.Remove(dto);
            }
        }
    }
}