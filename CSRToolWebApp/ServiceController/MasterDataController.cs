using System;
using System.Collections.Generic;
using AutoMapper;
using CSRTool.Core;
using CSRTool.Core.Interfaces;
using CSRTool.Dependencies.Repositories.EntityFramework;
using CSRToolWebApp.DataContracts.Base;
using CSRToolWebApp.Security;
using Microsoft.Practices.Unity;
using Answer = CSRToolWebApp.DataContracts.Base.Answer;
using AssessmentCustomer = CSRToolWebApp.DataContracts.Base.AssessmentCustomer;
using AssessmentSupplier = CSRToolWebApp.DataContracts.Base.AssessmentSupplier;
using AssessmentType = CSRToolWebApp.DataContracts.Base.AssessmentType;
using AssessmentTypeQuestion = CSRToolWebApp.DataContracts.Base.AssessmentTypeQuestion;
using CaseType = CSRToolWebApp.DataContracts.Base.CaseType;
using Customer = CSRToolWebApp.DataContracts.Base.Customer;
using Index = CSRToolWebApp.DataContracts.Base.Index;
using IndexType = CSRToolWebApp.DataContracts.Base.IndexType;
using OfferType = CSRToolWebApp.DataContracts.Base.OfferType;
using Question = CSRToolWebApp.DataContracts.Base.Question;
using QuestionAnswer = CSRToolWebApp.DataContracts.Base.QuestionAnswer;
using RiskCategory = CSRToolWebApp.DataContracts.Base.RiskCategory;
using Sector = CSRToolWebApp.DataContracts.Base.Sector;
using Territory = CSRTool.Core.Territory;
using TransactionType = CSRToolWebApp.DataContracts.Base.TransactionType;
using User = CSRToolWebApp.DataContracts.Base.User;
using Version = CSRToolWebApp.DataContracts.Base.Version;
using WebScan = CSRToolWebApp.DataContracts.Base.WebScan;
using AssessmentOfferType = CSRToolWebApp.DataContracts.Base.AssessmentOfferType;
using AssessmentTransactionType = CSRToolWebApp.DataContracts.Base.AssessmentTransactionType;
using AssessmentCustomerQuestionAnswer = CSRToolWebApp.DataContracts.Base.AssessmentCustomerQuestionAnswer;
using Supplier = CSRToolWebApp.DataContracts.Base.Supplier;
using SupplyChain = CSRToolWebApp.DataContracts.Base.SupplyChain;
using AssessmentSupplierQuestionAnswer = CSRToolWebApp.DataContracts.Base.AssessmentSupplierQuestionAnswer;

namespace CSRToolWebApp.ServiceController
{

    public class MasterDataController
    {

        private readonly IUnityContainer _unityContainer;
        private readonly IAuth _auth;

        public MasterDataController(IUnityContainer unityContainer, IAuth auth)
        {
            _unityContainer = unityContainer;
            _auth = auth;
        }

        public List<SimpleTerritory> GetTerritories()
        {
            var territoryLogic = _unityContainer.Resolve<ITerritoryService>();
            var territoryList = territoryLogic.GetTerritories();

            Mapper.CreateMap<Territory, SimpleTerritory>();
            var ret = Mapper.Map<List<Territory>, List<SimpleTerritory>>(territoryList);
            return ret;
        }

        public List<SimpleIndex> GetIndexes()
        {
            var indexLogic = _unityContainer.Resolve<IIndexService>();
            var indexList = indexLogic.GetIndexes();

            Mapper.CreateMap<CSRTool.Core.Index, SimpleIndex>();
            var ret = Mapper.Map<List<CSRTool.Core.Index>, List<SimpleIndex>>(indexList);
            return ret;
        }

        public List<Index> GetIndexes(Guid territoryId, Guid versionId)
        {
            var indexLogic = _unityContainer.Resolve<IIndexService>();
            var indexList = indexLogic.GetIndexes(territoryId, versionId);

            Mapper.CreateMap<CSRTool.Core.Index, Index>();
            var ret = Mapper.Map<List<CSRTool.Core.Index>, List<Index>>(indexList);
            return ret;
        }

