using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSRTool.Dependencies.Repositories.EntityFramework;

namespace CSRTool.IntegrationTest.RepositoryTest
{
    [TestClass]
    public class CaseTypeRepositoryTests : TestBase
    {
        [TestMethod]
        public void GetCaseType_Should_return_two()
        {
            //Arrange
            var sut = new CaseTypeRepository(DataContext);

            //Act
            var result = sut.GetCaseTypes();

            //Assert
            Assert.AreEqual(true, result.Count > 0);
        }
    }
}
