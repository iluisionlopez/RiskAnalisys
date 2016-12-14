using CSRTool.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSRTool.Dependencies.Repositories.EntityFramework;
using CSRTool.Core;
using System.Collections.Generic;

namespace CSRTool.IntegrationTest.RepositoryTest
{
    [TestClass]
    public class OfferTypeRepositoryTests : TestBase
    {
        [TestMethod]
        public void GetOfferType_Should_return_three()
        {
            //Arrange
            var sut = new OfferTypeRepository(DataContext);

            //Act
            var result = sut.GetOfferTypes();

            //Assert
            Assert.AreEqual(3, result.Count);
        }


        [TestMethod]
        public void GetOfferyTpesByAssessmentCustomerId_Should_return_one()
        {
            //Arrange
            var sut = new OfferTypeRepository(DataContext);
            
            //Act
            var result = sut.GetOfferTypesByAssessmentCustomerId(Constants.AndreasFirstCustomerAssessment);

            //Assert
            Assert.AreEqual(true, result.Count > 0);
        }

        [TestMethod]
        public void DeleteOfferTypesForAssessmentCustomerId_Should_work()
        {
            //Arrange
            var sut = new OfferTypeRepository(DataContext);
            var assessmentID = Constants.AndreasFirstCustomerAssessment;
            //Act
            var result = sut.DeleteAssessmentOfferTypes(assessmentID);

            //Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void SaveANewOfferTypesForAssessmentCustomerId_Should_return_Success()
        {
            //Arrange
            var sut = new OfferTypeRepository(DataContext);
            var offer = new Core.AssessmentOfferType
            {
                AssessmentCustomerId = Constants.AndreasFirstCustomerAssessment,
                OfferTypeId = Constants.OfferTypeService
            };

            //Act
            var result = sut.SaveAssessmentOfferType(offer);

            //Assert
            Assert.AreEqual(NotificationType.Success, result.NotificationType);
        }

        [TestMethod]
        public void TryToSaveSameOfferTypesForAssessmentCustomerId_Should_return_Warning()
        {
            //Arrange
            var sut = new OfferTypeRepository(DataContext);
            var offer = new Core.AssessmentOfferType
            {
                AssessmentCustomerId = Constants.AndreasFirstCustomerAssessment,
                OfferTypeId = Constants.OfferTypeService
            };

            //Act
            var result = sut.SaveAssessmentOfferType(offer);

            //Assert
            Assert.AreEqual(NotificationType.Warning, result.NotificationType);
        }

        [TestMethod]
        public void SaveAssessmentOfferTypes_Should_return_Success()
        {
            //Arrange
            var sut = new OfferTypeRepository(DataContext);
            var offers = new List<Core.AssessmentOfferType>();
            offers.Add(new Core.AssessmentOfferType
            {
                AssessmentCustomerId = Constants.AndreasFirstCustomerAssessment,
                OfferTypeId = Constants.OfferTypeFinancing,
                Id = System.Guid.NewGuid()

            });
            offers.Add(new Core.AssessmentOfferType
            {
                AssessmentCustomerId = Constants.AndreasFirstCustomerAssessment,
                OfferTypeId = Constants.OfferTypeProduct,
                Id = System.Guid.NewGuid()
            });

            //Act
            var result = sut.SaveAssessmentOfferTypes(offers);

            //Assert
            Assert.AreEqual(NotificationType.Success, result.NotificationType);
        }
    }
}
