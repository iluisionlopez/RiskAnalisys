using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSRTool.Dependencies.Repositories.EntityFramework;

namespace CSRTool.IntegrationTest.RepositoryTest
{
    [TestClass]
    public class SectorRepositoryTests : TestBase
    {
        [TestMethod]
        public void GetSector_Should_return_thireteen()
        {
            //Arrange
            var sut = new SectorRepository(DataContext);

            //Act
            var result = sut.GetSectors();

            //Assert
            Assert.AreEqual(13, result.Count);
        }
    }
}
