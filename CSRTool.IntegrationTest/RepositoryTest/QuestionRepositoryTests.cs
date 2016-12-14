using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSRTool.Dependencies.Repositories.EntityFramework;

namespace CSRTool.IntegrationTest.RepositoryTest
{
    [TestClass]
    public class QuestionRepositoryTests : TestBase
    {
        [TestMethod]
        public void GetQuestions_Should_return_nineteen()
        {
            //Arrange
            var sut = new QuestionRepository(DataContext);

            //Act
            var result = sut.GetQuestions();

            //Assert
            Assert.AreEqual(true, result.Count > 0);
        }
    }
}
