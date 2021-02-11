using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ECS.MemberManager.Core.DataAccess.ADO
{
    public class EventDocumentDal : IEventDocumentDal
    {
        private static IConfigurationRoot _config;
        private SqlConnection _db = null;
        
        public EventDocumentDal()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var cnxnString = _config.GetConnectionString("LocalDbConnection");
            
            _db = new SqlConnection(cnxnString);
        }

        public EventDocumentDal(SqlConnection cnxn)
        {
            _db = cnxn;
        }
        
        public async Task<List<EventDocument>> Fetch()
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT ed.*,dt.*,ev.* FROM EventDocuments ed");
            sql.AppendLine("INNER JOIN DocumentTypes dt ON dt.Id = ed.DocumentTypeId");
            sql.AppendLine("INNER JOIN Events ev on ev.id = ed.EventId");

            var result = await _db.QueryAsync<EventDocument,DocumentType,Event,EventDocument>(sql.ToString(),
                (eventDocument, documentType, eventObj) =>
                {
                    eventDocument.Event = eventObj;
                    eventDocument.DocumentType = documentType;

                    return eventDocument;
                }
            );

            return result.ToList();

        }

        public async Task<EventDocument> Fetch(int id)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT ed.*,dt.*,ev.* FROM   EventDocuments ed");
            sql.AppendLine("INNER JOIN DocumentTypes dt ON dt.Id = ed.DocumentTypeId");
            sql.AppendLine("INNER JOIN Events ev on ev.Id = ed.EventId");
            sql.AppendLine($"WHERE ed.Id={id}");

            var result = await _db.QueryAsync<EventDocument,DocumentType,Event,EventDocument>(sql.ToString(),
                (eventDocument, documentType, eventObj) =>
                {
                    eventDocument.Event = eventObj;
                    eventDocument.DocumentType = documentType;

                    return eventDocument;
                }
            );

            return result.First();
        }

        public async Task<EventDocument> Insert(EventDocument eventDocumentToInsert)
        {
            var sql =
                "INSERT INTO EventDocuments (EventId, DocumentName, DocumentTypeId, PathAndFileName, LastUpdatedBy, LastUpdatedDate, Notes) " +
                "SELECT @EventId, @DocumentName, @DocumentTypeId, @PathAndFileName, @LastUpdatedBy, @LastUpdatedDate, @Notes " +
                "SELECT SCOPE_IDENTITY()";
            
            eventDocumentToInsert.Id = await _db.ExecuteScalarAsync<int>(sql,new
            {
                EventId = eventDocumentToInsert.Event.Id,
                DocumentName = eventDocumentToInsert.DocumentName,
                DocumentTypeId = eventDocumentToInsert.DocumentType.Id,
                PathAndFileName = eventDocumentToInsert.PathAndFileName,
                LastUpdatedBy = eventDocumentToInsert.LastUpdatedBy,
                LastUpdatedDate = eventDocumentToInsert.LastUpdatedDate,
                Notes = eventDocumentToInsert.Notes,
                RowVersion = eventDocumentToInsert.RowVersion
            });

            //reretrieve EventDocument to get rowversion
            var insertedEmail = await _db.GetAsync<EventDocument>(eventDocumentToInsert.Id);
            eventDocumentToInsert.RowVersion = insertedEmail.RowVersion;
            
            return eventDocumentToInsert;
        }

        public async Task<EventDocument> Update(EventDocument eventDocumentToUpdate)
        {
            var sql = "UPDATE [dbo].[EventDocuments] " +
                      "SET [EventId] = @EventId, " +
                      "[DocumentName] = @DocumentName, " +
                      "[DocumentTypeId] = @DocumentTypeId, " +
                      "[PathAndFileName] = @PathAndFileName, " +
                      "[LastUpdatedBy] = @LastUpdatedBy, " +
                      "[LastUpdatedDate] = @LastUpdatedDate, " +
                      "[Notes] = @Notes " +
                      "OUTPUT inserted.RowVersion " +
                      "WHERE  [Id] = @Id AND RowVersion = @RowVersion";

            var rowVersion = await _db.ExecuteScalarAsync<byte[]>(sql,new
            {
                Id = eventDocumentToUpdate.Id,
                EventId = eventDocumentToUpdate.Event.Id,
                DocumentName = eventDocumentToUpdate.DocumentName,
                DocumentTypeId = eventDocumentToUpdate.DocumentType.Id,
                PathAndFileName = eventDocumentToUpdate.PathAndFileName,
                LastUpdatedBy = eventDocumentToUpdate.LastUpdatedBy,
                LastUpdatedDate = eventDocumentToUpdate.LastUpdatedDate,
                Notes = eventDocumentToUpdate.Notes,
                RowVersion = eventDocumentToUpdate.RowVersion              
            });
            if (rowVersion == null)
                throw new DBConcurrencyException("Entity has been updated since last read. Try again!");
            eventDocumentToUpdate.RowVersion = rowVersion;
            
            return eventDocumentToUpdate;
        }

        public async Task Delete(int id)
        {
            await _db.DeleteAsync<EventDocument>(new EventDocument() {Id = id});
        }
        
        public void Dispose()
        {
        }

    }
}