        public List<IndexType> GetIndexTypes()
        {
            var indexTypeLogic = _unityContainer.Resolve<IIndexTypeService>();
            var indexTypeList = indexTypeLogic.GetIndexTypes();

            Mapper.CreateMap<CSRTool.Core.IndexType, IndexType>();
            var ret = Mapper.Map<List<CSRTool.Core.IndexType>, List<IndexType>>(indexTypeList);
            return ret;
        }

        public List<Version> GetVersions()
        {
            var versionsLogic = _unityContainer.Resolve<IVersionService>();
            var versionList = versionsLogic.GetVersions();

            Mapper.CreateMap<CSRTool.Core.Version, Version>();
            var ret = Mapper.Map<List<CSRTool.Core.Version>, List<Version>>(versionList);
            return ret;
        }

        public List<RiskCategory> GetRiskCategories()
        {
            var riskCategoriesLogic = _unityContainer.Resolve<IRiskCategoryService>();
            var riskCategoryList = riskCategoriesLogic.GetRiskCategories();

            Mapper.CreateMap<CSRTool.Core.RiskCategory, RiskCategory>();
            var ret = Mapper.Map<List<CSRTool.Core.RiskCategory>, List<RiskCategory>>(riskCategoryList);
            return ret;
        }

        public List<Sector> GetSectors()
        {
            var sectorsLogic = _unityContainer.Resolve<ISectorService>();
            var sectorList = sectorsLogic.GetSectors();

            Mapper.CreateMap<CSRTool.Core.Sector, Sector>();
            var ret = Mapper.Map<List<CSRTool.Core.Sector>, List<Sector>>(sectorList);
            return ret;
        }
        
        public List<IndexType> GetIndexTypesByRiskCategory(Guid riskCategoryId)
        {
            var indexTypeLogic = _unityContainer.Resolve<IIndexTypeService>();
            var indexTypeList = indexTypeLogic.GetIndexTypesByRiskCategory(riskCategoryId);

            Mapper.CreateMap<CSRTool.Core.IndexType, IndexType>();
            var ret = Mapper.Map<List<CSRTool.Core.IndexType>, List<IndexType>>(indexTypeList);
            return ret;
        }

        public List<RiskCategory> GetRiskCategoriesByIndexType(Guid indexTypeId)
        {
            var riskCategoryLogic = _unityContainer.Resolve<IRiskCategoryService>();
            var riskCategoryList = riskCategoryLogic.GetRiskCategoriesByIndexType(indexTypeId);

            Mapper.CreateMap<CSRTool.Core.RiskCategory, RiskCategory>();
            var ret = Mapper.Map<List<CSRTool.Core.RiskCategory>, List<RiskCategory>>(riskCategoryList);

            return ret;
        }

        public List<AssessmentTypeQuestion> GetQuestionsForAssessmentType(Guid assessmentTypeId)
        {
            var questionLogic = _unityContainer.Resolve<IAssessmentTypeQuestionService>();
            var questionList = questionLogic.GetQuestionsForAssessment(assessmentTypeId);

            Mapper.CreateMap<CSRTool.Core.AssessmentTypeQuestion, AssessmentTypeQuestion>();
            var ret = Mapper.Map<List<CSRTool.Core.AssessmentTypeQuestion>, List<AssessmentTypeQuestion>>(questionList);

            return ret;
        }

        public List<Question> GetQuestions()
        {
            var questionLogic = _unityContainer.Resolve<IQuestionService>();
            var questionList = questionLogic.GetQuestions();

            Mapper.CreateMap<CSRTool.Core.Question, Question>();
            var ret = Mapper.Map<List<CSRTool.Core.Question>, List<Question>>(questionList);

            return ret;
        }

