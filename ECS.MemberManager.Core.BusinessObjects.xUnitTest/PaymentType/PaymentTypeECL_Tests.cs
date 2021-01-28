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
        
        [Fact]
        private async void PaymentTypeECL_TestDeletePaymentTypeEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPaymentTypeDal>();
            var childData = await dal.Fetch();
            
            var paymentTypeEditList = await PaymentTypeECL.GetPaymentTypeECL(childData);

            var paymentType = paymentTypeEditList.First(a => a.Id == 99);

            // remove is deferred delete
            paymentTypeEditList.Remove(paymentType); 

            var paymentTypeListAfterDelete = await paymentTypeEditList.SaveAsync();
            
            Assert.NotEqual(childData.Count,paymentTypeListAfterDelete.Count);
        }

        [Fact]
        private async void PaymentTypeECL_TestUpdatePaymentTypeEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPaymentTypeDal>();
            var childData = await dal.Fetch();
            
            var paymentTypeList = await PaymentTypeECL.GetPaymentTypeECL(childData);
            var countBeforeUpdate = paymentTypeList.Count;
            var idToUpdate = paymentTypeList.Min(a => a.Id);
            var paymentTypeToUpdate = paymentTypeList.First(a => a.Id == idToUpdate);

            paymentTypeToUpdate.Description = "This was updated";
            await paymentTypeList.SaveAsync();

            var updatedList = await dal.Fetch();
            var updatedPaymentTypesList = await PaymentTypeECL.GetPaymentTypeECL(updatedList);
            
            Assert.Equal("This was updated",updatedPaymentTypesList.First(a => a.Id == idToUpdate).Description);
            Assert.Equal(countBeforeUpdate, updatedPaymentTypesList.Count);
        }

        [Fact]
        private async void PaymentTypeECL_TestAddPaymentTypeEntry()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPaymentTypeDal>();
            var childData = await dal.Fetch();

            var paymentTypeList = await PaymentTypeECL.GetPaymentTypeECL(childData);
            var countBeforeAdd = paymentTypeList.Count;
            
            var paymentTypeToAdd = paymentTypeList.AddNew();
            BuildPaymentType(paymentTypeToAdd); 

            var paymentTypeEditList = await paymentTypeList.SaveAsync();
            
            Assert.NotEqual(countBeforeAdd, paymentTypeEditList.Count);
        }

        private void BuildPaymentType(PaymentTypeEC paymentType)
        {
            paymentType.Description = "doc type description";
            paymentType.Notes = "document type notes";
        }
        
    }
}
