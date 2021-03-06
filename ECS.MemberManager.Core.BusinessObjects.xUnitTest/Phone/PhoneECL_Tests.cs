﻿using System;
using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PhoneECL_Tests : CslaBaseTest
    {
        [Fact]
        private async void PhoneECL_TestPhoneECL()
        {
            var memberStatusEdit = await PhoneECL.NewPhoneECL();

            Assert.NotNull(memberStatusEdit);
            Assert.IsType<PhoneECL>(memberStatusEdit);
        }


        [Fact]
        private async void PhoneECL_TestGetPhoneECL()
        {
            var childData = MockDb.Phones;

            var listToTest = await PhoneECL.GetPhoneECL(childData);

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }


        private void BuildPhone(PhoneEC phone)
        {
            phone.PhoneType = "mobile";
            phone.AreaCode = "303";
            phone.Number = "555-2368";
            phone.Extension = "123";
            phone.DisplayOrder = 1;
            phone.LastUpdatedBy = "Hank";
            phone.LastUpdatedDate = DateTime.Now;
            phone.Notes = "notes for phone";
        }
    }
}