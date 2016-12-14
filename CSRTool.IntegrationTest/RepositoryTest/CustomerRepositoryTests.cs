using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSRTool.Dependencies.Repositories.EntityFramework;

namespace CSRTool.IntegrationTest.RepositoryTest
{
    [TestClass]
    public class CustomerRepositoryTests : TestBase
    {
        [TestMethod]
        public void GetCustomer_Should_return_one()
        {
            //Arrange
            var sut = new CustomerRepository(DataContext);

            //Act
            var result = sut.GetCustomers();

            //Assert
            Assert.AreEqual(true, result.Count > 0);
        }
    }
}
