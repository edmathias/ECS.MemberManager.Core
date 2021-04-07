using System.IO;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PaymentSourceERL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public PaymentSourceERL_Tests()
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
        private async void PaymentSourceERL_TestNewPaymentSourceERL()
        {
            var paymentSourceEdit = await PaymentSourceERL.NewPaymentSourceERL();

            Assert.NotNull(paymentSourceEdit);
            Assert.IsType<PaymentSourceERL>(paymentSourceEdit);
        }

        [Fact]
        private async void PaymentSourceERL_TestGetPaymentSourceERL()
        {
            var paymentSourceEdit =
                await PaymentSourceERL.GetPaymentSourceERL();

            Assert.NotNull(paymentSourceEdit);
            Assert.Equal(3, paymentSourceEdit.Count);
        }

        [Fact]
        private async void PaymentSourceERL_TestDeletePaymentSourceERL()
        {
            const int ID_TO_DELETE = 99;
            var categoryList =
                await PaymentSourceERL.GetPaymentSourceERL();
            var listCount = categoryList.Count;
            var categoryToDelete = categoryList.First(cl => cl.Id == ID_TO_DELETE);
            // remove is deferred delete
            var isDeleted = categoryList.Remove(categoryToDelete);

            var paymentSourceListAfterDelete = await categoryList.SaveAsync();

            Assert.NotNull(paymentSourceListAfterDelete);
            Assert.IsType<PaymentSourceERL>(paymentSourceListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount, paymentSourceListAfterDelete.Count);
        }

        [Fact]
        private async void PaymentSourceERL_TestUpdatePaymentSourceERL()
        {
            const int ID_TO_UPDATE = 1;
            const string NOTES_UPDATE = "Updated Notes";

            var categoryList =
                await PaymentSourceERL.GetPaymentSourceERL();
            var paymentSourceToUpdate = categoryList.First(cl => cl.Id == ID_TO_UPDATE);
            paymentSourceToUpdate.Notes = NOTES_UPDATE;

            var updatedList = await categoryList.SaveAsync();
            var updatedPaymentSource = updatedList.First(el => el.Id == ID_TO_UPDATE);

            Assert.NotNull(updatedList);
            Assert.NotNull(updatedPaymentSource);
            Assert.Equal(NOTES_UPDATE, updatedPaymentSource.Notes);
        }

        [Fact]
        private async void PaymentSourceERL_TestAddPaymentSourceERL()
        {
            var categoryList =
                await PaymentSourceERL.GetPaymentSourceERL();
            var countBeforeAdd = categoryList.Count;

            var paymentSourceToAdd = categoryList.AddNew();
            BuildPaymentSource(paymentSourceToAdd);

            var updatedCategoryList = await categoryList.SaveAsync();

            Assert.NotEqual(countBeforeAdd, updatedCategoryList.Count);
        }

        private void BuildPaymentSource(PaymentSourceEC categoryToBuild)
        {
            categoryToBuild.Description = "description for doctype";
            categoryToBuild.Notes = "notes for doctype";
        }
    }
}