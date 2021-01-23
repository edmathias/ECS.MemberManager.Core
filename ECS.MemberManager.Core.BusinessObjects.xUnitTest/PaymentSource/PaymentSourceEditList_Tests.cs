using System;
using System.IO;
using System.Linq;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Xunit;
using DalManager = ECS.MemberManager.Core.DataAccess.ADO.DalManager;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PaymentSourceEditList_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public PaymentSourceEditList_Tests()
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
        private async void PaymentSourceEditList_TestNewPaymentSourceList()
        {
            var paymentSourceEdit = await PaymentSourceEditList.NewPaymentSourceEditList();

            Assert.NotNull(paymentSourceEdit);
            Assert.IsType<PaymentSourceEditList>(paymentSourceEdit);
        }
        
        [Fact]
        private async void PaymentSourceEditList_TestGetPaymentSourceEditList()
        {
            var paymentSourceEdit = await PaymentSourceEditList.GetPaymentSourceEditList();

            Assert.NotNull(paymentSourceEdit);
            Assert.Equal(3, paymentSourceEdit.Count);
        }
        
        [Fact]
        private async void PaymentSourceEditList_TestDeletePaymentSourcesEntry()
        {
            var paymentSourceEdit = await PaymentSourceEditList.GetPaymentSourceEditList();
            var listCount = paymentSourceEdit.Count;
            var paymentSourceToDelete = paymentSourceEdit.First(et => et.Id == 99);

            // remove is deferred delete
            var isDeleted = paymentSourceEdit.Remove(paymentSourceToDelete); 

            var paymentSourceListAfterDelete = await paymentSourceEdit.SaveAsync();

            Assert.NotNull(paymentSourceListAfterDelete);
            Assert.IsType<PaymentSourceEditList>(paymentSourceListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount,paymentSourceListAfterDelete.Count);
        }

        [Fact]
        private async void PaymentSourceEditList_TestUpdatePaymentSourcesEntry()
        {
            const int idToUpdate = 1;
            
            var paymentSourceEditList = await PaymentSourceEditList.GetPaymentSourceEditList();
            var countBeforeUpdate = paymentSourceEditList.Count;
            var paymentSourceToUpdate = paymentSourceEditList.First(a => a.Id == idToUpdate);
            paymentSourceToUpdate.Description = "This was updated";

            var updatedList = await paymentSourceEditList.SaveAsync();
            
            Assert.Equal("This was updated",updatedList.First(a => a.Id == idToUpdate).Description);
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void PaymentSourceEditList_TestAddPaymentSourcesEntry()
        {
            var paymentSourceEditList = await PaymentSourceEditList.GetPaymentSourceEditList();
            var countBeforeAdd = paymentSourceEditList.Count;
            
            var paymentSourceToAdd = paymentSourceEditList.AddNew();
            BuildPaymentSource(paymentSourceToAdd);

            var updatedPaymentSourcesList = await paymentSourceEditList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, updatedPaymentSourcesList.Count);
        }

        private void BuildPaymentSource(PaymentSourceEdit paymentSourceToBuild)
        {
            paymentSourceToBuild.Notes = "member type notes";
            paymentSourceToBuild.Description = "member type";
        }
        
 
    }
}