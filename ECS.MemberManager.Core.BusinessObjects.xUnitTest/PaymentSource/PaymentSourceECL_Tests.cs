using System.IO;
using System.Linq;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class PaymentSourceECL_Tests 
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public PaymentSourceECL_Tests()
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
        private async void PaymentSourceECL_TestPaymentSourceECL()
        {
            var paymentSourceEdit = await PaymentSourceECL.NewPaymentSourceECL();

            Assert.NotNull(paymentSourceEdit);
            Assert.IsType<PaymentSourceECL>(paymentSourceEdit);
        }

        
        [Fact]
        private async void PaymentSourceECL_TestGetPaymentSourceECL()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPaymentSourceDal>();
            var childData = await dal.Fetch();
            
            var listToTest = await PaymentSourceECL.GetPaymentSourceECL(childData);
            
            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }
        
        [Fact]
        private async void PaymentSourceECL_TestDeletePaymentSourceEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPaymentSourceDal>();
            var childData = await dal.Fetch();
            
            var paymentSourceEditList = await PaymentSourceECL.GetPaymentSourceECL(childData);

            var paymentSource = paymentSourceEditList.First(a => a.Id == 99);

            // remove is deferred delete
            paymentSourceEditList.Remove(paymentSource); 

            var paymentSourceListAfterDelete = await paymentSourceEditList.SaveAsync();
            
            Assert.NotEqual(childData.Count,paymentSourceListAfterDelete.Count);
        }

        [Fact]
        private async void PaymentSourceECL_TestUpdatePaymentSourceEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPaymentSourceDal>();
            var childData = await dal.Fetch();
            
            var paymentSourceList = await PaymentSourceECL.GetPaymentSourceECL(childData);
            var countBeforeUpdate = paymentSourceList.Count;
            var idToUpdate = paymentSourceList.Min(a => a.Id);
            var paymentSourceToUpdate = paymentSourceList.First(a => a.Id == idToUpdate);

            paymentSourceToUpdate.Description = "This was updated";
            await paymentSourceList.SaveAsync();

            var updatedList = await dal.Fetch();
            var updatedPaymentSourcesList = await PaymentSourceECL.GetPaymentSourceECL(updatedList);
            
            Assert.Equal("This was updated",updatedPaymentSourcesList.First(a => a.Id == idToUpdate).Description);
            Assert.Equal(countBeforeUpdate, updatedPaymentSourcesList.Count);
        }

        [Fact]
        private async void PaymentSourceECL_TestAddPaymentSourceEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPaymentSourceDal>();
            var childData = await dal.Fetch();

            var paymentSourceList = await PaymentSourceECL.GetPaymentSourceECL(childData);
            var countBeforeAdd = paymentSourceList.Count;
            
            var paymentSourceToAdd = paymentSourceList.AddNew();
            BuildPaymentSource(paymentSourceToAdd); 

            var paymentSourceEditList = await paymentSourceList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, paymentSourceEditList.Count);
        }

        private void BuildPaymentSource(PaymentSourceEC paymentSource)
        {
            paymentSource.Description = "doc type description";
            paymentSource.Notes = "document type notes";
        }
        
    }
}
