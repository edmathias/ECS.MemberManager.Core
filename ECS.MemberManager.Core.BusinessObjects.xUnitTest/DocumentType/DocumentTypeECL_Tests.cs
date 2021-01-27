﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Csla.Rules;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class DocumentTypeECL_Tests 
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public DocumentTypeECL_Tests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var testLibrary = _config.GetValue<string>("TestLibrary");
            
            if(testLibrary == "Mock")
                MockDb.ResetMockDb();
            else
            {
                if (!IsDatabaseBuilt)
                {
                    var adoDb = new ADODb();
                    adoDb.BuildMemberManagerADODb();
                    IsDatabaseBuilt = true;
                }
            }
        }
        
        [Fact]
        private async void DocumentTypeECL_TestDocumentTypeECL()
        {
            var documentTypeEdit = await DocumentTypeECL.NewDocumentTypeECL();

            Assert.NotNull(documentTypeEdit);
            Assert.IsType<DocumentTypeECL>(documentTypeEdit);
        }

        
        [Fact]
        private async void DocumentTypeECL_TestGetDocumentTypeECL()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IDocumentTypeDal>();
            var childData = await dal.Fetch();
            
            var listToTest = await DocumentTypeECL.GetDocumentTypeECL(childData);
            
            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }
        
        [Fact]
        private async void DocumentTypeECL_TestDeleteDocumentTypeEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IDocumentTypeDal>();
            var childData = await dal.Fetch();
            
            var documentTypeEditList = await DocumentTypeECL.GetDocumentTypeECL(childData);

            var documentType = documentTypeEditList.First(a => a.Id == 99);

            // remove is deferred delete
            documentTypeEditList.Remove(documentType); 

            var documentTypeListAfterDelete = await documentTypeEditList.SaveAsync();
            
            Assert.NotEqual(childData.Count,documentTypeListAfterDelete.Count);
        }

        [Fact]
        private async void DocumentTypeECL_TestUpdateDocumentTypeEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IDocumentTypeDal>();
            var childData = await dal.Fetch();
            
            var documentTypeList = await DocumentTypeECL.GetDocumentTypeECL(childData);
            var countBeforeUpdate = documentTypeList.Count;
            var idToUpdate = documentTypeList.Min(a => a.Id);
            var documentTypeToUpdate = documentTypeList.First(a => a.Id == idToUpdate);

            documentTypeToUpdate.Description = "This was updated";
            await documentTypeList.SaveAsync();

            var updatedList = await dal.Fetch();
            var updatedDocumentTypesList = await DocumentTypeECL.GetDocumentTypeECL(updatedList);
            
            Assert.Equal("This was updated",updatedDocumentTypesList.First(a => a.Id == idToUpdate).Description);
            Assert.Equal(countBeforeUpdate, updatedDocumentTypesList.Count);
        }

        [Fact]
        private async void DocumentTypeECL_TestAddDocumentTypeEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IDocumentTypeDal>();
            var childData = await dal.Fetch();

            var documentTypeList = await DocumentTypeECL.GetDocumentTypeECL(childData);
            var countBeforeAdd = documentTypeList.Count;
            
            var documentTypeToAdd = documentTypeList.AddNew();
            BuildDocumentType(documentTypeToAdd); 

            var documentTypeEditList = await documentTypeList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, documentTypeEditList.Count);
        }

        private void BuildDocumentType(DocumentTypeEC documentType)
        {
            documentType.Description = "doc type description";
            documentType.Notes = "document type notes";
            documentType.LastUpdatedBy = "edm";
            documentType.LastUpdatedDate = DateTime.Now;
        }
        
    }
}
