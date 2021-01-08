﻿using System.ComponentModel;
using Csla;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EMailTypeInfo_Tests
    {

        [Fact]
        public async void EMailTypeInfo_TestGetById()
        {
            var emailInfo = await EMailTypeInfo.GetEMailTypeInfo(1);
            
            Assert.NotNull(emailInfo);
            Assert.IsType<EMailTypeInfo>(emailInfo);
            Assert.Equal(1, emailInfo.Id);
        }

        [Fact]
        public async void EMailTypeInfo_TestGetChild()
        {
            const int ID_VALUE = 999;
            
            var emailType = new EMailType()
            {
                Id = ID_VALUE,
                Description = "Test email type",
                Notes = "email type notes"
            };

            var emailTypeInfo = await EMailTypeInfo.GetEMailTypeInfo(emailType);
            
            Assert.NotNull(emailTypeInfo);
            Assert.IsType<EMailTypeInfo>(emailTypeInfo);
            Assert.Equal(ID_VALUE, emailTypeInfo.Id);

        }
    }
}