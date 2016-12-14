using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSRTool.Dependencies.Repositories.EntityFramework;

namespace CSRTool.IntegrationTest.RepositoryTest
{
    [TestClass]
    public class IndexRepositoryTests : TestBase
    {
        [TestMethod]
        public void GetIndex_Should_work()
        {
            //Arrange
            var sut = new IndexRepository(DataContext);

            //Act
            var result = sut.GetIndexes();

            //Assert
            Assert.AreEqual(true, result.Count > 0);
        }

        [TestMethod]
        public void GetIndexesByIndexTypeId_Should_work()
        {
            //Arrange
            var sut = new IndexRepository(DataContext);

            //Act
            var result = sut.GetIndexesByIndexTypeId(Common.Constants.IndexTypeCorruption);

            //Assert
            Assert.AreEqual(true, result.Count > 0);
        }


    }
}