        public List<Answer> GetAnswersForQuestion(Guid questionId)
        {
            var answerLogic = _unityContainer.Resolve<IAnswerService>();
            var answerList = answerLogic.GetAnswersForQuestion(questionId);

            Mapper.CreateMap<CSRTool.Core.Answer, Answer>();
            var ret = Mapper.Map<List<CSRTool.Core.Answer>, List<Answer>>(answerList);

            return ret;
        }

        public List<AssessmentType> GetAssessmentTypes()
        {
            var assessmentTypeLogic = _unityContainer.Resolve<IAssessmentTypeService>();
            var assessmentList = assessmentTypeLogic.GetAssessmentTypes();

            Mapper.CreateMap<CSRTool.Core.AssessmentType, AssessmentType>();
            var ret = Mapper.Map<List<CSRTool.Core.AssessmentType>, List<AssessmentType>>(assessmentList);

            return ret;
        }

        public List<TransactionType> GetTransactionTypes()
        {
            var transactionTypeLogic = _unityContainer.Resolve<ITransactionTypeService>();
            var transactionList = transactionTypeLogic.GetTransactionTypes();

            Mapper.CreateMap<CSRTool.Core.TransactionType, TransactionType>();
            var ret = Mapper.Map<List<CSRTool.Core.TransactionType>, List<TransactionType>>(transactionList);

            return ret;
        }
        
        public List<TransactionType> GetTransactionTypesByAssessmentCustomerId(Guid assessmentCustomerId)
        {
            var transactionTypeLogic = _unityContainer.Resolve<ITransactionTypeService>();
            var transactionList = transactionTypeLogic.GetTransactionTypesByAssessmentCustomerId(assessmentCustomerId);

            Mapper.CreateMap<CSRTool.Core.TransactionType, TransactionType>();
            var ret = Mapper.Map<List<CSRTool.Core.TransactionType>, List<TransactionType>>(transactionList);

            return ret;
        }

        public List<CaseType> GetCaseTypes()
        {
            var caseTypeLogic = _unityContainer.Resolve<ICaseTypeService>();
            var caseTypeList = caseTypeLogic.GetCaseTypes();

            Mapper.CreateMap<CSRTool.Core.CaseType, CaseType>();
            var ret = Mapper.Map<List<CSRTool.Core.CaseType>, List<CaseType>>(caseTypeList);

            return ret;
        }

        public List<Customer> GetCustomers()
        {
            var customerLogic = _unityContainer.Resolve<ICustomerService>();
            var customerList = customerLogic.GetCustomers();

            Mapper.CreateMap<CSRTool.Core.Customer, Customer>();
            var ret = Mapper.Map<List<CSRTool.Core.Customer>, List<Customer>>(customerList);

            return ret;
        }

        public Customer GetCustomer(Guid customerId)
        {
            var customerLogic = _unityContainer.Resolve<ICustomerService>();
            var customer = customerLogic.GetCustomer(customerId);

            Mapper.CreateMap<CSRTool.Core.Customer, Customer>();
            var ret = Mapper.Map<CSRTool.Core.Customer, Customer>(customer);

            return ret;
        }
               

        public List<RiskCategory> GetRiskCategoriesForQuestion(Guid questionId)
        {
            var riskCategoryLogic = _unityContainer.Resolve<IRiskCategoryService>();
            var riskCategoryList = riskCategoryLogic.GetRiskCategoriesForQuestion(questionId);

            Mapper.CreateMap<CSRTool.Core.RiskCategory, RiskCategory>();
            var ret = Mapper.Map<List<CSRTool.Core.RiskCategory>, List<RiskCategory>>(riskCategoryList);

            return ret;
        }

       

        public List<OfferType> GetOfferTypes()
        {
            var offerTypeLogic = _unityContainer.Resolve<IOfferTypeService>();
            var offerTypeList = offerTypeLogic.GetOfferTypes();

            Mapper.CreateMap<CSRTool.Core.OfferType, OfferType>();
            var ret = Mapper.Map<List<CSRTool.Core.OfferType>, List<OfferType>>(offerTypeList);

            return ret;
        }

