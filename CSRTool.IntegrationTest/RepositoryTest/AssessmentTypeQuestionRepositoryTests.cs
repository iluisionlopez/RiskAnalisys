using CSRTool.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSRTool.Dependencies.Repositories.EntityFramework;

namespace CSRTool.IntegrationTest.RepositoryTest
{
    [TestClass]
    public class AssessmentTypeQuestionRepositoryTests : TestBase
    {
        [TestMethod]
        public void AssessmentTypeQuestionByTypeCustomer_Should_return_twenty()
        {
            //Arrange
            var sut = new AssessmentTypeQuestionRepository(DataContext);

            //Act
            var result = sut.GetAssessmentTypeQuestions(Constants.AssessmentTypeCustomer);

            //Assert
            Assert.AreEqual(20, result.Count);
        }
        [TestMethod]
        public void AssessmentTypeQuestionByTypeSupplier_Should_return_six()
        {
            //Arrange
            var sut = new AssessmentTypeQuestionRepository(DataContext);

            //Act
            var result = sut.GetAssessmentTypeQuestions(Constants.AssessmentTypeSupplier);

            //Assert
            Assert.AreEqual(true, result.Count > 0);
        }
    }
    
}
