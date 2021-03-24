using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EMailECL_Tests : CslaBaseTest
    {
        [Fact]
        private async void EMailECL_TestEMailECL()
        {
            var eMailEdit = await EMailECL.NewEMailECL();

            Assert.NotNull(eMailEdit);
            Assert.IsType<EMailECL>(eMailEdit);
        }


        [Fact]
        private async void EMailECL_TestGetEMailECL()
        {
            var listToTest = await EMailECL.GetEMailECL(GetEmails());

            Assert.NotNull(listToTest);
            Assert.Equal(3, listToTest.Count);
        }

        private async Task BuildEMail(EMailEC eMail)
        {
            eMail.EMailAddress = "email@email.com";
            eMail.EMailType = await EMailTypeEC.GetEMailTypeEC(
                new EMailType()
                {
                    Id = 1,
                    Notes = "EMailType notes",
                    Description = "Email description"
                }
            );
            eMail.Notes = "document type notes";
            eMail.LastUpdatedBy = "edm";
            eMail.LastUpdatedDate = DateTime.Now;
        }

        private static IList<EMail> GetEmails()
        {
            return new List<EMail>()
            {
                new EMail()
                {
                    Id = 1, EMailType = new EMailType() {Id = 1},
                    EMailAddress = "edm@ecs.com", LastUpdatedBy = "edm", LastUpdatedDate = DateTime.Now,
                    Notes = "some notes", RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new EMail()
                {
                    Id = 2, EMailType = new EMailType() {Id = 1},
                    EMailAddress = "edm@ecs.com", LastUpdatedBy = "edm", LastUpdatedDate = DateTime.Now,
                    Notes = "some notes", RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                },
                new EMail()
                {
                    Id = 99, EMailType = new EMailType() {Id = 1},
                    EMailAddress = "edm@ecs.com", LastUpdatedBy = "edm", LastUpdatedDate = DateTime.Now,
                    Notes = "test the delete", RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks)
                }
            };
        }
    }
}