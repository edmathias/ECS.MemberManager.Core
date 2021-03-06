﻿using System;
using System.IO;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EventDocumentECL_Tests : CslaBaseTest
    {
        [Fact]
        private async void EventDocumentECL_TestEventDocumentECL()
        {
            var eventDocumentObjEdit = await EventDocumentECL.NewEventDocumentECL();

            Assert.NotNull(eventDocumentObjEdit);
            Assert.IsType<EventDocumentECL>(eventDocumentObjEdit);
        }


        [Fact]
        private async void EventDocumentECL_TestGetEventDocumentECL()
        {
            var childData = MockDb.EventDocuments ;

            var listToTest = await EventDocumentECL.GetEventDocumentECL(childData);

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }

        private async Task BuildEventDocument(EventDocumentEC eventDocumentObj)
        {
            eventDocumentObj.DocumentName = "eventDocument name";
            eventDocumentObj.Event = await EventEC.GetEventEC(new Event() {Id = 1});
            eventDocumentObj.DocumentType = await DocumentTypeEC.GetDocumentTypeEC(new DocumentType() {Id = 1});
            eventDocumentObj.Notes = "eventDocument notes";
            eventDocumentObj.PathAndFileName = "C:\\pathandfilename";
            eventDocumentObj.LastUpdatedBy = "edm";
            eventDocumentObj.LastUpdatedDate = DateTime.Now;
            eventDocumentObj.Notes = "notes for eventdocument";
        }
    }
}