        public Customer GetCustomerByName(string customerName)
        {
            var customerLogic = _unityContainer.Resolve<ICustomerService>();
            var customer = customerLogic.GetCustomerByName(customerName);

            Mapper.CreateMap<CSRTool.Core.Customer, Customer>();
            var ret = Mapper.Map<CSRTool.Core.Customer, Customer>(customer);

            return ret;
        }

        public List<SupplyChain> GetSupplyTypes()
        {
            var repository = _unityContainer.Resolve<ISupplyChainRespository>();
            var types = repository.GetAll();

            Mapper.CreateMap<CSRTool.Core.SupplyChain, SupplyChain>();
            var ret = Mapper.Map<IList<CSRTool.Core.SupplyChain>, List<SupplyChain>>(types);

            return ret;
        }

        public Supplier GetSupplierByName(string name)
        {
            var service = _unityContainer.Resolve<ISupplierService>();
            var supplier = service.GetSupplierByName(name);

            Mapper.CreateMap<CSRTool.Core.Supplier, Supplier>();
            var ret = Mapper.Map<CSRTool.Core.Supplier, Supplier>(supplier);

            return ret;
        }

        public Supplier GetSupplier(Guid id)
        {
            var service = _unityContainer.Resolve<ISupplierService>();
            var supplier = service.GetSupplier(id);

            Mapper.CreateMap<CSRTool.Core.Supplier, Supplier>();
            var ret = Mapper.Map<CSRTool.Core.Supplier, Supplier>(supplier);

            return ret;
        }

        public List<AssessmentCustomer> GetAssessmentCustomers()
        {
            var assessmentCustomerLogic = _unityContainer.Resolve<IAssessmentCustomerService>();
            var assessmentCustomerList = assessmentCustomerLogic.GetAssessmentCustomers();

            Mapper.CreateMap<CSRTool.Core.AssessmentCustomer, AssessmentCustomer>();
            var ret = Mapper.Map<List<CSRTool.Core.AssessmentCustomer>, List<AssessmentCustomer>>(assessmentCustomerList);

            return ret;
        }
       
        public List<AssessmentCustomer> GetAssessmentCustomersForAssessor(Guid userId)
        {
            var assessmentCustomerLogic = _unityContainer.Resolve<IAssessmentCustomerService>();
            var assessmentCustomerList = assessmentCustomerLogic.GetAssessmentCustomersForAssessor(userId);

            Mapper.CreateMap<CSRTool.Core.AssessmentCustomer, AssessmentCustomer>();
            var ret = Mapper.Map<List<CSRTool.Core.AssessmentCustomer>, List<AssessmentCustomer>>(assessmentCustomerList);

            return ret;
        }

        public IList<AssessmentSupplier> GetAssessmentSuppliersForAssessor(Guid userId)
        {
            var logic = _unityContainer.Resolve<IAssessmentSupplierService>();
            var response = logic.FindBy(x => x.AssessorId == userId);

            var ret = Mapper.Map<IList<CSRTool.Core.AssessmentSupplier>, IList<AssessmentSupplier>>(response);

            return ret;
        }

        public AssessmentSupplier GetAssessmentSupplierById(Guid assessmentSupplierId)
        {
            var logic = _unityContainer.Resolve<IAssessmentSupplierService>();
            var assessmnet = logic.GetSingle(assessmentSupplierId);            
            var ret = Mapper.Map<CSRTool.Core.AssessmentSupplier, AssessmentSupplier>(assessmnet);

            return ret;
        }

        public AssessmentCustomer GetAssessmentCustomersById(Guid assessmentCustomerId)
        {
            var assessmentCustomerLogic = _unityContainer.Resolve<IAssessmentCustomerService>();
            var assessmentCustomerList = assessmentCustomerLogic.GetAssessmentCustomerById(assessmentCustomerId);

            Mapper.CreateMap<CSRTool.Core.AssessmentCustomer, AssessmentCustomer>();
            var ret = Mapper.Map<CSRTool.Core.AssessmentCustomer, AssessmentCustomer>(assessmentCustomerList);

            return ret;
        }

