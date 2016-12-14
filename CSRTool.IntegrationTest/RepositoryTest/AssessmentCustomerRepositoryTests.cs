using System;
using CSRTool.Common;
using CSRTool.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSRTool.Dependencies.Repositories.EntityFramework;
using AssessmentCustomer = CSRTool.Dependencies.Repositories.EntityFramework.AssessmentCustomer;

namespace CSRTool.IntegrationTest.RepositoryTest
{
    [TestClass]
    public class AssessmentCustomerRepositoryTests : TestBase
    {
        [TestMethod]
        public void GetAssessmentCustomers_Should_return_three()
        {
            //Arrange
            var sut = new AssessmentCustomerRepository(DataContext);

            //Act
            var result = sut.GetAssessmentCustomers();

            //Assert
            Assert.AreEqual(true, result.Count > 0);
        }

        [TestMethod]
        public void GetAssessmentCustomersForUser_Anna_Should_return_one()
        {
            //Arrange
            var sut = new AssessmentCustomerRepository(DataContext);

            //Act
            var result = sut.GetAssessmentCustomersForAssessor(Constants.UserAnna);

            //Assert
            Assert.AreEqual(true, result.Count > 0);
        }

        [TestMethod]
        public void SaveAssessmentCustomers_Should_return_sucess()
        {
            //Arrange
            var sut = new AssessmentCustomerRepository(DataContext);
            var coreAssessmentCustomer = new Core.AssessmentCustomer
            {
                Name = "RepositoryTest-ToBeDeleted",
                AssessmentTypeId = Constants.AssessmentTypeSupplier,
                AssessorId = Constants.UserAnna,
                Date = DateTime.Now,
                IsComplete = false,
                Created = DateTime.Now,
                CreatedBy = Constants.UserAnna,
                Changed = DateTime.Now,
                ChangedBy = Constants.UserAnna,
                IsActive = true,
                CaseTypeId = Guid.Parse("0DDC2333-8F15-40BA-9682-E5230A1259B7")
            };


            //Act
            var result = sut.SaveAssessmentCustomer(coreAssessmentCustomer);

            //Assert
            Assert.AreEqual(NotificationType.Success, result.NotificationType);
        }


        [TestMethod]
        public void GetAssessmentCustomerById_Should_Work()
        {
            //Arrange
            var sut = new AssessmentCustomerRepository(DataContext);
            var id = Constants.AndreasFirstCustomerAssessment;

            //Act
            var result = sut.GetAssessmentCustomerById(id);

            //Assert
            Assert.AreEqual(true, result.Id != Guid.Empty);
        }

    }
}
