using AutoMapper;
using CSRTool.Core;
using CSRTool.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace CSRTool.Dependencies.Repositories.EntityFramework
{
    /// <summary>
    /// 
    /// </summary>
    public class AssessmentSupplierRepository : IAssessmentSupplierReposity
    {

        private readonly DbContext _dbContext;

        private readonly DbSet<AssessmentSupplier> _assessmentSupplier;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataContext"></param>
        public AssessmentSupplierRepository(DbContext dataContext)
        {
            _dbContext = dataContext;
            _assessmentSupplier = _dbContext.Set<AssessmentSupplier>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Core.AssessmentSupplier GetSingle(Guid id)
        {
            var response = _assessmentSupplier.FirstOrDefault(x => x.Id == id);
            return Mapper.Map<AssessmentSupplier, Core.AssessmentSupplier>(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<Core.AssessmentSupplier> GetAll()
        {
            return Mapper.Map<IList<AssessmentSupplier>, IList<Core.AssessmentSupplier>>(_assessmentSupplier.ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IList<Core.AssessmentSupplier> FindBy(Expression<Func<Core.AssessmentSupplier, bool>> predicate)
        {
            var expression = Mapper.Map<Expression<Func<AssessmentSupplier, bool>>>(predicate);
            var response = _assessmentSupplier.Where(expression);
            return Mapper.Map<IList<AssessmentSupplier>, IList<Core.AssessmentSupplier>>(response.ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Add(Core.AssessmentSupplier entity)
        {
            try
            {
                _assessmentSupplier.Add(Mapper.Map<AssessmentSupplier>(entity));
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(Core.AssessmentSupplier entity)
        {
            try
            {
                var toDelete = _assessmentSupplier.FirstOrDefault(x => x.Id == entity.Id);
                _assessmentSupplier.Remove(toDelete);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        public void RemoveBy(Expression<Func<Core.AssessmentSupplier, bool>> predicate)
        {
            try
            {
                var expression = Mapper.Map<Expression<Func<AssessmentSupplier, bool>>>(predicate);
                var dbAssessmentSupplier = _assessmentSupplier.Where(expression);
                if (dbAssessmentSupplier != null)
                {
                    _assessmentSupplier.RemoveRange(dbAssessmentSupplier);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public CSRToolNotifier Save(Core.AssessmentSupplier entity)
        {
            var csrToolNotifier = new CSRToolNotifier();
            var dbAssessmentSupplier = _dbContext.Set<AssessmentSupplier>().FirstOrDefault(x => x.Id == entity.Id);
            try
            {
                if (dbAssessmentSupplier != null)
                {//update                    
                    dbAssessmentSupplier.Changed = DateTime.Now;
                    dbAssessmentSupplier.ChangedBy = entity.AssessorId;
                    dbAssessmentSupplier.IsActive = true;

                    dbAssessmentSupplier.Name = entity.Name;
                    dbAssessmentSupplier.Site = entity.Site;
                    dbAssessmentSupplier.Buyer = entity.Buyer;
                    if (!entity.SupplierId.Equals(Guid.Empty))
                        dbAssessmentSupplier.SupplierId = entity.SupplierId;
                    dbAssessmentSupplier.Commodity = entity.Commodity;
                    dbAssessmentSupplier.Auditor = entity.Auditor;
                    dbAssessmentSupplier.RiskRating = entity.RiskRating;
                    dbAssessmentSupplier.PerformanceRating = entity.PerformanceRating;
                    dbAssessmentSupplier.RatingComment = entity.RatingComment;
                    dbAssessmentSupplier.Complete = entity.Complete;

                    if (entity.AuditDate != DateTime.MinValue)
                    {
                        dbAssessmentSupplier.AuditDate = entity.AuditDate;
                    }

                    if (!entity.SectorId.Equals(Guid.Empty))
                        dbAssessmentSupplier.SectorId = entity.SectorId;
                    if (!entity.TerritoryId.Equals(Guid.Empty))
                        dbAssessmentSupplier.TerritoryId = entity.TerritoryId;
                    if (!entity.CaseTypeId.Equals(Guid.Empty))
                        dbAssessmentSupplier.CaseTypeId = entity.CaseTypeId;
                    if (!entity.SupplyChainID.Equals(Guid.Empty))
                        dbAssessmentSupplier.SupplyChainID = entity.SupplyChainID;
                }
                else
                {//create
                    AssessmentSupplier newassessment = _assessmentSupplier.Create();


                    newassessment.Id = Guid.NewGuid();
                    newassessment.VersionId = entity.VersionId;
                    newassessment.Created = DateTime.Now;
                    newassessment.CreatedBy = entity.AssessorId;
                    newassessment.Changed = DateTime.Now;
                    newassessment.ChangedBy = entity.AssessorId;
                    newassessment.IsActive = true;

                    newassessment.Name = entity.Name;
                    newassessment.AssessmentTypeId = entity.AssessmentTypeId;
                    newassessment.AssessorId = entity.AssessorId;
                    newassessment.Date = DateTime.Now;
                    newassessment.Complete = entity.Complete;
                    newassessment.PerformanceRating = entity.PerformanceRating;
                    newassessment.RiskRating = entity.RiskRating;
                    newassessment.RatingComment = entity.RatingComment;
                    newassessment.Auditor = entity.Auditor;

                    newassessment.Buyer = entity.Buyer;
                    newassessment.Commodity = entity.Commodity;
                    newassessment.Site = entity.Site;
                    if (!entity.SupplierId.Equals(Guid.Empty))
                        newassessment.SupplierId = entity.SupplierId;

                    if (entity.AuditDate != DateTime.MinValue)
                    {
                        newassessment.AuditDate = entity.AuditDate;
                    }

                    if (!entity.CaseTypeId.Equals(Guid.Empty))
                        newassessment.CaseTypeId = entity.CaseTypeId;
                    if (!entity.SupplyChainID.Equals(Guid.Empty))
                        newassessment.SupplyChainID = entity.SupplyChainID;
                    if (!entity.SectorId.Equals(Guid.Empty))
                        newassessment.SectorId = entity.SectorId;
                    if (!entity.TerritoryId.Equals(Guid.Empty))
                        newassessment.TerritoryId = entity.TerritoryId;

                    dbAssessmentSupplier = newassessment;
                    _assessmentSupplier.Add(dbAssessmentSupplier);
                }
                _dbContext.SaveChanges();

                csrToolNotifier.NotificationType = NotificationType.Success;
                csrToolNotifier.Message = dbAssessmentSupplier.Id.ToString();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public object SaveRange(IList<Core.AssessmentSupplier> entities)
        {
            var response = false;
            try
            {
                _assessmentSupplier.AddRange(Mapper.Map<IList<AssessmentSupplier>>(entities));
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

    }
}
