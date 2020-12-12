using System;
using Xunit;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var dt = DateTime.Now;
            
            Assert.NotEqual(dt, DateTime.Now);
        }
    }
}