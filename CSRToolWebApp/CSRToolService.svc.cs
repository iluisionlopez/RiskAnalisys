using CSRToolWebApp.DataContracts.Base;
using CSRToolWebApp.Security;
using CSRToolWebApp.ServiceController;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.ServiceModel.Web;
using System.Web;
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
using RiskCategory = CSRToolWebApp.DataContracts.Base.RiskCategory;
using Sector = CSRToolWebApp.DataContracts.Base.Sector;
using TransactionType = CSRToolWebApp.DataContracts.Base.TransactionType;
using User = CSRToolWebApp.DataContracts.Base.User;
using Version = CSRToolWebApp.DataContracts.Base.Version;
using WebScan = CSRToolWebApp.DataContracts.Base.WebScan;
using Supplier = CSRToolWebApp.DataContracts.Base.Supplier;

namespace CSRToolWebApp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CSRService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CSRService.svc or CSRService.svc.cs at the Solution Explorer and start debugging.

    public class CSRToolService : ICSRToolService
    {
        private static readonly IUnityContainer UnityContainer = (HttpContext.Current == null ? new UnityContainer().LoadConfiguration() : HttpContext.Current.Application["unityContainer"] as IUnityContainer);
        private readonly IAuth _auth;

        #region Constructors
        public CSRToolService()
        {
            //HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            _auth = UnityContainer.Resolve<IAuth>();
            _auth.IsValidUser();
        }

        public CSRToolService(IAuth auth)
        {
            if (!UnityContainer.IsRegistered<DbContext>())
            {
                UnityContainer.RegisterType<DbContext>(new PerResolveLifetimeManager(),
                    new InjectionConstructor("CSRToolContext"));
            }

            _auth = auth;
            _auth.IsValidUser();
        }
        #endregion

        #region GET Actions
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "territories")]
        public List<SimpleTerritory> GetCountries()
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetTerritories();
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "indexes")]
        public List<SimpleIndex> GetIndexes()
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetIndexes();
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "indexesbycountry/?territory={territoryid}&Version={Versionid}")]
        public List<Index> GetIndexesForCountry(Guid territoryId, Guid VersionId)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetIndexes(territoryId, VersionId);
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "indexesbyriskcategory/?riskcategory={riskcategoryid}")]
        public List<IndexType> GetIndexesForRiskCategory(Guid riskCategoryId)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetIndexTypesByRiskCategory(riskCategoryId);
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "indextypes")]
        public List<IndexType> GetIndexTypes()
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetIndexTypes();
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Versions")]
        public List<Version> GetVersions()
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetVersions();
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "riskcategories")]
        public List<RiskCategory> GetRiskCategories()
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetRiskCategories();
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "sectors")]
        public List<Sector> GetSectors()
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetSectors();
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "riskcategoriesbyindex/?indextype={indextypeid}")]
        public List<RiskCategory> GetRiskCategoriesForIndexType(Guid indexTypeId)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetRiskCategoriesByIndexType(indexTypeId);
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "questionsbyassessmenttype/?assessmenttype={assessmenttypeid}")]
        public List<AssessmentTypeQuestion> GetQuestionsForAssessmentType(Guid assessmentTypeId)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetQuestionsForAssessmentType(assessmentTypeId);
        }
        
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "questions")]
        public List<Question> GetQuestions()
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetQuestions();
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "answersbyquestion/?question={questionid}")]
        public List<Answer> GetAnswersForQuestion(Guid questionId)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetAnswersForQuestion(questionId);
        }
        
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "assessmenttypes")]
        public List<AssessmentType> GetAssessmentTypes()
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetAssessmentTypes();
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "transactiontypes")]
        public List<TransactionType> GetTransactionTypes()
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetTransactionTypes();
        }
        
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "transactiontypesbyassessment/?assessmentcustomerid={assessmentcustomerId}")]
        public List<TransactionType> GetTransactionTypesByAssessmentCustomerId(Guid assessmentCustomerId)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetTransactionTypesByAssessmentCustomerId(assessmentCustomerId);
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "casetypes")]
        public List<CaseType> GetCaseTypes()
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetCaseTypes();
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "customers")]
        public List<Customer> GetCustomers()
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetCustomers();
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "customerbyid/?customerId={customerId}")]
        public Customer GetCustomer(Guid customerId)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetCustomer(customerId);
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "customerbyname/?Name={name}")]
        public Customer GetCustomerByName(string name)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetCustomerByName(name);
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "supplierbyname/?Name={name}")]
        public Supplier GetSupplierByName(string name)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetSupplierByName(name);
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "supplierbyid/?Id={id}")]
        public Supplier GetSupplierByID(Guid id)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetSupplier(id);
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "riskcategoriesbyquestion/?question={questionid}")]
        public List<RiskCategory> GetRiskCategoriesForQuestion(Guid questionId)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetRiskCategoriesForQuestion(questionId);
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "offertypes")]
        public List<OfferType> GetOfferTypes()
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetOfferTypes();
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "supplytypes")]
        public List<SupplyChain> GetSupplyTypes()
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetSupplyTypes();
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "offertypes/?assessmentcustomerid={assessmentcustomerId}")]
        public List<OfferType> GetOfferTypesByAssessmentCustomerId(Guid assessmentCustomerId)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetOfferTypesByAssessmentCustomerId(assessmentCustomerId);
        }
        
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "assessmentcustomers")]
        public List<AssessmentCustomer> GetAssessmentCustomers()
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetAssessmentCustomers();
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "assessmentcustomers/?user={userId}")]
        public List<AssessmentCustomer> GetAssessmentCustomersForAssessor(Guid userId)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetAssessmentCustomersForAssessor(userId);
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "assessmentsuppliers/?user={userId}")]
        public IList<AssessmentSupplier> GetAssessmentSuppliersForAssessor(Guid userId)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetAssessmentSuppliersForAssessor(userId);
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "assessmentsupplier/?id={assessmentSupplierId}")]
        public AssessmentSupplier GetAssessmentSupplierById(Guid assessmentSupplierId)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetAssessmentSupplierById(assessmentSupplierId);
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "users")]
        public List<User> GetUsers()
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetUsers();
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "assessmentcustomer/?id={assessmentcustomerId}")]
        public AssessmentCustomer GetAssessmentCustomerById(Guid assessmentCustomerId)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetAssessmentCustomersById(assessmentCustomerId);
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "webscan/?id={assessmentcustomerId}")]
        public List<WebScan> GetWebScansByAssessmentCustomerId(Guid assessmentCustomerId)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetWebScansByAssessmentCustomerId(assessmentCustomerId);
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "supplierwebscan/?id={assessmentcustomerId}")]
        public List<WebScan> GetWebScansByAssessmentSupplierId(Guid assessmentCustomerId)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetWebScansByAssessmentSupplierId(assessmentCustomerId);
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "questionanser/?answerId={answerId}&questionID={questionId}")]
        public QuestionAnswer GetQuestionAnser(Guid answerId, Guid questionId)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetQuestionAnswer(answerId, questionId);
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "customerassessmentquestionanser/?Id={assessmentId}")]
        public IList<QuestionAnswer> GetQuestionAnserByCustomerAssessmentID(Guid assessmentId)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetQuestionAnswerByCustomerAssessment(assessmentId);
        }

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "supplierassessmentquestionanser/?Id={assessmentId}")]
        public IList<QuestionAnswer> GetQuestionAnserBySupplierAssessmentID(Guid assessmentId)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.GetQuestionAnswerBySupplierAssessment(assessmentId);
        }

        #endregion GET

        #region POST Actions
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "assessmentcustomer")]
        public GenericResponse SaveAssessmentCustomer(AssessmentCustomer assessmentCustomer)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.Save(assessmentCustomer);
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "assessmentsupplier")]
        public GenericResponse SaveAssessmentSupplier(AssessmentSupplier assessment)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.SaveAssessmentSupplier(assessment);
        }

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "user")]
        public GenericResponse SaveFirstTimeUser(User user)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.SaveFirstTimeUser(user);
        }

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "customer")]
        public GenericResponse SaveCustomer(Customer customer)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.SaveCustomer(customer);
        }

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "supplier")]
        public GenericResponse SaveSupplier(Supplier supplier)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.SaveSupplier(supplier);
        }

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "assessmentoffertype")]
        public GenericResponse SaveAssessmentOfferType(AssessmentOfferType offferType)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.SaveAssessmentOfferType(offferType);
        }

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "assessmentoffertypes")]
        public GenericResponse SaveAssessmentOfferTypes(IEnumerable<AssessmentOfferType> offfers)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.SaveAssessmentOfferTypes(offfers);
        }

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "assessmenttransactiontypes")]
        public GenericResponse SaveAssessmentTransactionTypes(IEnumerable<AssessmentTransactionType> transactions)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.SaveAssessmentTransactionTypes(transactions);
        }

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "assessmentwebscan")]
        public bool SaveCustomerAssessmentWebscan(WebScan scan)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.SaveCustomerWebScan(scan);
        }

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "assessmentsupplierwebscan")]
        public bool SaveSupplierAssessmentWebscan(WebScan scan)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.SaveSupplierWebScan(scan);
        }

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "customerquestionanswer")]
        public bool SaveAssessmentCustomerQuestionAnswerList(IEnumerable<AssessmentCustomerQuestionAnswer> entities)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.SaveAssessmentCustomerQuestionAnswerList(entities);
        }

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "supplierquestionanswer")]
        public bool SaveAssessmentSupplierQuestionAnswerList(IEnumerable<AssessmentSupplierQuestionAnswer> entities)
        {
            var masterDataController = new MasterDataController(UnityContainer, _auth);
            return masterDataController.SaveAssessmentSupplierQuestionAnswerList(entities);
        }


        #endregion POST

        #region DELETE
        //[WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "removeoffertypes/?assessmentcustomerid={assessmentcustomerId}")]
        //public bool DeleteAssessmentOfferType(Guid assessmentCustomerId)
        //{
        //    var masterDataController = new MasterDataController(UnityContainer, _auth);
        //    return masterDataController.DeleteAssessmentOfferType(assessmentCustomerId);
        //}
        #endregion

    }
}

