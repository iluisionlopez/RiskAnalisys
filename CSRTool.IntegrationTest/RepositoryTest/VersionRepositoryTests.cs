using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSRTool.Dependencies.Repositories.EntityFramework;

namespace CSRTool.IntegrationTest.RepositoryTest
{
    [TestClass]
    public class VersionRepositoryTests : TestBase
    {
        [TestMethod]
        public void GetVersionTypes_Should_return_one()
        {
            //Arrange
            var sut = new VersionRepository(DataContext);

            //Act
            var result = sut.GetVersions();

            //Assert
            Assert.AreEqual(1, result.Count);
        }
    }
}
