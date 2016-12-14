using CSRTool.Common;
using CSRTool.Core;
using CSRTool.Dependencies.Repositories.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSRTool.IntegrationTest.RepositoryTest
{
    [TestClass]
    public class AssessmentSupplierRepositoryTest : TestBase
    {
        [TestMethod]
        public void SaveAssessmentSupplier_Should_return_sucess()
        {
            //Arrange
            var sut = new AssessmentSupplierRepository(DataContext);
            var assessment = new Core.AssessmentSupplier
            {
                Name = "AndresSecondTestAssessmentSupplier",
                VersionId = Constants.Version1,
                AssessmentTypeId = Constants.AssessmentTypeSupplier,
                AssessorId = Constants.UserAndreas,
                Date = DateTime.Now,
                //SupplierId = Constants.TheSupplier,
                Site = "entity.Site",
                Commodity = "entity.Commodity",
                TerritoryId = Constants.TerritoryGerman,
                Auditor = "Luis López",
                AuditDate = DateTime.Now,
                CaseTypeId = Guid.Parse("7510F8FC-1DA5-49FE-8F1F-D7EA38AC7D4E")
            };

            //Act
            var result = (CSRToolNotifier)sut.Save(assessment);

            //Assert
            Assert.AreEqual(NotificationType.Success, result.NotificationType);
        }

        [TestMethod]
        public void UpdateAssessmentSupplier_Should_return_sucess()
        {
            //Arrange
            var sut = new AssessmentSupplierRepository(DataContext);
            var assesor = Constants.UserAndreas;
            var assessment = sut.FindBy(x => x.AssessorId == assesor).FirstOrDefault();

            assessment.Name = "ChangeName of Supplier Assessmnet";
            assessment.Site = "Change the site";
            assessment.Commodity = "Change the Commodity";
            assessment.Auditor = "Change Auditor";
            assessment.AuditDate = DateTime.Now;
            assessment.SectorId = Guid.Parse("65F17169-5D2D-45D1-A6BF-9F234ACA0FAF");
            assessment.TerritoryId = Guid.Parse("83A4DC67-F4C5-4789-A1EB-4E15E953B53F");
            assessment.CaseTypeId = Guid.Parse("7510F8FC-1DA5-49FE-8F1F-D7EA38AC7D4E"); 
            //Act
            var result = (CSRToolNotifier)sut.Save(assessment);

            //Assert
            Assert.AreEqual(NotificationType.Success, result.NotificationType);
        }

        [TestMethod]
        public void GetAllAssessmentSupplier_Should_return_sucess()
        {
            //Arrange
            var sut = new AssessmentSupplierRepository(DataContext);

            //Act
            var result = sut.GetAll();

            //Assert
            Assert.AreEqual(true, result.Count > 0);
        }

        [TestMethod]
        public void GetAssessmentSupplierByAssesor_Should_return_sucess()
        {
            //Arrange
            var sut = new AssessmentSupplierRepository(DataContext);
            var assesor = Constants.UserAndreas;

            //Act
            var result = sut.FindBy(x => x.AssessorId == assesor);

            //Assert
            Assert.AreEqual(true, result.Count > 0);
        }

        [TestMethod]
        public void GetAssessmentSupplierByID_Should_return_sucess()
        {
            //Arrange
            var sut = new AssessmentSupplierRepository(DataContext);
            var assesor = Constants.UserAndreas;
            var id = sut.FindBy(x => x.AssessorId == assesor).FirstOrDefault().Id;

            //Act
            var result = sut.GetSingle(id);

            //Assert
            Assert.AreEqual(true, result != null);
        }

        [TestMethod][Ignore]
        public void DeleteAssessmentSupplier_Should_return_sucess()
        {
            //Arrange
            var sut = new AssessmentSupplierRepository(DataContext);
            var assesor = Constants.UserAndreas;
            var id = sut.FindBy(x => x.AssessorId == assesor).FirstOrDefault().Id;
            var assessment = sut.GetSingle(id);

            //Act
            sut.Delete(assessment);

            var result = sut.GetSingle(id);


            //Assert
            Assert.AreEqual(true, result == null);
        }
    }
}
