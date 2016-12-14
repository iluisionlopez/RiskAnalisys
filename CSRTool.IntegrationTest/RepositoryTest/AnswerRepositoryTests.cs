using CSRTool.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSRTool.Dependencies.Repositories.EntityFramework;

namespace CSRTool.IntegrationTest.RepositoryTest
{
    [TestClass]
    public class AnswerRepositoryTests : TestBase
    {
        [TestMethod]
        public void GetAnswersForQuestion01_Should_return_five()
        {
            //Arrange
            var sut = new AnswerRepository(DataContext);

            //Act
            var result = sut.GetAnswersForQuestion(Constants.Question01);

            //Assert
            Assert.AreEqual(5, result.Count);
        }
    }
    
}
