using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSRTool.Dependencies.Repositories.EntityFramework;

namespace CSRTool.IntegrationTest.RepositoryTest
{
    [TestClass]
    public class AssessmentTypeRepositoryTests : TestBase
    {
        [TestMethod]
        public void GetAssessmentType_Should_return_two()
        {
            //Arrange
            var sut = new AssessmentTypeRepository(DataContext);

            //Act
            var result = sut.GetAssessmentTypes();

            //Assert
            Assert.AreEqual(2, result.Count);
        }
    }
}
