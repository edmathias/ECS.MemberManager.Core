﻿using System.IO;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.DataAccess.Mock;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class TitleROCL_Tests
    {
        private IConfigurationRoot _config = null;
        private bool IsDatabaseBuilt = false;

        public TitleROCL_Tests()
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
        private async void TitleInfoList_TestGetTitleInfoList()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITitleDal>();
            var childData = await dal.Fetch();

            var titleTypeInfoList = await TitleROCL.GetTitleROCL(childData);
            
            Assert.NotNull(titleTypeInfoList);
            Assert.True(titleTypeInfoList.IsReadOnly);
            Assert.Equal(3, titleTypeInfoList.Count);
        }
      
    }
}