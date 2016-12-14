using AutoMapper;
using CSRTool.Common;
using CSRTool.Core;
using CSRTool.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CSRTool.Dependencies.Repositories.EntityFramework
{
    public class AssessmentCustomerRepository : IAssessmentCustomerRepository
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<AssessmentCustomer> _assessmentCustomer;
        private readonly DbSet<AssessmentCustomerQuestionAnswer> _assessmentQuestion;

        public AssessmentCustomerRepository(DbContext dataContext)
        {
            _dbContext = dataContext;
            _assessmentCustomer = _dbContext.Set<AssessmentCustomer>();
            _assessmentQuestion = _dbContext.Set<AssessmentCustomerQuestionAnswer>();
        }


        public List<Core.AssessmentCustomer> GetAssessmentCustomers()
        {
            var ret = Mapper.Map<List<AssessmentCustomer>, List<Core.AssessmentCustomer>>(GetDbAssessmentCustomers().ToList());
            return ret;
        }


        public List<Core.AssessmentCustomer> GetAssessmentCustomersForAssessor(Guid userId)
        {
            var ret = Mapper.Map<List<AssessmentCustomer>, List<Core.AssessmentCustomer>>(GetDbAssessmentCustomersForAssessor(userId).ToList());
            return ret;
        }

        public Core.AssessmentCustomer GetAssessmentCustomerById(Guid assessmentCustomerId)
        {
            var ret = Mapper.Map<AssessmentCustomer, Core.AssessmentCustomer>(GetDbAssessmentCustomerById(assessmentCustomerId));
            return ret;
        }

        /// <summary>
        /// Create or Update an AssessmentCustomer
        /// </summary>
        /// <param name="assessmentCustomer">Entity</param>
        /// <returns>A NotificationType</returns>
        public CSRToolNotifier SaveAssessmentCustomer(Core.AssessmentCustomer assessmentCustomer)
        {
            var csrToolNotifier = new CSRToolNotifier();

            var dbAssessmentCustomer = _dbContext.Set<AssessmentCustomer>().FirstOrDefault(x => x.Id == assessmentCustomer.Id);
            try
            {
                if (dbAssessmentCustomer != null)
                {//update
                    UpdateEntity(assessmentCustomer, dbAssessmentCustomer);
                }
                else
                {//create
                    dbAssessmentCustomer = NewEntity(assessmentCustomer);
                }

                _dbContext.SaveChanges();

                csrToolNotifier.NotificationType = NotificationType.Success;
                csrToolNotifier.Message = dbAssessmentCustomer.Id.ToString();
            }
            catch (Exception e)
            {
                csrToolNotifier.NotificationType = NotificationType.Error;
                csrToolNotifier.Message = string.Concat("Message: ", e.Message, Environment.NewLine,
                                                        "Trace: InnerException", e.InnerException.InnerException, Environment.NewLine,
                                                        "Trace: ", e.StackTrace);
            }

            return csrToolNotifier;
        }
        public void Delete(Core.AssessmentCustomer entity)
        {
            try
            {
                var toDelete = _assessmentCustomer.FirstOrDefault(x => x.Id == entity.Id);
                _assessmentCustomer.Remove(toDelete);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private AssessmentCustomer NewEntity(Core.AssessmentCustomer assessmentCustomer)
        {
            AssessmentCustomer dbAssessmentCustomer = _assessmentCustomer.Create();
            dbAssessmentCustomer.Id = Guid.NewGuid();
            dbAssessmentCustomer.Name = assessmentCustomer.Name;
            dbAssessmentCustomer.AssessmentTypeId = Constants.AssessmentTypeCustomer;
            dbAssessmentCustomer.AssessorId = assessmentCustomer.AssessorId;
            dbAssessmentCustomer.ResponibleSalesPerson = assessmentCustomer.ResponibleSalesPerson;
            dbAssessmentCustomer.Date = assessmentCustomer.Date;
            if (!assessmentCustomer.CustomerId.Equals(Guid.Empty))
                dbAssessmentCustomer.CustomerId = assessmentCustomer.CustomerId;
            //dbAssessmentCustomer.Site = assessmentCustomer.Site;
            dbAssessmentCustomer.Project = assessmentCustomer.Project;
            dbAssessmentCustomer.Segment = assessmentCustomer.Segment;
            dbAssessmentCustomer.Comment = assessmentCustomer.Comment;

            if (!assessmentCustomer.TerritoryId.Equals(Guid.Empty))
                dbAssessmentCustomer.TerritoryId = assessmentCustomer.TerritoryId;
            if (!assessmentCustomer.VersionId.Equals(Guid.Empty))
                dbAssessmentCustomer.VersionId = assessmentCustomer.VersionId;
            dbAssessmentCustomer.ScaniaDepartment = assessmentCustomer.ScaniaDepartment;
            if (!assessmentCustomer.CaseTypeId.Equals(Guid.Empty))
                dbAssessmentCustomer.CaseTypeId = assessmentCustomer.CaseTypeId;
            if (!assessmentCustomer.SectorId.Equals(Guid.Empty))
                dbAssessmentCustomer.SectorId = assessmentCustomer.SectorId;
            dbAssessmentCustomer.Comment = assessmentCustomer.Comment;
            dbAssessmentCustomer.IsComplete = assessmentCustomer.IsComplete;
            dbAssessmentCustomer.Created = DateTime.Now;
            dbAssessmentCustomer.CreatedBy = assessmentCustomer.CreatedBy;
            dbAssessmentCustomer.Changed = DateTime.Now;
            dbAssessmentCustomer.ChangedBy = assessmentCustomer.ChangedBy;
            dbAssessmentCustomer.IsActive = assessmentCustomer.IsActive;
            dbAssessmentCustomer.RiskIndication = assessmentCustomer.RiskIndication;

            _assessmentCustomer.Add(dbAssessmentCustomer);
            return dbAssessmentCustomer;
        }

        private static void UpdateEntity(Core.AssessmentCustomer assessmentCustomer, AssessmentCustomer dbAssessmentCustomer)
        {
            dbAssessmentCustomer.Name = assessmentCustomer.Name;
            dbAssessmentCustomer.AssessmentTypeId = Constants.AssessmentTypeCustomer;
            dbAssessmentCustomer.AssessorId = assessmentCustomer.AssessorId;
            dbAssessmentCustomer.Date = assessmentCustomer.Date;
            if (!assessmentCustomer.CustomerId.Equals(Guid.Empty))
                dbAssessmentCustomer.CustomerId = assessmentCustomer.CustomerId;
            dbAssessmentCustomer.ResponibleSalesPerson = assessmentCustomer.ResponibleSalesPerson;
            //dbAssessmentCustomer.Site = assessmentCustomer.Site;
            dbAssessmentCustomer.Project = assessmentCustomer.Project;
            dbAssessmentCustomer.Segment = assessmentCustomer.Segment;
            dbAssessmentCustomer.TerritoryId = assessmentCustomer.TerritoryId;
            dbAssessmentCustomer.VersionId = assessmentCustomer.VersionId;
            dbAssessmentCustomer.ScaniaDepartment = assessmentCustomer.ScaniaDepartment;
            dbAssessmentCustomer.CaseTypeId = assessmentCustomer.CaseTypeId;
            dbAssessmentCustomer.SectorId = assessmentCustomer.SectorId;
            dbAssessmentCustomer.Comment = assessmentCustomer.Comment;
            dbAssessmentCustomer.IsComplete = assessmentCustomer.IsComplete;
            dbAssessmentCustomer.Changed = assessmentCustomer.Changed;
            dbAssessmentCustomer.ChangedBy = assessmentCustomer.ChangedBy;
            dbAssessmentCustomer.IsActive = assessmentCustomer.IsActive;
            dbAssessmentCustomer.RiskIndication = assessmentCustomer.RiskIndication;
            dbAssessmentCustomer.Comment = assessmentCustomer.Comment;
        }

        private IEnumerable<AssessmentCustomer> GetDbAssessmentCustomersForAssessor(Guid userId)
        {
            return _assessmentCustomer.Where(ac => ac.AssessorId == userId);

        }

        private IEnumerable<AssessmentCustomer> GetDbAssessmentCustomers()
        {
            return _assessmentCustomer;
        }

        private AssessmentCustomer GetDbAssessmentCustomerById(Guid assessmentCustomerId)
        {
            var assessment = _assessmentCustomer.FirstOrDefault(x => x.Id == assessmentCustomerId);
            assessment.AssessmentCustomerQuestionAnswer = _assessmentQuestion.Where(a => a.AssessmentCustomerId == assessmentCustomerId).ToList();
            return assessment;
        }
    }
}

