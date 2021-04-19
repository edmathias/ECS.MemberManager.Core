using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PaymentERL_Tests : CslaBaseTest
    {
        [Fact]
        private async void PaymentERL_TestNewPaymentList()
        {
            var paymentEdit = await PaymentERL.NewPaymentERL();

            Assert.NotNull(paymentEdit);
            Assert.IsType<PaymentERL>(paymentEdit);
        }

        [Fact]
        private async void PaymentERL_TestGetPaymentERL()
        {
            var paymentEdit = await PaymentERL.GetPaymentERL();

            Assert.NotNull(paymentEdit);
            Assert.Equal(3, paymentEdit.Count);
        }

        [Fact]
        private async void PaymentERL_TestDeletePaymentsEntry()
        {
            var paymentEdit = await PaymentERL.GetPaymentERL();
            var listCount = paymentEdit.Count;
            var paymentToDelete = paymentEdit.First(et => et.Id == 99);

            // remove is deferred delete
            var isDeleted = paymentEdit.Remove(paymentToDelete);

            var paymentListAfterDelete = await paymentEdit.SaveAsync();

            Assert.NotNull(paymentListAfterDelete);
            Assert.IsType<PaymentERL>(paymentListAfterDelete);
            Assert.True(isDeleted);
            Assert.NotEqual(listCount, paymentListAfterDelete.Count);
        }

        [Fact]
        private async void PaymentERL_TestUpdatePaymentsEntry()
        {
            const int idToUpdate = 1;

            var paymentEditList = await PaymentERL.GetPaymentERL();
            var countBeforeUpdate = paymentEditList.Count;
            var paymentToUpdate = paymentEditList.First(a => a.Id == idToUpdate);
            paymentToUpdate.Notes = "This was updated";

            var updatedList = await paymentEditList.SaveAsync();

            Assert.Equal("This was updated", updatedList.First(a => a.Id == idToUpdate).Notes);
            Assert.Equal(countBeforeUpdate, updatedList.Count);
        }

        [Fact]
        private async void PaymentERL_TestAddPaymentsEntry()
        {
            var paymentEditList = await PaymentERL.GetPaymentERL();
            var countBeforeAdd = paymentEditList.Count;

            var paymentToAdd = paymentEditList.AddNew();
            await BuildPayment(paymentToAdd);

            var updatedPaymentsList = await paymentEditList.SaveAsync();

            Assert.NotEqual(countBeforeAdd, updatedPaymentsList.Count);
        }

        private async Task BuildPayment(PaymentEC payment)
        {
            payment.Amount = 39.99;
            payment.Person = await PersonEC.GetPersonEC(new Person() {Id = 1});
            payment.LastUpdatedBy = "edm";
            payment.LastUpdatedDate = DateTime.Now;
            payment.Notes = "notes here";
            payment.PaymentDate = DateTime.Now;
            payment.PaymentExpirationDate = DateTime.Now;
            payment.PaymentSource = await PaymentSourceEC.GetPaymentSourceEC(new PaymentSource() {Id = 1});
            payment.PaymentType = await PaymentTypeEC.GetPaymentTypeEC(new PaymentType() {Id = 1});
        }
    }
}