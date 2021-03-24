using System.IO;
using System.Linq;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PaymentTypeERL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public PaymentTypeERL_Tests()
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
        private async void PaymentTypeERL_TestNewPaymentTypeERL()
        {
            var paymentTypeEdit = await PaymentTypeERL.NewPaymentTypeERL();

            Assert.NotNull(paymentTypeEdit);
            Assert.IsType<PaymentTypeERL>(paymentTypeEdit);
        }

        [Fact]
        private async void PaymentTypeERL_TestGetPaymentTypeERL()
        {
            var paymentTypeEdit =
                await PaymentTypeERL.GetPaymentTypeERL();

            Assert.NotNull(paymentTypeEdit);
            Assert.Equal(3, paymentTypeEdit.Count);
        }

        [Fact]
        private async void PaymentTypeERL_TestDeletePaymentTypeERL()
        {
            const int ID_TO_DELETE = 99;
            var categoryList =
                await PaymentTypeERL.GetPaymentTypeERL();
            var listCount = categoryList.Count;
            var categoryToDelete = categoryList.First(cl => cl.Id == ID_TO_DELETE);
            // remove is deferred delete
            var isDeleted = categoryList.Remove(categoryToDelete);

            var paymentTypeListAfterDelete = await categoryList.SaveAsync();

            Assert.NotNull(paymentTypeListAfterDelete);
            Assert.IsType<PaymentTypeERL>(paymentTypeListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount, paymentTypeListAfterDelete.Count);
        }

        [Fact]
        private async void PaymentTypeERL_TestUpdatePaymentTypeERL()
        {
            const int ID_TO_UPDATE = 1;
            const string NOTES_UPDATE = "Updated Notes";

            var categoryList =
                await PaymentTypeERL.GetPaymentTypeERL();
            var paymentTypeToUpdate = categoryList.First(cl => cl.Id == ID_TO_UPDATE);
            paymentTypeToUpdate.Notes = NOTES_UPDATE;

            var updatedList = await categoryList.SaveAsync();
            var updatedPaymentType = updatedList.First(el => el.Id == ID_TO_UPDATE);

            Assert.NotNull(updatedList);
            Assert.NotNull(updatedPaymentType);
            Assert.Equal(NOTES_UPDATE, updatedPaymentType.Notes);
        }

        [Fact]
        private async void PaymentTypeERL_TestAddPaymentTypeERL()
        {
            var categoryList =
                await PaymentTypeERL.GetPaymentTypeERL();
            var countBeforeAdd = categoryList.Count;

            var paymentTypeToAdd = categoryList.AddNew();
            BuildPaymentType(paymentTypeToAdd);

            var updatedCategoryList = await categoryList.SaveAsync();

            Assert.NotEqual(countBeforeAdd, updatedCategoryList.Count);
        }

        private void BuildPaymentType(PaymentTypeEC categoryToBuild)
        {
            categoryToBuild.Description = "description for doctype";
            categoryToBuild.Notes = "notes for doctype";
        }
    }
}