        internal List<WebScan> GetWebScansByAssessmentSupplierId(Guid assessmentId)
        {
            var webScanLogic = _unityContainer.Resolve<IWebScanService>();
            var webScanList = webScanLogic.GetWebScansByAssessmentSupplierId(assessmentId);

            Mapper.CreateMap<CSRTool.Core.WebScan, WebScan>();
            var ret = Mapper.Map<List<CSRTool.Core.WebScan>, List<WebScan>>(webScanList);

            return ret;
        }

        public List<User> GetUsers()
        {
            var userLogic = _unityContainer.Resolve<IUserService>();
            var userList = userLogic.GetUsers();

            Mapper.CreateMap<CSRTool.Core.User, User>();
            var ret = Mapper.Map<List<CSRTool.Core.User>, List<User>>(userList);

            return ret;
        }

        public List<OfferType> GetOfferTypesByAssessmentCustomerId(Guid assessmentCustomerId)
        {
            var offerTypeLogic = _unityContainer.Resolve<IOfferTypeService>();
            var offerTypeList = offerTypeLogic.GetOfferTypesByAssessmentCustomerId(assessmentCustomerId);

            Mapper.CreateMap<CSRTool.Core.OfferType, OfferType>();
            var ret = Mapper.Map<List<CSRTool.Core.OfferType>, List<OfferType>>(offerTypeList);

            return ret;
        }

        internal IList<QuestionAnswer> GetQuestionAnswerBySupplierAssessment(Guid assessmentId)
        {
            var service = _unityContainer.Resolve<IQuestionAnswerService>();
            var offerTypeList = service.GetQuestionAnswersBySupplierAssessment(assessmentId);

            Mapper.CreateMap<CSRTool.Core.QuestionAnswer, QuestionAnswer>();
            var ret = Mapper.Map<List<CSRTool.Core.QuestionAnswer>, List<QuestionAnswer>>(offerTypeList);

            return ret;
        }

        public QuestionAnswer GetQuestionAnswer(Guid answerId, Guid questionId)
        {
            var service = _unityContainer.Resolve<IQuestionAnswerService>();
            var result = service.FindBy(x=>x.AnswerId == answerId && x.QuestionId == questionId);

            Mapper.CreateMap<CSRTool.Core.QuestionAnswer, QuestionAnswer>();
            var ret = Mapper.Map<CSRTool.Core.QuestionAnswer, QuestionAnswer>(result);

            return ret;
        }

        public List<QuestionAnswer> GetQuestionAnswerByCustomerAssessment(Guid assessmentId)
        {
            var service = _unityContainer.Resolve<IQuestionAnswerService>();
            var offerTypeList = service.GetQuestionAnswersByCustomerAssessment(assessmentId);

            Mapper.CreateMap<CSRTool.Core.QuestionAnswer, QuestionAnswer>();
            var ret = Mapper.Map<List<CSRTool.Core.QuestionAnswer>, List<QuestionAnswer>>(offerTypeList);

            return ret;
        }

        internal GenericResponse SaveAssessmentSupplier(AssessmentSupplier assessment)
        {
            var ret = new GenericResponse { ResponseMessage = "FAILED" };
            Mapper.CreateMap<AssessmentSupplier, CSRTool.Core.AssessmentSupplier>();

            var coreAssessmentSupplier = Mapper.Map<CSRTool.Core.AssessmentSupplier>(assessment);
            var repository = _unityContainer.Resolve<IAssessmentSupplierReposity>();
            var csrToolNotifier = repository.Save(coreAssessmentSupplier);

            ret.ResponseMessage = csrToolNotifier.NotificationType == NotificationType.Success ? "OK" : csrToolNotifier.Message;
            Guid id;
            if (Guid.TryParse(csrToolNotifier.Message, out id))
                ret.Id = id;

            return ret;
        }

