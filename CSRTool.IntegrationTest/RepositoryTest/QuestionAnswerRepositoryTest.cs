using CSRTool.Common;
using CSRTool.Dependencies.Repositories.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CSRTool.IntegrationTest.RepositoryTest
{
    [TestClass]
    public class QuestionAnswerRepositoryTest : TestBase
    {
        [TestMethod]
        public void SaveAssessmentCustomerQuestionAnswer_Should_Work()
        {
            //Arrange
            var sut = new QuestionAnswerRepository(DataContext);
            var entity = new Core.AssessmentCustomerQuestionAnswer
            {
                AssessmentCustomerId = Constants.AndreasFirstCustomerAssessment,
                QuestionAnswerId = Guid.Parse("AA181413-1758-409A-8CBE-5B891D203822")
            };

            //Act
            var result = sut.SaveAssessmentCustomerQuestionAnswer(entity);

            //Assert
            Assert.AreEqual(true, result);
        }


        [TestMethod]
        public void GetQuestionAnswersByAssessment_Should_Work()
        {
            //Arrange
            var sut = new QuestionAnswerRepository(DataContext);
            var assessmentID = Constants.AndreasFirstCustomerAssessment;

            //Act
            var result = sut.GetQuestionAnswersByCustomerAssessment(assessmentID);

            //Assert
            Assert.AreEqual(true, result.Count > 0);
        }

        [TestMethod]
        public void SaveAssessmentCustomerQuestionAnswerList_Should_Work()
        {
            //Arrange
            var sut = new QuestionAnswerRepository(DataContext);
            var list = new List<Core.AssessmentCustomerQuestionAnswer>();
            var entity1 = new Core.AssessmentCustomerQuestionAnswer
            {
                Id = Guid.NewGuid(),
                AssessmentCustomerId = Constants.AndreasFirstCustomerAssessment,
                QuestionAnswerId = Guid.Parse("AA181413-1758-409A-8CBE-5B891D203822"),
                Comment = "From Test"
            };
            var entity2 = new Core.AssessmentCustomerQuestionAnswer
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

            //Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void FindBy_Should_Work()
        {
            //Arrange
            var sut = new QuestionAnswerRepository(DataContext);
            var question = Guid.Parse("F53A9026-9FE7-45A0-AB1F-B54600846F6F");
            var answer = Guid.Parse("4927B417-14FC-4097-A88F-FDC930C59F87");

            //Act
            var result = sut.FindBy(x => x.AnswerId == answer && x.QuestionId == question);

            //Assert
            Assert.AreNotEqual(null, result);

        }

        [TestMethod]
        public void DeleteQuestionAnswersByAssessmnetID_Should_Work()
        {
            //Arrange
            var sut = new QuestionAnswerRepository(DataContext);
            var assessmentID = Constants.AndreasFirstCustomerAssessment;

            //Act
            var result = sut.DeleteCustomerQuestionAnswersByAssessmnetID(assessmentID);

            //Assert
            Assert.AreEqual(true, result);
        }
    }
}
