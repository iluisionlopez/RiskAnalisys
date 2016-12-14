using AutoMapper;
using CSRTool.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CSRTool.Core;

namespace CSRTool.Dependencies.Repositories.EntityFramework
{
    public class OfferTypeRepository : IOfferTypeRepository
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<OfferType> _offerType;
        private readonly DbSet<AssessmentOfferType> _assessmentofferType;

        public OfferTypeRepository(DbContext dataContext)
        {
            _dbContext = dataContext;
            _offerType = _dbContext.Set<OfferType>();
            _assessmentofferType = _dbContext.Set<AssessmentOfferType>();
        }
        
        public List<Core.OfferType> GetOfferTypes()
        {
            var ret = Mapper.Map<List<OfferType>, List<Core.OfferType>>(GetDbOfferTypes().ToList());
            return ret;
        }

        public List<Core.OfferType> GetOfferTypesByAssessmentCustomerId(Guid assessmentCustomerId)
        {
            var ret = Mapper.Map<List<OfferType>, List<Core.OfferType>>(GetDbOfferTypesByAssessmentCustomerId(assessmentCustomerId).ToList());
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offerTypeId"></param>
        /// <returns></returns>
        public bool SaveVersion(Core.OfferType offerTypeId)
        {
            var dbOfferType = _offerType.FirstOrDefault(x => x.Id == offerTypeId.Id);

            if (dbOfferType != null)
            {
                //update

                dbOfferType.Name = offerTypeId.Name;
              
            }
            else
            {
                //create

                dbOfferType = _offerType.Create();

                dbOfferType.Id          = offerTypeId.Id;
                dbOfferType.Name       = offerTypeId.Name;

                _offerType.Add(dbOfferType);
            }

            _dbContext.SaveChanges();

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offerType"></param>
        /// <returns></returns>
        public CSRToolNotifier SaveAssessmentOfferType(Core.AssessmentOfferType offerType)
        {
            var csrToolNotifier = new CSRToolNotifier();
            try
            {
                var dbAssessmentOfferType = _assessmentofferType.FirstOrDefault(aot => aot.AssessmentCustomerId == offerType.AssessmentCustomerId 
                                                                                     && aot.OfferTypeId == offerType.OfferTypeId);
                if (dbAssessmentOfferType == null)
                {//
                    dbAssessmentOfferType = _assessmentofferType.Create();
                    dbAssessmentOfferType.Id = Guid.NewGuid();
                    dbAssessmentOfferType.OfferTypeId = offerType.OfferTypeId;
                    dbAssessmentOfferType.AssessmentCustomerId = offerType.AssessmentCustomerId;

                    _assessmentofferType.Add(dbAssessmentOfferType);
                    csrToolNotifier.NotificationType = NotificationType.Success;
                    csrToolNotifier.Message = dbAssessmentOfferType.Id.ToString();
                }
                else
                {
                    csrToolNotifier.NotificationType = NotificationType.Warning;
                    csrToolNotifier.Message = string.Format("The offertype {0} is already linked to the AssessmentCustomer {1}", 
                                                            dbAssessmentOfferType.OfferType.Name, 
                                                            dbAssessmentOfferType.AssessmentCustomerId.ToString());
                }
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                csrToolNotifier.NotificationType = NotificationType.Error;
                csrToolNotifier.Message = string.Concat("Message: ", e.Message, Environment.NewLine,
                                                        "Trace: InnerException", e.InnerException, Environment.NewLine,
                                                        "Trace: ", e.StackTrace);
            }
            return csrToolNotifier;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="offertypes"></param>
        /// <returns></returns>
        public CSRToolNotifier SaveAssessmentOfferTypes(IEnumerable<Core.AssessmentOfferType> offertypes)
        {
            var csrToolNotifier = new CSRToolNotifier();
            try
            {
                var dbAssessmentOfferType = _assessmentofferType.Where(aot => aot.AssessmentCustomerId == offertypes.FirstOrDefault().AssessmentCustomerId).AsEnumerable();
                if (dbAssessmentOfferType == null)
                {//                   
                    var offerts = Mapper.Map<IEnumerable<Core.AssessmentOfferType>, IEnumerable<AssessmentOfferType>>(offertypes);
                    dbAssessmentOfferType = _assessmentofferType.AddRange(offerts);
                    csrToolNotifier.NotificationType = NotificationType.Success;
                    csrToolNotifier.Message = "All offerTypes has been added";
                }
                else
                {
                    if (DeleteAssessmentOfferTypes(offertypes.FirstOrDefault().AssessmentCustomerId))
                    {

                        var offerts = Mapper.Map<IEnumerable<Core.AssessmentOfferType>, IEnumerable<AssessmentOfferType>>(offertypes);
                        dbAssessmentOfferType = _assessmentofferType.AddRange(offerts);
                        csrToolNotifier.NotificationType = NotificationType.Success;
                        csrToolNotifier.Message = "All offerTypes has been added";
                    }
                    else
                    {
                        csrToolNotifier.NotificationType = NotificationType.Warning;
                        csrToolNotifier.Message = "Operation couldn't be committed";
                    }
                }
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                csrToolNotifier.NotificationType = NotificationType.Error;
                csrToolNotifier.Message = string.Concat("Message: ", e.Message, Environment.NewLine,
                                                        "Trace: InnerException", e.InnerException, Environment.NewLine,
                                                        "Trace: ", e.StackTrace);
            }
            return csrToolNotifier;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assessmnetID"></param>
        /// <returns></returns>
        public bool DeleteAssessmentOfferTypes(Guid assessmnetID)
        {
            var response = false;
            try
            {
                var dbAssessmentOfferType = _assessmentofferType.Where(x => x.AssessmentCustomerId == assessmnetID);
                if (dbAssessmentOfferType != null)
                {
                    _assessmentofferType.RemoveRange(dbAssessmentOfferType);
                    response = true;
                }
                else
                {
                    response = false;
                }
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                response = false;
                throw;
            }
            return response;
        }

        private IEnumerable<OfferType> GetDbOfferTypes()
        {
            return _offerType;
        }

        private IEnumerable<OfferType> GetDbOfferTypesByAssessmentCustomerId(Guid assessmentCustomerId)
        {
            return _offerType.Where(x=>x.AssessmentOfferType.Any(y=>y.AssessmentCustomerId==assessmentCustomerId));
        }

        public List<Core.AssessmentOfferType> GetAssessmentOfferTypes(Guid assessmentCustomerId)
        {
            var ret = Mapper.Map<List<AssessmentOfferType>, List<Core.AssessmentOfferType>>(GetDbAssessmentOfferTypes(assessmentCustomerId).ToList());
            return ret;
        }

        private IEnumerable<AssessmentOfferType> GetDbAssessmentOfferTypes(Guid assessmentCustomerId)
        {
            return _assessmentofferType.Where(x=>x.AssessmentCustomerId == assessmentCustomerId);
        }
    }
}