        public List<WebScan> GetWebScansByAssessmentCustomerId(Guid assessmentCustomerId)
        {
            var webScanLogic = _unityContainer.Resolve<IWebScanService>();
            var webScanList = webScanLogic.GetWebScansByAssessmentCustomerId(assessmentCustomerId);

            Mapper.CreateMap<CSRTool.Core.WebScan, WebScan>();
            var ret = Mapper.Map<List<CSRTool.Core.WebScan>, List<WebScan>>(webScanList);

            return ret;
        }
        
        public GenericResponse Save(AssessmentCustomer assessmentCustomer)
        {
            var ret = new GenericResponse { ResponseMessage = "FAILED" };
            Mapper.CreateMap<AssessmentCustomer, CSRTool.Core.AssessmentCustomer>();

            var coreAssessmentCustomer = Mapper.Map<CSRTool.Core.AssessmentCustomer>(assessmentCustomer);
            var repository = _unityContainer.Resolve<IAssessmentCustomerRepository>();
            var csrToolNotifier = repository.SaveAssessmentCustomer(coreAssessmentCustomer);

            ret.ResponseMessage = csrToolNotifier.NotificationType == NotificationType.Success ? "OK" : csrToolNotifier.Message;
            Guid id;
            if (Guid.TryParse(csrToolNotifier.Message, out id))
                ret.Id = id;

            return ret;
        }

        public GenericResponse SaveAssessmentOfferType(AssessmentOfferType offferType)
        {
            var ret = new GenericResponse { ResponseMessage = "FAILED" };
            Mapper.CreateMap<AssessmentOfferType, CSRTool.Core.AssessmentOfferType>();

            var assessmentOfferType = Mapper.Map<CSRTool.Core.AssessmentOfferType>(offferType);
            var repository = _unityContainer.Resolve<IOfferTypeRepository>();
            var csrToolNotifier = repository.SaveAssessmentOfferType(assessmentOfferType);

            ret.ResponseMessage = csrToolNotifier.NotificationType == NotificationType.Success ? "OK" : csrToolNotifier.Message;
            Guid id;
            if (Guid.TryParse(csrToolNotifier.Message, out id))
                ret.Id = id;

            return ret;
        }

        public bool SaveSupplierWebScan(WebScan scan)
        {
            var ret = new GenericResponse { ResponseMessage = "FAILED" };
            Mapper.CreateMap<WebScan, CSRTool.Core.WebScan>();

            var webScan = Mapper.Map<CSRTool.Core.WebScan>(scan);
            var repository = _unityContainer.Resolve<IWebScanRepository>();
            var response = repository.SaveSupplierWebScan(webScan);
            return response;
        }

        public bool SaveCustomerWebScan(WebScan scan)
        {
            var ret = new GenericResponse { ResponseMessage = "FAILED" };
            Mapper.CreateMap<WebScan, CSRTool.Core.WebScan>();

            var webScan = Mapper.Map<CSRTool.Core.WebScan > (scan);
            var repository = _unityContainer.Resolve<IWebScanRepository>();
            var response = repository.SaveCustomerWebScan(webScan);
            return response;
        }

        internal bool SaveAssessmentSupplierQuestionAnswerList(IEnumerable<AssessmentSupplierQuestionAnswer> entities)
        {
            var result = false;
            Mapper.CreateMap<AssessmentSupplierQuestionAnswer, CSRTool.Core.AssessmentSupplierQuestionAnswer>();

            var list = Mapper.Map<IEnumerable<CSRTool.Core.AssessmentSupplierQuestionAnswer>>(entities);
            var repository = _unityContainer.Resolve<IQuestionAnswerRepository>();
            result = repository.SaveAssessmentSupplierQuestionAnswerList(list);

            return result;
        }

        public bool SaveAssessmentCustomerQuestionAnswerList(IEnumerable<AssessmentCustomerQuestionAnswer> entities)
        {
            var result = false;
            Mapper.CreateMap<AssessmentCustomerQuestionAnswer, CSRTool.Core.AssessmentCustomerQuestionAnswer>();

            var list = Mapper.Map<IEnumerable<CSRTool.Core.AssessmentCustomerQuestionAnswer>>(entities);
            var repository = _unityContainer.Resolve<IQuestionAnswerRepository>();
            result = repository.SaveAssessmentCustomerQuestionAnswerList(list);

            return result;
        }

