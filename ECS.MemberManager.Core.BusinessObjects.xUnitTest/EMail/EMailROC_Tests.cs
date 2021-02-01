﻿using System;
using System.ComponentModel;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EMailROC_Tests
    {
        [Fact]
        public async void EMailROC_TestGetChild()
        {
            const int ID_VALUE = 1;

            var mail = BuildEMail();
            mail.Id = ID_VALUE;

            var eMail = await EMailROC.GetEMailROC(mail);
            
            Assert.NotNull(eMail);
            Assert.IsType<EMailROC>(eMail);
            Assert.Equal(eMail.Id, eMail.Id);
            Assert.Equal(eMail.EMailAddress, eMail.EMailAddress);
            Assert.Equal(eMail.Notes, eMail.Notes);
            Assert.Equal(eMail.LastUpdatedBy, eMail.LastUpdatedBy);
            Assert.Equal(eMail.LastUpdatedDate, eMail.LastUpdatedDate);
            Assert.Equal(eMail.RowVersion, eMail.RowVersion);
        }

        private EMail BuildEMail()
        {
            var eMail = new EMail();
            eMail.Id = 1;
            eMail.EMailTypeId = 1;
            eMail.EMailAddress = "email@email.com";
            eMail.LastUpdatedBy = "edm";
            eMail.LastUpdatedDate = DateTime.Now;
            eMail.Notes = "notes for doctype";

            return eMail;
        }        
        

    }
}