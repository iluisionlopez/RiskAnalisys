using CSRTool.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSRTool.Dependencies.Repositories.EntityFramework;
using System.Collections.Generic;
using CSRTool.Core;

namespace CSRTool.IntegrationTest.RepositoryTest
{
    [TestClass]
    public class TRansactionRepositoryTests : TestBase
    {
        [TestMethod]
        public void GetTransactionTypes_Should_return_five()
        {
            //Arrange
            var sut = new TransactionTypeRepository(DataContext);

            //Act
            var result = sut.GetTransactionTypes();

            //Assert
            Assert.AreEqual(4, result.Count);
        }
        [TestMethod]
        public void SaveAssessmentOfferTypes_Should_return_Success()
        {
            //Arrange
            var sut = new TransactionTypeRepository(DataContext);
            var transactions = new List<Core.AssessmentTransactionType>();
            transactions.Add(new Core.AssessmentTransactionType
            {
                AssessmentCustomerId = Constants.AndreasFirstCustomerAssessment,
                TransactionTypeId = Constants.TransactionTypeAgent,
                Id = System.Guid.NewGuid()

            });
            transactions.Add(new Core.AssessmentTransactionType
            {
                AssessmentCustomerId = Constants.AndreasFirstCustomerAssessment,
                TransactionTypeId = Constants.TransactionTypeDirect,
                Id = System.Guid.NewGuid()
            });

            //Act
            var result = sut.SaveAssessmentTransactionTypes(transactions);

            //Assert
            Assert.AreEqual(NotificationType.Success, result.NotificationType);
        }

        [TestMethod]
        public void GetTransactionTypesByAssessmentCustomerId_Should_return_one()
        {
            //Arrange
            var sut = new TransactionTypeRepository(DataContext);

            //Act
            var result = sut.GetTransactionTypesByAssessmentCustomerId(Constants.AndreasFirstCustomerAssessment);

            //Assert
            Assert.AreEqual(true, result.Count> 0);
        }

        [TestMethod]
        public void DeleteAssessmentTransactionTypesForAssessmentCustomerId_Should_work()
        {
            //Arrange
            var sut = new TransactionTypeRepository(DataContext);
            var assessmentID = Constants.AndreasFirstCustomerAssessment;
            //Act
            var result = sut.DeleteAssessmentTransactionTypes(assessmentID);

            //Assert
            Assert.AreEqual(true, result);
        }
       
    }
    
}
