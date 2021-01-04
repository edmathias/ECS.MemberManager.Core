using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ECS.MemberManager.Core.DataAccess.ADO
{
    public class EMailDal : IEMailDal
    {
        private static IConfigurationRoot _config;
        private SqlConnection _db = null;

        public EMailDal()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var cnxnString = _config.GetConnectionString("LocalDbConnection");
            
            _db = new SqlConnection(cnxnString);
        }

        public List<EMail> Fetch()
        {
            return _db.GetAll<EMail>().ToList();
        }

        public EMail Fetch(int id)
        {
            return _db.Get<EMail>(id);
        }

        public EMail Insert(EMail eMailTypeToInsert)
        {
            _db.Insert(eMailTypeToInsert);
            
            return eMailTypeToInsert;
            
        }

        public EMail Update(EMail eMailToUpdate)
        {
            _db.Update<EMail>(eMailToUpdate);
            
            return eMailToUpdate;
        }

        public void Delete(int id)
        {
            _db.Delete<EMail>(new EMail() {Id = id});
        }
        
        public void Dispose()
        {
            _db.Dispose(); 
        }
    }
}