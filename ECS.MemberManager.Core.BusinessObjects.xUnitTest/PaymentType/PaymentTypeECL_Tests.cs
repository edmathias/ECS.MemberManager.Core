﻿using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PaymentTypeECL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public PaymentTypeECL_Tests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
            var testLibrary = _config.GetValue<string>("TestLibrary");

            if (testLibrary == "Mock")
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
        private async void PaymentTypeECL_TestPaymentTypeECL()
        {
            var paymentTypeEdit = await PaymentTypeECL.NewPaymentTypeECL();

            Assert.NotNull(paymentTypeEdit);
            Assert.IsType<PaymentTypeECL>(paymentTypeEdit);
        }


        [Fact]
        private async void PaymentTypeECL_TestGetPaymentTypeECL()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPaymentTypeDal>();
            var childData = await dal.Fetch();

            var listToTest = await PaymentTypeECL.GetPaymentTypeECL(childData);

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }

        private void BuildPaymentType(PaymentTypeEC paymentType)
        {
            paymentType.Description = "doc type description";
            paymentType.Notes = "document type notes";
        }
    }
}