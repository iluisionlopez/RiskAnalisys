using System.Linq;
using CSRTool.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSRTool.Dependencies.Repositories.EntityFramework;

namespace CSRTool.IntegrationTest.RepositoryTest
{
    [TestClass]
    public class RiskCategoryRepositoryTests : TestBase
    {
        [TestMethod]
        public void GetRiskCategory_Should_return_five()
        {
            //Arrange
            var sut = new RiskCategoryRepository(DataContext);

            //Act
            var result = sut.GetRiskCategories();

            //Assert
            Assert.AreEqual(5, result.Count);
        }

        [TestMethod]
        public void GetRiskCategoryByIndexType_Corruption_Should_return_one()
        {
            //Arrange
            var sut = new RiskCategoryRepository(DataContext);

            //Act
            var result = sut.GetRiskCategoriesByIndexType(Constants.IndexTypeCorruption);

            //Assert
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void GetRiskCategoryByIndexType_Should_Be_Corruption()
        {
            //Arrange
            var sut = new RiskCategoryRepository(DataContext);

            //Act
            var result = sut.GetRiskCategoriesByIndexType(Constants.IndexTypeCorruption);

            //Assert
            Assert.AreEqual("Corruption", result.First().Name);
        }

        [TestMethod]
        public void GetRiskCategoryByQuestion_Question01_Should_return_one()
        {
            //Arrange
            var sut = new RiskCategoryRepository(DataContext);

            //Act
            var result = sut.GetRiskCategoriesByQuestion(Constants.Question01);

            //Assert
            Assert.AreEqual(1, result.Count);
        }
        [TestMethod]
        public void GetRiskCategoryByQuestion_Question01_Should_return_Environment()
        {
            //Arrange
            var sut = new RiskCategoryRepository(DataContext);

            //Act
            var result = sut.GetRiskCategoriesByQuestion(Constants.Question01);

            //Assert
            Assert.AreEqual("Environment", result.First().Name);
        }
    }
}
