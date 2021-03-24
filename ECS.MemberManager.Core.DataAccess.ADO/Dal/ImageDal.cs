using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ECS.MemberManager.Core.DataAccess.ADO
{
    public class ImageDal : IImageDal
    {
        private static IConfigurationRoot _config;
        private SqlConnection _db = null;

        public ImageDal()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var cnxnString = _config.GetConnectionString("LocalDbConnection");

            _db = new SqlConnection(cnxnString);
        }

        public ImageDal(SqlConnection cnxn)
        {
            _db = cnxn;
        }

        public async Task<List<Image>> Fetch()
        {
            var sql = "select * from Images tio ";

            var result = await _db.QueryAsync<Image>(sql);


            return result.ToList();
        }

        public async Task<Image> Fetch(int id)
        {
            var sql = $"select * from Images tio WHERE tio.Id = {id}";

            var result = await _db.QueryAsync<Image>(sql);

            return result.FirstOrDefault();
        }

        public async Task<Image> Insert(Image imageToInsert)
        {
            var sql =
                "INSERT INTO [dbo].[Images] ([PersonId], [OfficeId], [StartDate], [LastUpdatedBy], [LastUpdatedDate], [Notes]) " +
                "SELECT @PersonId, @OfficeId, @StartDate, @LastUpdatedBy, @LastUpdatedDate, @Notes " +
                "SELECT SCOPE_IDENTITY()";
            imageToInsert.Id = await _db.ExecuteScalarAsync<int>(sql, new
            {
                Id = imageToInsert.Id,
                ImagePath = imageToInsert.ImagePath
            });

            //reretrieve Image to get rowversion
            var insertedEmail = await _db.GetAsync<Image>(imageToInsert.Id);
            imageToInsert.RowVersion = insertedEmail.RowVersion;

            return imageToInsert;
        }

        public async Task<Image> Update(Image imageToInsert)
        {
            var sql = "UPDATE [dbo].[Images] " +
                      "SET [ImagePath] = @ImagePath " +
                      "[ImageFile] = @ImageFile " +
                      "OUTPUT inserted.RowVersion " +
                      "WHERE  [Id] = @Id AND RowVersion = @RowVersion ";

            var rowVersion = await _db.ExecuteScalarAsync<byte[]>(sql, new
            {
                Id = imageToInsert.Id,
                ImagePath = imageToInsert.ImagePath,
                ImageFile = imageToInsert.ImageFile,
                RowVersion = imageToInsert.RowVersion
            });

            if (rowVersion == null)
                throw new DBConcurrencyException("Entity has been updated since last read. Try again!");
            imageToInsert.RowVersion = rowVersion;

            return imageToInsert;
        }

        public async Task Delete(int id)
        {
            await _db.DeleteAsync<Image>(new Image() {Id = id});
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}