using CSRTool.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSRTool.Dependencies.Repositories.EntityFramework;

namespace CSRTool.IntegrationTest.RepositoryTest
{
    [TestClass]
    public class WebScanRepositoryTest: TestBase
    {
        [TestMethod]
        public void GetWebScanForAssessmnetCustomer()
        {
            //Arrange
            var sut = new WebScanRepository(DataContext);
            var assessmentID = Constants.AndreasFirstCustomerAssessment;

            //Act
            var result = sut.GetWebScansByAssessmentCustomerId(assessmentID);

            //Assert
            Assert.AreEqual(true, result.Count > 0);
        }

        [TestMethod]
        public void GetAllWebScan()
        {
            //Arrange
            var sut = new WebScanRepository(DataContext);
            
            //Act
            var result = sut.GetWebScans();

            //Assert
            Assert.AreEqual(true, result.Count > 0);
        }

        [TestMethod]
        public void SaveWebScanFoAssessment()
        {
            //Arrange
            var sut = new WebScanRepository(DataContext);
            var scan = new Core.WebScan {
                Comment = "No negative information found",
                AssessmentId = Constants.AndreasFirstCustomerAssessment,
                WebScanTypeId = Constants.WebScan1,
                SearchString = "Customer OR resten"
            };
            //Act
            var result = sut.SaveCustomerWebScan(scan);

            //Assert
            Assert.AreEqual(true, result);
        }
    }
}
