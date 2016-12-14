using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSRTool.Dependencies.Repositories.EntityFramework;

namespace CSRTool.IntegrationTest.RepositoryTest
{
    [TestClass]
    public class TerritoryRepositoryTests : TestBase
    {
        [TestMethod]
        public void GetTerritories_Should_work()
        {
            //Arrange
            var sut = new TerritoryRepository(DataContext);

            //Act
            var result = sut.GetTerritories();

            //Assert
            Assert.AreEqual(true, result.Count > 0);
        }
    }
    
}
