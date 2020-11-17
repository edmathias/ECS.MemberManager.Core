using System;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using Microsoft.Extensions.Configuration;

namespace ECS.MemberManager.Core.UI.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITitleDal>();

            var title = dal.Fetch(1);
        }
    }
}