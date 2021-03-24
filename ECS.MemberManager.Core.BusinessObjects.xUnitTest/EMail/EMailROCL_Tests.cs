using System;
using System.Collections.Generic;
using ECS.MemberManager.Core.EF.Domain;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class EMailROCL_Tests : CslaBaseTest
    {
        [Fact]
        private async void EMailInfoList_TestGetEMailInfoList()
        {
            var eMailTypeInfoList = await EMailROCL.GetEMailROCL(GetEmails());

            Assert.NotNull(eMailTypeInfoList);
            Assert.True(eMailTypeInfoList.IsReadOnly);
            Assert.Equal(3, eMailTypeInfoList.Count);
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