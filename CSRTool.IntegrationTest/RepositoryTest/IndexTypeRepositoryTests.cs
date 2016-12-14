using System;
using CSRTool.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSRTool.Dependencies.Repositories.EntityFramework;

namespace CSRTool.IntegrationTest.RepositoryTest
{
    [TestClass]
    public class IndexTypeRepositoryTests : TestBase
    {
        [TestMethod]
        public void GetIndexType_Should_return_twelwe()
        {
            //Arrange
            var sut = new IndexTypeRepository(DataContext);

            //Act
            var result = sut.GetIndexTypes();

            //Assert
            Assert.AreEqual(12, result.Count);
        }

        [TestMethod]
        public void GetIndexTypeByRiskCategory_Environment_Should_return_one()
        {
            //Arrange
            var riskCategory = Constants.RiskCategoryEnvironment;
            var sut = new IndexTypeRepository(DataContext);

            //Act
            var result = sut.GetIndexTypesByRiskCategory(riskCategory);

            //Assert
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void GetIndexTypeByRiskCategory_LabourConditions_Should_return_one()
        {
            //Arrange
            var riskCategory = Constants.RiskCategoryLabourConditions;
            var sut = new IndexTypeRepository(DataContext);

            //Act
            var result = sut.GetIndexTypesByRiskCategory(riskCategory);

            //Assert
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void GetIndexTypeByRiskCategory_Corruption_Should_return_four()
        {
            //Arrange
            var riskCategory = Constants.RiskCategoryCorruption;
            var sut = new IndexTypeRepository(DataContext);

            //Act
            var result = sut.GetIndexTypesByRiskCategory(riskCategory);

            //Assert
            Assert.AreEqual(4, result.Count);
        }

        [TestMethod]
        public void GetIndexTypeByRiskCategory_HumanRights_Should_return_six()
        {
            //Arrange
            var riskCategory = Constants.RiskCategoryHumanRights;
            var sut = new IndexTypeRepository(DataContext);

            //Act
            var result = sut.GetIndexTypesByRiskCategory(riskCategory);

            //Assert
            Assert.AreEqual(6, result.Count);
        }
    }
}
