using CSRTool.Dependencies.Repositories.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSRTool.IntegrationTest.RepositoryTest
{
    [TestClass]
    public class SupplyChainRepositoryTest : TestBase
    {
        [TestMethod]
        public void GetAll_should_work()
        {
            //Arrange
            var sut = new SupplyChainRepository(DataContext);

            //Act
            var result = sut.GetAll();

            //Assert
            Assert.AreEqual(true, result.Count > 0);
        }
    }
}
