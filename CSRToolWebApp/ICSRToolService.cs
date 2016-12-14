using System;
using System.Collections.Generic;
using System.ServiceModel;
using CSRTool.Dependencies.Repositories.EntityFramework;
using CSRToolWebApp.DataContracts.Base;
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
using TransactionType = CSRToolWebApp.DataContracts.Base.TransactionType;
using User = CSRToolWebApp.DataContracts.Base.User;
using Version = CSRToolWebApp.DataContracts.Base.Version;
using AssessmentOfferType = CSRToolWebApp.DataContracts.Base.AssessmentOfferType;
using AssessmentTransactionType = CSRToolWebApp.DataContracts.Base.AssessmentTransactionType;
using AssessmentCustomerQuestionAnswer = CSRToolWebApp.DataContracts.Base.AssessmentCustomerQuestionAnswer;
using AssessmentSupplierQuestionAnswer = CSRToolWebApp.DataContracts.Base.AssessmentSupplierQuestionAnswer;
using Supplier = CSRToolWebApp.DataContracts.Base.Supplier;
using SupplyChain = CSRToolWebApp.DataContracts.Base.SupplyChain;

namespace CSRToolWebApp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICSRService" in both code and config file together.
    [ServiceContract]
    public interface ICSRToolService
    {
        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        List<SimpleTerritory> GetCountries();

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        List<IndexType> GetIndexTypes();

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        List<SimpleIndex> GetIndexes();

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        List<Index> GetIndexesForCountry(Guid territoryId, Guid versionId);

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        List<IndexType> GetIndexesForRiskCategory(Guid riskCategoryId);

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        List<Version> GetVersions();

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        List<RiskCategory> GetRiskCategories();

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        List<RiskCategory> GetRiskCategoriesForIndexType(Guid indexTypeId);

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        List<AssessmentTypeQuestion> GetQuestionsForAssessmentType(Guid assessmentTypeId);

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        List<Sector> GetSectors();

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        List<Question> GetQuestions();

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        List<Answer> GetAnswersForQuestion(Guid questionId);

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        List<AssessmentType> GetAssessmentTypes();

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        List<CaseType> GetCaseTypes();

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        List<TransactionType> GetTransactionTypes();

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        List<TransactionType> GetTransactionTypesByAssessmentCustomerId(Guid assessmentCustomerId);

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        List<Customer> GetCustomers();

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        Customer GetCustomer(Guid customerId);

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        Customer GetCustomerByName(string name);

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        Supplier GetSupplierByName(string name);

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        Supplier GetSupplierByID(Guid id);

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        List<SupplyChain> GetSupplyTypes();

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        List<RiskCategory> GetRiskCategoriesForQuestion(Guid questionId);

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        List<OfferType> GetOfferTypes();

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        List<OfferType> GetOfferTypesByAssessmentCustomerId(Guid assessmentCustomerId);

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        List<AssessmentCustomer> GetAssessmentCustomers();


        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        AssessmentCustomer GetAssessmentCustomerById(Guid assessmentCustomerId);

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        List<WebScan> GetWebScansByAssessmentCustomerId(Guid assessmentCustomerId);

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        List<WebScan> GetWebScansByAssessmentSupplierId(Guid assessmentCustomerId);

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        List<AssessmentCustomer> GetAssessmentCustomersForAssessor(Guid userId);

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        IList<AssessmentSupplier> GetAssessmentSuppliersForAssessor(Guid userId);

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        AssessmentSupplier GetAssessmentSupplierById(Guid assessmentSupplierId);

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        List<User> GetUsers();

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        QuestionAnswer GetQuestionAnser(Guid answerId, Guid questionId);

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        IList<QuestionAnswer> GetQuestionAnserByCustomerAssessmentID(Guid assessmentId);

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        IList<QuestionAnswer> GetQuestionAnserBySupplierAssessmentID(Guid assessmentId);

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        GenericResponse SaveFirstTimeUser(User user);

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        GenericResponse SaveAssessmentCustomer(AssessmentCustomer assessmentCustomer);

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        GenericResponse SaveCustomer(Customer customer);

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        GenericResponse SaveSupplier(Supplier supplier);

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        GenericResponse SaveAssessmentOfferType(AssessmentOfferType offferType);

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        GenericResponse SaveAssessmentOfferTypes(IEnumerable<AssessmentOfferType> offferType);

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        GenericResponse SaveAssessmentTransactionTypes(IEnumerable<AssessmentTransactionType> transactions);

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        bool SaveAssessmentCustomerQuestionAnswerList(IEnumerable<AssessmentCustomerQuestionAnswer> transactions);

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        bool SaveAssessmentSupplierQuestionAnswerList(IEnumerable<AssessmentSupplierQuestionAnswer> entities);

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        bool SaveCustomerAssessmentWebscan(WebScan scan);

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        bool SaveSupplierAssessmentWebscan(WebScan scan);

        [OperationContract]
        [FaultContract(typeof(CSRToolFault))]
        GenericResponse SaveAssessmentSupplier(AssessmentSupplier assessment);
    }
}
