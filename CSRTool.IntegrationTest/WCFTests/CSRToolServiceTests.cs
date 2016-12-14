using CSRTool.Common;
using CSRToolWebApp;
using CSRToolWebApp.DataContracts.Base;
using CSRToolWebApp.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;

namespace CSRTool.IntegrationTest.WCFTests
{
    [TestClass]
    public class CSRToolServiceTests : TestBase
    {
        [TestMethod]
        public void GetWebScan_should_work()
        {
            //Just test invocation works
            //Arrange

            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);

            //Act
            var result = sut.GetWebScansByAssessmentCustomerId(Constants.AndreasFirstCustomerAssessment);

            Assert.AreNotEqual(0, result.Count);

        }

        [TestMethod]
        public void GetUsers_should_work()
        {
            //Just test invocation works
            //Arrange

            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);

            //Act
            var result = sut.GetUsers();

            Assert.AreNotEqual(0, result.Count);

        }

        [TestMethod]
        [Ignore]//Please ignore when success - otherwise there will be a testuser in the database
        public void SaveFirstTimeUser_should_work()
        {
            //Just test invocation works
            //Arrange

            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);

            var baseUser = new User()
            {
                UserName = "MyTestUser",
                FirstName = "My Test",
                LastName = "User",
                Email = "toolkitsupport@scania.com",
                Phone = "+46 723 87 90 87"
            };

            //Act
            var result = sut.SaveFirstTimeUser(baseUser);

            Assert.AreEqual("OK", result.ResponseMessage);

        }

        [TestMethod]
        public void GetAssessmentCustomersForAssessor_should_work()
        {
            //Just test invocation works
            //Arrange

            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);

            //Act
            var result = sut.GetAssessmentCustomersForAssessor(Constants.UserAndreas);

            Assert.AreNotEqual(0, result.Count);

        }

        [TestMethod]
        public void GetAssessmentCustomerById_should_work()
        {
            //Just test invocation works
            //Arrange

            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);
            var assessment = sut.GetAssessmentCustomersForAssessor(Constants.UserAndreas)[0];

            //Act
            var result = sut.GetAssessmentCustomerById(assessment.Id);

            Assert.IsNotNull(result);

        }

        [TestMethod]
        [Ignore]
        public void SaveAssessmentCustomers_should_work()
        {
            //Just test invocation works
            //Arrange

            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);

            var baseAssessmentCustomer = new AssessmentCustomer
            {
                Name = "RepositoryTest-ToBeDeleted",
                AssessmentTypeId = Constants.AssessmentTypeSupplier,
                AssessorId = Constants.UserAnna,
                Date = DateTime.Now.ToString(),
                IsComplete = false,
                Created = DateTime.Now.ToString(),
                CreatedBy = Constants.UserAnna,
                Changed = DateTime.Now.ToString(),
                ChangedBy = Constants.UserAnna
            };

            //Act
            var result = sut.SaveAssessmentCustomer(baseAssessmentCustomer);

            Assert.AreEqual("OK", result.ResponseMessage);

        }

        [TestMethod]
        public void GetAssessmentCustomers_should_work()
        {
            //Just test invocation works
            //Arrange

            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);

            //Act
            var result = sut.GetAssessmentCustomers();

            Assert.AreNotEqual(0, result.Count);

        }

        [TestMethod]
        public void GetOfferTypes_should_work()
        {
            //Just test invocation works
            //Arrange

            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);

            //Act
            var result = sut.GetOfferTypes();

            Assert.AreNotEqual(0, result.Count);

        }

        [TestMethod]
        [Ignore]
        public void GetOfferTypesByAssessmentCustomerId_should_work()
        {
            //Just test invocation works
            //Arrange

            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);
            var assessment = sut.GetAssessmentCustomers()[0];

            //Act
            var result = sut.GetOfferTypesByAssessmentCustomerId(assessment.Id);

            Assert.AreNotEqual(0, result.Count);

        }

        [TestMethod]
        public void GetRiskCategoriesForQuestion_should_work()
        {
            //Just test invocation works
            //Arrange

            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);

            //Act
            var result = sut.GetRiskCategoriesForQuestion(Constants.Question01);

            Assert.AreNotEqual(0, result.Count);

        }

        [TestMethod]
        public void GetCustomers_should_work()
        {
            //Just test invocation works
            //Arrange

            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);

            //Act
            var result = sut.GetCustomers();

            Assert.AreNotEqual(0, result.Count);

        }

        [TestMethod]
        public void GetCustomerByID_should_work()
        {
            //Just test invocation works
            //Arrange

            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);
            var customer = Constants.TheCustomer;

            //Act
            var result = sut.GetCustomer(customer);

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void GetCustomerByName_should_work()
        {
            //Just test invocation works
            //Arrange

            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);
            var customer = "TheCustomer";

            //Act
            var result = sut.GetCustomerByName(customer);

            Assert.IsNotNull(result);

        }

        [TestMethod]
        [Ignore]
        public void SaveCustomers_should_work()
        {
            //Just test invocation works
            //Arrange

            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);

            var baseCustomer = new Customer
            {
                Name = "New Customer"
            };

            //Act
            var result = sut.SaveCustomer(baseCustomer);

            Assert.AreEqual("OK", result.ResponseMessage);

        }

        [TestMethod]
        public void SaveSupplier_should_work()
        {
            //Just test invocation works
            //Arrange

            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);

            var supplier = new Supplier
            {
                Name = "New Supplier",
                DUNS = 1234
            };

            //Act
            var result = sut.SaveSupplier(supplier);

            Assert.AreEqual("OK", result.ResponseMessage);

        }

        [TestMethod]
        public void GetCaseTypes_should_work()
        {
            //Just test invocation works
            //Arrange

            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);

            //Act
            var result = sut.GetCaseTypes();

            Assert.AreNotEqual(0, result.Count);

        }

        [TestMethod]
        public void GetTransactionTypes_should_work()
        {
            //Just test invocation works
            //Arrange

            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);

            //Act
            var result = sut.GetTransactionTypes();

            Assert.AreNotEqual(0, result.Count);

        }

        [TestMethod]
        public void GetTransactionTypesByAssessmentCustomerId_should_work()
        {
            //Just test invocation works
            //Arrange

            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);

            //Act
            var result = sut.GetTransactionTypesByAssessmentCustomerId(Guid.Parse("712E2E9D-4E29-46E9-838D-B5F70E0336E0"));

            Assert.AreNotEqual(0, result.Count);

        }

        [TestMethod]
        public void GetAssessmentTypes_should_work()
        {
            //Just test invocation works
            //Arrange

            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);

            //Act
            var result = sut.GetAssessmentTypes();

            Assert.AreNotEqual(0, result.Count);

        }

        [TestMethod]
        public void GetQuestions_should_work()
        {
            //Just test invocation works
            //Arrange

            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);

            //Act
            var result = sut.GetQuestions();

            Assert.AreNotEqual(0, result.Count);

        }

        [TestMethod]
        public void GetAnswersForQuestion_should_work()
        {
            //Just test invocation works
            //Arrange

            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);

            //Act
            var result = sut.GetAnswersForQuestion(Constants.Question01);

            Assert.AreNotEqual(0, result.Count);

        }

        [TestMethod]
        public void GetCountries_should_work()
        {
            //Just test invocation works
            //Arrange

            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);

            //Act
            var result = sut.GetCountries();

            Assert.AreNotEqual(0, result.Count);

        }

        [TestMethod]
        public void GetIndexes_should_work()
        {
            //Just test invocation works
            //Arrange

            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);

            //Act
            var result = sut.GetIndexes();

            Assert.AreNotEqual(0, result.Count);

        }

        [TestMethod]
        public void GetIndexesByCountry_should_work()
        {
            //Just test invocation works
            //Arrange

            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);

            //Act
            var result = sut.GetIndexesForCountry(Constants.TerritorySweden, Constants.Version1);

            Assert.AreNotEqual(0, result.Count);

        }

        [TestMethod]
        public void GetIndexTypes_should_work()
        {
            //Just test invocation works
            //Arrange

            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);

            //Act
            var result = sut.GetIndexTypes();

            Assert.AreNotEqual(0, result.Count);

        }

        [TestMethod]
        public void GetIndexTypesByRiskCategory_should_work()
        {
            //Just test invocation works
            //Arrange

            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);

            //Act
            var result = sut.GetIndexesForRiskCategory(Constants.RiskCategoryEnvironment);

            Assert.AreNotEqual(0, result.Count);

        }

        [TestMethod]
        public void GetVersions_should_work()
        {
            //Just test invocation works
            //Arrange

            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);

            //Act
            var result = sut.GetVersions();

            Assert.AreNotEqual(0, result.Count);

        }

        [TestMethod]
        public void GetRiskCategoriesByIndexType_should_work()
        {
            //Just test invocation works
            //Arrange

            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);

            //Act
            var result = sut.GetRiskCategoriesForIndexType(Constants.IndexTypeDemocraticGovernance);

            Assert.AreNotEqual(0, result.Count);

        }

        [TestMethod]
        public void GetRiskCategories_should_work()
        {
            //Just test invocation works
            //Arrange

            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);

            //Act
            var result = sut.GetRiskCategories();

            Assert.AreNotEqual(0, result.Count);

        }

        [TestMethod]
        public void GetSectors_should_work()
        {
            //Just test invocation works
            //Arrange

            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);

            //Act
            var result = sut.GetSectors();

            Assert.AreNotEqual(0, result.Count);

        }

        [TestMethod]
        public void GetQuestionsForAssessmentType_should_work()
        {
            //Just test invocation works
            //Arrange

            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);

            //Act
            var result = sut.GetQuestionsForAssessmentType(Constants.AssessmentTypeCustomer);

            //Assert
            Assert.AreNotEqual(0, result.Count);

        }

        [TestMethod]
        public void SaveAssessmentCustomerQuestionAnswerList_should_work()
        {
            //Arrange
            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);

            var list = new List<AssessmentCustomerQuestionAnswer>();
            var entity1 = new AssessmentCustomerQuestionAnswer
            {
                Id = Guid.NewGuid(),
                AssessmentCustomerId = Constants.AndreasFirstCustomerAssessment,
                QuestionAnswerId = Guid.Parse("AA181413-1758-409A-8CBE-5B891D203822"),
                Comment = "From Test"
            };
            var entity2 = new AssessmentCustomerQuestionAnswer
            {
                Id = Guid.NewGuid(),
                AssessmentCustomerId = Constants.AndreasFirstCustomerAssessment,
                QuestionAnswerId = Guid.Parse("950299DA-CDCD-4136-869B-4B5324FD9FA2"),
                Comment = "From Test"
            };
            list.Add(entity1);
            list.Add(entity2);

            //Act
            var result = sut.SaveAssessmentCustomerQuestionAnswerList(list);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void GetQuestionAnswerByCustomerAssessment_should_work()
        {
            //Arrange
            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);

            //Act
            var result = sut.GetQuestionAnserByCustomerAssessmentID(Constants.AndreasFirstCustomerAssessment);

            //Assert
            Assert.AreNotEqual(0, result.Count);
        }

        [TestMethod]
        [Ignore]
        public void SaveAssessmentSupplier_should_work()
        {
            //Arrange

            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);

            var assessment = new AssessmentSupplier
            {
                Name = "AssessmentSupplier Test 2",
                VersionId = Constants.Version1,
                AssessmentTypeId = Constants.AssessmentTypeSupplier,
                AssessorId = Constants.UserAndreas,
                Date = DateTime.Now.ToShortDateString(),
                SupplierId = Constants.TheSupplier,
                Site = "entity.Site",
                Commodity = "entity.Commodity",
                TerritoryId = Constants.TerritoryGerman,
                Auditor = "Luis López",
                AuditDate = DateTime.Now.ToShortDateString(),
                SectorId = Guid.Parse("361458B8-97AF-4D61-BFAC-54BEF4FBDF7F"),
                CaseTypeId = Guid.Parse("B07F1352-3D20-441A-92E3-72E8F2FF510F"),
                SupplyChainID = Guid.Parse("7BA496BD-ECCD-4899-BDC3-A95B060A21EC")
            };

            //Act
            var result = sut.SaveAssessmentSupplier(assessment);

            Assert.AreEqual("OK", result.ResponseMessage);
        }

        [TestMethod]
        public void GetSupplyTypes_should_work()
        {
            //Arrange
            var mockAuth = Substitute.For<IAuth>();
            var sut = new CSRToolService(mockAuth);

            //Act
            var result = sut.GetSupplyTypes();

            //Assert
            Assert.AreEqual(true, result.Count > 0);

        }

    }
}