        public GenericResponse SaveAssessmentTransactionTypes(IEnumerable<DataContracts.Base.AssessmentTransactionType> transactions)
        {
            var ret = new GenericResponse { ResponseMessage = "FAILED" };
            Mapper.CreateMap<AssessmentTransactionType, CSRTool.Core.AssessmentTransactionType>();

            var assessmentTransactionType = Mapper.Map<IEnumerable<CSRTool.Core.AssessmentTransactionType>>(transactions);
            var repository = _unityContainer.Resolve<ITransactionTypeRepository>();
            var csrToolNotifier = repository.SaveAssessmentTransactionTypes(assessmentTransactionType);

            ret.ResponseMessage = csrToolNotifier.NotificationType == NotificationType.Success ? "OK" : csrToolNotifier.Message;
            return ret;
        }

        public GenericResponse SaveAssessmentOfferTypes(IEnumerable<AssessmentOfferType> offfers)
        {
            var ret = new GenericResponse { ResponseMessage = "FAILED" };
            Mapper.CreateMap<AssessmentOfferType, CSRTool.Core.AssessmentOfferType>();

            var assessmentOfferType = Mapper.Map<IEnumerable<CSRTool.Core.AssessmentOfferType>>(offfers);
            var repository = _unityContainer.Resolve<IOfferTypeRepository>();
            var csrToolNotifier = repository.SaveAssessmentOfferTypes(assessmentOfferType);

            ret.ResponseMessage = csrToolNotifier.NotificationType == NotificationType.Success ? "OK" : csrToolNotifier.Message;
            return ret;
        }

        public bool DeleteAssessmentOfferType(Guid assessmentCustomerId)
        {
            var ret = false;

            var repository = _unityContainer.Resolve<IOfferTypeRepository>();
            ret = repository.DeleteAssessmentOfferTypes(assessmentCustomerId);            
            
            return ret;
        }

        public GenericResponse SaveFirstTimeUser(User user)
        {
            var ret = new GenericResponse { ResponseMessage = "FAILED" };
            Mapper.CreateMap<User, CSRTool.Core.User>();

            var coreUser = Mapper.Map<CSRTool.Core.User>(user);
            var repository = _unityContainer.Resolve<IUserRepository>();
            var csrToolNotifier = repository.SaveFirstTimeUser(coreUser);

            ret.ResponseMessage = csrToolNotifier.NotificationType == NotificationType.Success ? "OK" : csrToolNotifier.Message;
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public GenericResponse SaveCustomer(Customer customer)
        {
            var ret = new GenericResponse { ResponseMessage = "FAILED" };
            Mapper.CreateMap<Customer, CSRTool.Core.Customer>();

            var coreCustomer = Mapper.Map<CSRTool.Core.Customer>(customer);
            var repository = _unityContainer.Resolve<ICustomerRepository>();
            var csrToolNotifier = repository.SaveCustomer(coreCustomer);

            ret.ResponseMessage = csrToolNotifier.NotificationType == NotificationType.Success ? "OK" : csrToolNotifier.Message;
            Guid id;
            if (Guid.TryParse(csrToolNotifier.Message, out id))
                ret.Id = id;

            return ret;
        }


        public GenericResponse SaveSupplier(Supplier supplier)
        {
            var ret = new GenericResponse { ResponseMessage = "FAILED" };
            Mapper.CreateMap<Supplier, CSRTool.Core.Supplier>();

            var coreSupplier = Mapper.Map<CSRTool.Core.Supplier>(supplier);
            var repository = _unityContainer.Resolve<ISupplierRepository>();
            var csrToolNotifier = repository.SaveSupplier(coreSupplier);

            ret.ResponseMessage = csrToolNotifier.NotificationType == NotificationType.Success ? "OK" : csrToolNotifier.Message;
            Guid id;
            if (Guid.TryParse(csrToolNotifier.Message, out id))
                ret.Id = id;

            return ret;
        }
    }
}
